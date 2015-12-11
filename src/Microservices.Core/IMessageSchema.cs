using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core.Messaging
{
	public interface IMessageValueSchema
	{
		ParameterType Type { get; }
		IEnumerable<IMessageParameterSchema> Parameters { get; }
	}

	public interface IMessageParameterSchema: IMessageValueSchema
	{
		string ParameterName { get; }
	}

	public interface IMessageSchema: IMessageValueSchema
	{
		MessageName Name { get; }
	}
}
