using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microservices.Core;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.AspNet5Source
{
	
	public class HttpMicroserviceMessageSource : MessageSource, IRouter
	{
		
		public HttpMicroserviceMessageSource(IMessageDestination dst)
			: base(dst)
		{

		}

		public async Task RouteAsync(RouteContext context)
		{
			var message = new HttpMiddlewareMessageContext(this, context);
			await message.Prepare();
			await Process(message);
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			return null;
		}
	}

	public static class MicroservicesMiddleware
	{
		public static void UseMicroservices(this IApplicationBuilder appbilder)
		{
			UseMicroservices(appbilder, routes =>
			{
				routes.MapRoute(
				"default",
				"{microservice}/{method}/{id}",
				new { microservice = "host", method = "index", id = "" });
			});
		}

		public static IApplicationBuilder UseMicroservices(this IApplicationBuilder app, Action<IRouteBuilder> configureRoutes)
		{
			var host = app.ApplicationServices.GetService<IMessageDestination>();
			host.Initialize();

			var routes = new RouteBuilder
			{
				DefaultHandler = new HttpMicroserviceMessageSource(host),
				ServiceProvider = app.ApplicationServices,
			};

			configureRoutes(routes);
			
			return app.UseRouter(routes.Build());
		}

		public static void AddMicroservices(this IServiceCollection services)
		{
			var builder = new ConfigurationBuilder().AddJsonFile("hosting.json");

			var configuration = builder.Build();

			services.Configure<MicroservicesOptions>(configuration.GetSection("Microservices"));
			services.AddSingleton<IMessageDestination, MicroservicesDispatcher>();
			services.AddSingleton<IMicroservicesLocator, DefaultMicroservicesLocator>();
			
		}
	}
}