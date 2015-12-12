using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public interface IMessageTypeSchema
	{
		ParameterType Type { get; }
		IEnumerable<IMessageParameterSchema> Parameters { get; }
	}

	public interface IMessageParameterSchema: IMessageTypeSchema
	{
		string ParameterName { get; }
	}

	public interface IMessageSchema: IMessageTypeSchema
	{
		MessageName Name { get; }
	}
}
