using System;
using Nemi.Extensions.Configuration.Yaml;
using Microsoft.Extensions.FileProviders;

namespace Microsoft.Extensions.Configuration
{
	/// <summary>
	/// Extension method for adding <see cref="YamlConfigurationProvider"/>
	/// </summary>
	public static class YamlConfigurationExtensions
    {
		/// <summary>
		/// Adds the YAML configuration provider at <paramref name="path"/> to <paramref name="builder"/>
		/// </summary>
		/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add.</param>
		/// <param name="path">relative path</param>
		/// <returns>The <see cref="IConfigurationBuilder"/></returns>
		public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder,string path)
		{
			return AddYamlFile(builder,provider: null,path: path,optional: false,reloadOnChange: false);
		}

		/// <summary>
		/// Adds the YAML configuration provider at <paramref name="path"/> to <paramref name="builder"/>
		/// </summary>
		/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add</param>
		/// <param name="path">relative path</param>
		/// <param name="optional">file is optional or not</param>
		/// <returns>The <see cref="IConfigurationBuilder"/></returns>
		public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional)
		{
			return AddYamlFile(builder, provider: null, path: path, optional: optional, reloadOnChange: false);
		}

		/// <summary>
		/// Adds the YAML configuration provider at <paramref name="path"/> to <paramref name="builder"/>
		/// </summary>
		/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add</param>
		/// <param name="path">relative path</param>
		/// <param name="optional">file is optional or not</param>
		/// <param name="reloadOnChange">reload the settings on change</param>
		/// <returns>The <see cref="IConfigurationBuilder"/></returns>
		public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
		{
			return AddYamlFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
		}

		/// <summary>
		/// Adds the YAML configuration provider at <paramref name="path"/> to <paramref name="builder"/>
		/// </summary>
		/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add</param>
		/// <param name="provider">The <see cref="IFileProvider"/> to add</param>
		/// <param name="path">relative path</param>
		/// <param name="optional">file is optional</param>
		/// <param name="reloadOnChange">reload the settings on change</param>
		/// <returns>The <see cref="IConfigurationBuilder"/></returns>
		public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
		{
			if (builder == null)
			{
				throw new ArgumentNullException(nameof(builder));
			}
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException("File path must be a non-empty string.", nameof(path));
			}

			return builder.AddYamlFile(s =>
			{
				s.FileProvider = provider;
				s.Path = path;
				s.Optional = optional;
				s.ReloadOnChange = reloadOnChange;
			});
		}

		/// <summary>
		/// Adds the YAML configuration provider at <paramref name="path"/> to <paramref name="builder"/>
		/// </summary>
		/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add</param>
		/// <param name="configureSource">Configure the source</param>
		/// <returns>The <see cref="IConfigurationBuilder"/></returns>
		public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, Action<YamlConfigurationSource> configureSource)
			=> builder.Add(configureSource);
	}
}
