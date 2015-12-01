using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core.Messaging
{
	public interface IMessageSchema
	{
		string Name { get; }
		ParameterType Type { get; }
		IDictionary<string, IMessageSchema> Parameters { get; set; }
	}
}
