using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using DotLiquid;
using Microservices.Core;
using Microservices.LiquidEngine;
namespace Microservices.HostService.Microservices
{
	public class HostMicroservice
	{
		public HtmlMessage Index(IMessageHandlersHost host, ICookies cookies)
		{
			var handlers = Handlers(host);
			cookies.Append("mycookie","myvalue");
			return this.Liquid("Index",new { host, handlers });
		}

		public MessageHandlerInfo[] Handlers(IMessageHandlersHost host)
		{
			return host.MessageHandlers.Select(h => new MessageHandlerInfo() {CatchPattern = h.Name}).ToArray();
		}
	}
}
