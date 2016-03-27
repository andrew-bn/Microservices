using System.Text;
using System.Threading.Tasks;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Message;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace Microhandlers.MessageSources.AspNet5
{
	public class MicrohandlersRouter : IRouter
	{
	    private readonly IHandlersRegistry _registry;
	    private readonly IServicesContainer _servicesContainer;

	    public MicrohandlersRouter(IHandlersRegistry registry, IServicesContainer servicesContainer)
	    {
	        _registry = registry;
	        _servicesContainer = servicesContainer;
	    }

	    public async Task RouteAsync(RouteContext context)
	    {
	        var version = $"v{context.RouteData.Values["apiversion"] ??"0"}";
	        var name = context.RouteData.Values["messagename"]?.ToString().Replace("/",".");
	        var body = await ReadBody(context.HttpContext);

	        var msg = new Message(name);
	        
	        var handler = _registry.First(msg.Name);

	        var result = await handler.Handle(msg, null, _servicesContainer);
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