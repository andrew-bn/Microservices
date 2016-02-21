using System.Threading.Tasks;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Message;
using Microhandlers.Sources.AspNet5;
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
	        var msg = new Message(name);
	        var handler = _registry.First(msg.Name);
	        var result = await handler.Handle(msg, _servicesContainer);
	    }

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			return null;
		}
	}
}