using System;
using System.Collections.Generic;
using Microhandlers.Core.Implementation;
using Microhandlers.Core.Infrastructure;
using Microhandlers.MessageSources.AspNet5;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microhandlers.Hosts.AspNet5
{
    public static class Middleware
    {
        public static void UseMicrohand(this IApplicationBuilder appbilder, params Func<IEnumerable<IMessageHandler>>[] handlers)
        {
            UseMicrohand(appbilder, routes =>
            {
                routes.MapRoute(
                "default",
                "{microservice}/{method}/{id}",
                new { microservice = "host", method = "index", id = "" });
            }, handlers);
        }

        public static IApplicationBuilder UseMicrohand(this IApplicationBuilder app, Action<IRouteBuilder> configureRoutes, params Func<IEnumerable<IMessageHandler>>[] handlers)
        {
            var container = app.ApplicationServices.GetService<IServicesContainer>();
            var registry = app.ApplicationServices.GetService<IHandlersRegistry>();

            var routes = new RouteBuilder
            {
                DefaultHandler = new MicrohandlersRouter(registry, container),
                ServiceProvider = app.ApplicationServices,
            };

            configureRoutes(routes);

            foreach (var reg in handlers)
                foreach (var h in reg())
                    registry.Register(h);

            return app.UseRouter(routes.Build());
        }

        public static void AddMicrohand(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHandlersRegistry, HandlersRegistry>();
            services.AddSingleton<IServicesContainer, ServicesContainer>();
        }
    }
}
