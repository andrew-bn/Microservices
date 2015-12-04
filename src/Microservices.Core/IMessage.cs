using System;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
	public interface IMessage : IMessageSchema
	{
		IMessage this[string parameterName] { get; }
		object Value { get; }
		object ValueAs(Type type);
		string ToResponseString();
	}
}