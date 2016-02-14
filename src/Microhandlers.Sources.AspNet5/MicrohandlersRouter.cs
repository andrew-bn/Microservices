using System.Threading.Tasks;
using Microsoft.AspNet.Routing;

namespace Microhandlers.Sources.AspNet5
{
	public class MicrohandlersRouter : IRouter
	{
		public MicrohandlersRouter()
		{
		}

		public async Task RouteAsync(RouteContext context)
		{
		   
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