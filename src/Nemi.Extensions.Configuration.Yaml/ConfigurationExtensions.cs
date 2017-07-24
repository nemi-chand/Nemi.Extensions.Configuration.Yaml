using Microsoft.Extensions.Configuration;
using System;

namespace Nemi.Extensions.Configuration.Yaml
{
	public static class ConfigurationExtensions
	{
		/// <summary>
		/// Adds a new configuration source.
		/// </summary>
		/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
		/// <param name="configureSource">Configures the source secrets.</param>
		/// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
		public static IConfigurationBuilder Add<TSource>(this IConfigurationBuilder builder, Action<TSource> configureSource) where TSource : IConfigurationSource, new()
		{
			var source = new TSource();
			configureSource?.Invoke(source);
			return builder.Add(source);
		}
	}
}
