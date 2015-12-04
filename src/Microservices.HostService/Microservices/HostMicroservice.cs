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
		public HtmlMessage Index(IMessageHandlersHost host)
		{
			var handlers = Handlers(host);
			return this.Content("index",new { host, handlers });
		}

		public MessageHandlerInfo[] Handlers(IMessageHandlersHost host)
		{
			return host.MessageHandlers.Select(h => new MessageHandlerInfo() {CatchPattern = h.CatchPattern}).ToArray();
		}
	}
}
