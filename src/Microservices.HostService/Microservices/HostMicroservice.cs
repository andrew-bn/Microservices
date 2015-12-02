using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core;

namespace Microservices.HostService.Microservices
{
	public class MessageHandlerInfo
	{
		public string CatchPattern { get; set; }
	}
	public class HostMicroservice
	{
		public async Task<string> Index(IMessageHandlersHost host)
		{
			return $"{host.Name}:{host.Version}";
		}
		public async Task<MessageHandlerInfo[]> Handlers(IMessageHandlersHost host)
		{
			return host.MessageHandlers.Select(h => new MessageHandlerInfo() {CatchPattern = h.CatchPattern}).ToArray();
		}
	}
}
