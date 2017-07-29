using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nemi.Extensions.Configuration.Yaml.Test
{
    public class YamlConfigurationExtensionTest
    {
		[Fact]
		public void file_not_found()
		{
			var configurationBuilder = new ConfigurationBuilder();
			string path = "file_not_found.yaml";
			var ex = Assert.Throws<ArgumentException>(() => new ConfigurationBuilder().AddYamlFile(path).Build());
			Assert.Equal("path", ex.ParamName);
		}
    }
}
