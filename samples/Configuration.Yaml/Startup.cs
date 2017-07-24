using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Configuration.Yaml
{
    public class Startup
    {
		public Startup(IHostingEnvironment env)
		{
			var builder =new ConfigurationBuilder();
			builder.SetBasePath(env.ContentRootPath);
			builder.AddYamlFile("config.yaml",optional: false);
			//builder.AddJsonFile("appsettings.json", optional: false);

			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
				context.Response.ContentType = "text/plain";
				await DumpConfig(context.Response, Configuration);
				//await context.Response.WriteAsync("Hello World!");
            });
        }

		private static async Task DumpConfig(HttpResponse response, IConfiguration config, string indentation = "")
		{
			foreach (var child in config.GetChildren())
			{
				await response.WriteAsync(indentation + "[" + child.Key + "] " + config[child.Key] + "\r\n");
				await DumpConfig(response, child, indentation + "  ");
			}
		}
	}
}
