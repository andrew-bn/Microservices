using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.AspNet5Source;
using Microservices.RabbitMQEventHandler;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.AspNetHosting
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				
				.AddJsonFile("hosting.json")
				.AddJsonFile($"hosting.{env.EnvironmentName}.json", optional: true);
			
			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
            services.AddOptions();
			services.AddRouting();

			services.AddMicroservices();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMicroservices();
		}
    }
}
