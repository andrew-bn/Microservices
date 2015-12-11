using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
	public interface IMessageHandler
	{
		IMessageValueSchema Message { get; }
		IMessageValueSchema Response { get; }
		Task<IMessage> Handle(IMessageHandlersHost host, IMessage message, IHandlersQueue sequence);
	}
}