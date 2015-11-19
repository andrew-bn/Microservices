using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microservices.Core;

namespace Microservices.Host
{
    public class AspNet5MicroserviceMessageSource : IMessageSource
    {
        private readonly RequestDelegate _next;

        public AspNet5MicroserviceMessageSource(RequestDelegate next)
        {
            _next = next;
        }

        public void Activate()
        {
            Message msg;


        }

        public async Task Invoke(HttpContext context)
        {
			await context.Response.WriteAsync("hello!");
        }
    }

    public static class AspNet5MicroservicesMessageSourceMiddleware
    {
        public static void UseMicroservices(this IApplicationBuilder appbilder)
        {
            appbilder.UseMiddleware<AspNet5MicroserviceMessageSource>();
        }
    }
}