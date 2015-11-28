using System.Threading.Tasks;
using Microservices.Core;
using Microsoft.AspNet.Routing;

namespace Microservices.AspNet5Source
{
	public class HttpMicroserviceMessageSource : MessageSource, IRouter
	{
		public HttpMicroserviceMessageSource(IMicroservicesDispatcher dispatcher)
			: base(dispatcher)
		{
		}

		public async Task RouteAsync(RouteContext context)
		{
			var message = new HttpMiddlewareMessageContext(Dispatcher, this, context);
			await message.Prepare();
			await Process(message);
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			return null;
		}
	}
}