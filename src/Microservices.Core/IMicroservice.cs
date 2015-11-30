using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public enum ParameterType
	{
		String,
		Integer,
		Float,
		Object,
		Array,
		Boolean,
	}

	public interface IMessageSchema
	{
		string Name { get; }
		ParameterType Type { get; }
		IDictionary<string, IMessageSchema> Parameters { get; set; }
	}
	
	public interface IMessageHandler
	{
		string CatchPattern { get; }
		IMessageSchema Message { get; }
		IMessageSchema Response { get; }
		Task<IMessage> Handle(IMessage message);
	}
}