using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
	public interface IMessageHandler
	{
		//string Name { get; }
		IMessageSchema Message { get; }
		IMessageSchema Response { get; }
		Task<IMessage> Handle(IMessageHandlersHost host, IMessage message, IHandlersSequence sequence);
	}
}