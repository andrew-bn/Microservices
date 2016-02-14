using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Implementation;
using Microhandlers.Core.Infrastructure;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microhandlers.Sources.AspNet5
{
    public static class Middleware
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
            //var host = app.ApplicationServices.GetService<IMessageHandlersHost>();
            //var messageHandlerProviders = app.ApplicationServices.GetServices<IMessageHandlersProvider>();

            //foreach (var provider in messageHandlerProviders)
            //    provider.ProvideMessageHandlers();

            var routes = new RouteBuilder
            {
                DefaultHandler = new MicrohandlersRouter(),
                ServiceProvider = app.ApplicationServices,
            };

            configureRoutes(routes);

            return app.UseRouter(routes.Build());
        }

        public static void AddMicroservices(this IServiceCollection services, IConfiguration configuration)
        {
     

            //services.Configure<MicroservicesOptions>(configuration.GetSection("Microservices"));
           // services.AddSingleton<IHandlersRegistry, HandlersRegistry>();

        }
    }
}
