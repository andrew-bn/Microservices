using System.Threading.Tasks;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Message;
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
	        var msg = (IMessage) null;
	        var handler = _registry.First(msg.Name);
	        var result = await handler.Handle(msg, _servicesContainer);
	        //var message = new HttpJsonMessage(context);
	        //await message.Prepare();

	        //var result = await Handle(message);
	        //await context.HttpContext.Response.WriteAsync(result.ToResponseString());
	    }

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			return null;
		}
	}
}