using Microservices.Core;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Host
{
	public class Startup
	{
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMicroservices();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMicroservices();
		}
	}
}
