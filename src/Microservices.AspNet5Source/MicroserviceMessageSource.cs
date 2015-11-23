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
		private class HttpMiddlewareMessageContext : IMessageContext, IMessageResponse, IMessageRequest
		{
			private const string MicroserviceNameKey = "microservice";
			private const string DefaultMicroserviceName = "host";

			private const string MicroserviceMethodNameKey = "method";
			private const string DefaultMicroserviceMethodName = "index";

			private readonly RouteContext _routeContext;
			public IMessageResponse Response { get; }
			public IMessageRequest Request { get; }
			public MessageSource Source { get; }

			public string MicroserviceName { get; }
			public string MicroserviceMethod { get; }

			public HttpMiddlewareMessageContext(MessageSource source, RouteContext routeContext)
			{
				_routeContext = routeContext;
				Response = this;
				Request = this;
				Source = source;

				MicroserviceName = routeContext.RouteData.Values[MicroserviceNameKey]?.ToString() ?? DefaultMicroserviceName;
				MicroserviceMethod = routeContext.RouteData.Values[MicroserviceMethodNameKey]?.ToString() ?? DefaultMicroserviceMethodName;

			}

			public Task WriteString(string str)
			{
				return _routeContext.HttpContext.Response.WriteAsync(str);
			}
		}

		public HttpMicroserviceMessageSource(IMessageDestination dst)
			: base(dst)
		{

		}

		public async Task RouteAsync(RouteContext context)
		{
			var message = new HttpMiddlewareMessageContext(this, context);

			await Process(message);
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			return null;
		}

		private async Task<string> ReadBody(HttpContext context)
		{
			if (!context.Request.ContentLength.HasValue)
				return string.Empty;

			var data = new byte[context.Request.ContentLength.Value];
			var done = 0;

			while (done < data.Length)
				done += await context.Request.Body.ReadAsync(data, done, data.Length - done);

			return Encoding.UTF8.GetString(data);
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

		public static void AddMicroservices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<MicroservicesOptions>(configuration.GetSection("Microservices"));
			services.AddSingleton<IMessageDestination, MicroservicesDispatcher>();
			services.AddSingleton<IMicroservicesLocator, DefaultMicroservicesLocator>();
			
		}
	}
}