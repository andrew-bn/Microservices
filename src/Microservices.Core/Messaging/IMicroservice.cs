using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Core.Messaging;

namespace Microservices.Core
{


	
	public interface IMessageHandler
	{
		string CatchPattern { get; }
		IMessageSchema Message { get; }
		IMessageSchema Response { get; }
		Task<IMessage> Handle(IMessage message);
	}

	public interface IMessage : IMessageSchema
	{
		IMessage this[string parameterName] { get; }
		object Value { get; }
		object ValueAs(Type type);
	}
}