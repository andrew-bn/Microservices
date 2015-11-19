using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microservices.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Host
{
    public class AspNet5MicroserviceMessageSource : MessageSource
    {
        private class AspNet5MiddlewareMessageContext : MessageContext, IMessageResponse
        {
            private readonly HttpContext _httpContext;
            public override sealed IMessageResponse Response { get; protected set; }
            public override sealed MessageSource Source { get; protected set; }

            public AspNet5MiddlewareMessageContext(MessageSource source, HttpContext httpContext)
            {
                _httpContext = httpContext;
                Response = this;
                Source = source;
            }


            public Task WriteString(string str)
            {
                return _httpContext.Response.WriteAsync(str);
            }
        }

        private readonly RequestDelegate _next;

        public AspNet5MicroserviceMessageSource(IMessageDestination dst, RequestDelegate next)
			:base(dst)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var body = await ReadBody(context);
            var message = new AspNet5MiddlewareMessageContext(this, context);

            await Process(message);
        }

        public async Task<string> ReadBody(HttpContext context)
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