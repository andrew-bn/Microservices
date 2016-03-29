using System.Text;
using System.Threading.Tasks;
using Microhandlers.Core;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Message;
using Microhandlers.Protocol.Json;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace Microhandlers.MessageSources.AspNet5
{
	public class MicrohandlersRouter : MessageSource, IRouter
	{
		private IProtocolStringSerializer _protocolStringSerializer = new MicroJsonSerializer();
		public MicrohandlersRouter(IHandlersRegistry registry, IServicesContainer servicesContainer)
			: base(registry, servicesContainer)
		{
		}

		public async Task RouteAsync(RouteContext context)
		{
			var version = $"v{context.RouteData.Values["apiversion"] ?? "0"}";

			var name = context.RouteData.Values["messagename"]?.ToString().Replace("/", ".");

			var body = await ReadBody(context.HttpContext);
			var msgItem = _protocolStringSerializer.Deserialize(body);

			var msg = new Message(name, msgItem);
			var result = await Handle(msg);

			var output = _protocolStringSerializer.SerializeToString(result);

			await context.HttpContext.Response.WriteAsync(output);
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			return null;
		}

		private static async Task<string> ReadBody(HttpContext context)
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
}