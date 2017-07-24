using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using YamlDotNet.Serialization;

namespace Nemi.Extensions.Configuration.Yaml
{
	public class YamlConfigurationProvider : FileConfigurationProvider
	{
		/// <summary>
		/// Initializes a new instance of YamlConfiguration source
		/// </summary>
		/// <param name="source"></param>
		public YamlConfigurationProvider(FileConfigurationSource source) : base(source)
		{
		}

		/// <summary>
		/// load the file stream
		/// </summary>
		/// <param name="stream"> file stream to load</param>
		public override void Load(Stream stream)
		{
			try
			{
				var parser = new YamlConfigurationFileParser();
				Data = parser.Parse(stream);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Could not parse the Yaml file.", ex);
			}
		}
	}
}
