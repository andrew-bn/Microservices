using System;

namespace Microservices.Core
{
	public enum MessageObjectType
	{
		Null,
		String,
		Integer,
		Float,
		Object,
		Array,
		Boolean,
	}
	public interface IMessageObject
	{
		MessageObjectType ObjectType { get; }
		IMessageObject this[string parameterName] { get; }
		IMessageObject this[int parameterIndex] { get; }
		object Value { get;}
		object ValueAs(Type type);
	}

    public interface IMessage: IMessageObject
	{
        string Microservice { get; }
        string MessageName { get; }
	}
}