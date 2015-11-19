using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microservices.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Host
{
    public class AspNet5MicroserviceMessageSource : MessageSource
    {
        private readonly RequestDelegate _next;

        public AspNet5MicroserviceMessageSource(IMessageDestination dst, RequestDelegate next)
			:base(dst)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
			//context.Request.ReadFormAsync().ContinueWithStandard(null);
			await context.Response.WriteAsync("hello!");
        }
    }

    public static class AspNet5MicroservicesMessageSourceMiddleware
    {
        public static void UseMicroservices(this IApplicationBuilder appbilder)
        {
            appbilder.UseMiddleware<AspNet5MicroserviceMessageSource>();
        }

		public static void AddMicroservices(this IServiceCollection services)
		{
			services.AddSingleton<IMessageDestination, MicroservicesHost>();
		}

	}
}