using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.RepresentationModel;
using System.Linq;

namespace Nemi.Extensions.Configuration.Yaml
{
	public class YamlConfigurationFileParser
    {
		private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		private readonly Stack<string> _context = new Stack<string>();
		private string _currentPath;

		/// <summary>
		/// Parse the input stream
		/// </summary>
		/// <param name="input">YAML file stream</param>
		/// <returns>collection of <see cref="IDictionary{TKey, TValue}"/></returns>
		public IDictionary<string, string> Parse(Stream input)
		{
			//clear
			_data.Clear();
			_context.Clear();

			var yaml = new YamlStream();
			yaml.Load(new StreamReader(input));

			if (yaml.Documents.Count > 0)
			{

				// Examine the stream
				var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

				foreach (var item in mapping.Children)
				{
					EnterContext(item.Key.ToString());
					if (item.Value is YamlSequenceNode)
					{
						VisitSequenceNode((YamlSequenceNode)item.Value);
					}

					if (item.Value is YamlScalarNode)
					{
						VisitScalarNode(item.Value.ToString());
					}

					if (item.Value is YamlMappingNode)
					{
						VisitMappingNode((YamlMappingNode)item.Value);
					}
					ExitContext();
				}
			}
			return _data;
		}		

		private void VisitScalarNode(string data)
		{
			var key = _currentPath;
			if (_data.ContainsKey(key))
			{
				throw new ArgumentException(string.Format("A duplicate key '{0}' was found.", key));
			}
			_data[key] = data;
		}

		private void VisitMappingNode(YamlMappingNode node)
		{
			foreach (var item in node.Children)
			{
				EnterContext(item.Key.ToString());

				if (item.Value.NodeType != YamlNodeType.Mapping)
					VisitScalarNode(item.Value.ToString());
				else
					VisitMappingNode((YamlMappingNode)item.Value);

				ExitContext();
			}
		}

		private void VisitSequenceNode(YamlSequenceNode node)
		{
			foreach (var item in node.Children)
			{
				VisitScalarNode(item.ToString());
			}
		}



		private void EnterContext(string context)
		{
			_context.Push(context);
			_currentPath = ConfigurationPath.Combine(_context.Reverse());
		}

		private void ExitContext()
		{
			_context.Pop();
			_currentPath = ConfigurationPath.Combine(_context.Reverse());
		}

	}
}
