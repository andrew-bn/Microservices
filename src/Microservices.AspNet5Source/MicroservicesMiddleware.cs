using System;
using System.Text;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microservices.Core;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.AspNet5Source
{
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
			var host = app.ApplicationServices.GetService<IMessageHandlersHost>();
			var messageHandlerProviders = app.ApplicationServices.GetServices<IMessageHandlersProvider>();

			foreach(var provider in messageHandlerProviders)
				provider.ProvideMessageHandlers();

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
			services.AddSingleton<IMessageHandlersHost, MicroservicesHost>();
			services.AddSingleton<IMessageHandlersProvider, MicroservicesProvider>();
		}
	}
}