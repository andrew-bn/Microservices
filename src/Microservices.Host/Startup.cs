using Microservices.AspNet5Source;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microservices.Host
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile("config.json")
				.AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);
			
			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
            services.AddOptions();
			services.AddRouting();

			services.AddMicroservices(Configuration);
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMicroservices();
		}
	}
}
