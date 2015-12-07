using Microservices.Core;
using Microservices.LiquidEngine;
namespace Microservices.HostService.Microservices
{
	public class HostMicroservice
	{
		public HtmlMessage Index(IMessageHandlersHost host, ICookies cookies)
		{
			return this.Liquid("Index",new { host, handlers = host.HandlersTree });
		}
	}
}
