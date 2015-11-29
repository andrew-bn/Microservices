using System.Threading.Tasks;
using Microservices.Core;
using Microsoft.AspNet.Routing;

namespace Microservices.AspNet5Source
{
	public class HttpMicroserviceMessageSource : MessageSource, IRouter
	{
		public HttpMicroserviceMessageSource(IMicroservicesHost host)
			: base(host)
		{
		}

		public async Task RouteAsync(RouteContext context)
		{
			var message = new HttpMiddlewareMessageContext(Host, this, context);
			await message.Prepare();
			await Process(message);
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			return null;
		}
	}
}