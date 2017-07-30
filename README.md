# Nemi.Extensions.Configuration.Yaml
YAML extension for ASP.NET Core Configuration

Nuget Package :  https://www.nuget.org/packages/Nemi.Extensions.Configuration.Yaml
```csharp
Install-Package Nemi.Extensions.Configuration.Yaml
```
.NET CLI
```csharp
dotnet add package Nemi.Extensions.Configuration.Yaml
```

## Usages

add AddYamlFile method in startup class

```csharp
public Startup(IHostingEnvironment env)
		{
			var builder =new ConfigurationBuilder();
			builder.SetBasePath(env.ContentRootPath);
			builder.AddYamlFile("config.yaml",optional: false);

			Configuration = builder.Build();
		}
```
