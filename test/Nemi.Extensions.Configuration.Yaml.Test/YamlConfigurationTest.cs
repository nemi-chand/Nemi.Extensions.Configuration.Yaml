using System;
using Xunit;
using Nemi.Extensions.Configuration.Yaml;
using System.IO;

namespace Nemi.Extensions.Configuration.Yaml.Test
{
    public class YamlConfigurationTest
	{
        [Fact]
        public void YamlParser()
        {	
			YamlConfigurationFileParser parser = new YamlConfigurationFileParser();
			string includeScope = parser.Parse(GetYamlStream())["logging:includeScopes"];

			//Assert.
			Assert.Equal("false", includeScope);

		}

		[Fact]
		public void YamlProviderTest()
		{
			YamlConfigurationProvider yamlProvider = LoadProviders();
			string loglevel = string.Empty;
			yamlProvider.TryGet("logging:logLevel:default", out loglevel);
			
			Assert.Equal("Warning", loglevel);

		}

		private YamlConfigurationProvider LoadProviders()
		{
			var provider = new YamlConfigurationProvider(new YamlConfigurationSource() { Optional = false });
			provider.Load(GetYamlStream());
			return provider;
		}


		private Stream GetYamlStream()
		{
			string yamlString = @"connectionStrings:
    defaultConnection: Server=(localdb)\\mssqllocaldb;Database=aspnet-CookieManagerTest-c32569c5-4c52-46fa-ab26-c4f4c4280f8a;Trusted_Connection=True;MultipleActiveResultSets=true
logging:
    includeScopes: false
    logLevel:
      default: Warning";

			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(yamlString);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
    }
}
