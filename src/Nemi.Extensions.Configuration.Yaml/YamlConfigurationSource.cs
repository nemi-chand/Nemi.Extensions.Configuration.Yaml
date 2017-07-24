using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nemi.Extensions.Configuration.Yaml
{
	/// <summary>
	/// Represents a Yaml file as an <see cref="IConfigurationSource"/>.
	/// </summary>
	public class YamlConfigurationSource : FileConfigurationSource
	{
		/// <summary>
		/// Builds the <see cref="IConfigurationProvider"/> for this source
		/// </summary>
		/// <param name="builder"> <see cref="IConfigurationBuilder"/></param>
		/// <returns>A <see cref="YamlConfigurationProvider"/></returns>
		public override IConfigurationProvider Build(IConfigurationBuilder builder)
		{
			FileProvider = FileProvider ?? builder.GetFileProvider();
			return new YamlConfigurationProvider(this);
		}
	}
}
