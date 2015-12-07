using System;
using System.Collections.Generic;
using Microservices.Core;
using Microservices.LiquidEngine;
namespace Microservices.HostService.Microservices
{

	public class HostMicroservice
	{
		public HtmlMessage Index(IMessageHandlersHost host, ICookies cookies)
		{
			var handlers = new List<IHandlersTreeNode>();
			CollectHandlers(host.HandlersTree, handlers);
			return this.Liquid("Index",new { host, handlers = handlers.ToArray() });
		}

		private void CollectHandlers(IHandlersTreeNode node, List<IHandlersTreeNode> handlers)
		{
			if (!node.HasSubHandlers)
				return;
			foreach (var n in node.SubHandlers)
			{
				if (n.Handler!=null)
					handlers.Add(n);
				CollectHandlers(n,handlers);
			}
		}

	}
}
