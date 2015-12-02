using System.IO;
using System.Threading.Tasks;
using Microservices.Core;
using Microservices.Core.Messaging;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Newtonsoft.Json;

namespace Microservices.AspNet5Source
{
	public class HttpMicroserviceMessageSource : MessageSource, IRouter
	{
		public HttpMicroserviceMessageSource(IMessageHandlersHost host)
			: base(host)
		{
		}

		public async Task RouteAsync(RouteContext context)
		{
			var message = new HttpJsonMessage(context);
			await message.Prepare();

			var result = await Handle(message);

			if (result is ObjectBasedMessage)
			{
				var sw = new StringWriter();
				JsonSerializer.Create().Serialize(sw, ((ObjectBasedMessage)result).UnderlyingObject);
				await context.HttpContext.Response.WriteAsync(sw.ToString());
			}
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			return null;
		}
	}
}