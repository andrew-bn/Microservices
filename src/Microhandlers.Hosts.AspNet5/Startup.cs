using Microhandlers.HandlersDiscovery.Microservice;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microhandlers.MessageSources.AspNet5;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microhandlers.Hosts.AspNet5
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
			services.AddMicrohand(Configuration);
		}

		public void Configure(IApplicationBuilder app)
		{
		    var md = new MicroserviceDiscoverer();
		    var libManager = app.ApplicationServices.GetService<ILibraryManager>();

            app.UseMicrohand(()=> md.Discover(libManager));
		}
	}
}
