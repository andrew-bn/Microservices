using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMessageHandlersHost : IMessageHandler
	{
		string HostName { get; }
		string Version { get; }
		void AddDependency<T>(T implementation);
		object ResolveDependency(Type type);
		void Register(string messageName, IMessageHandler handler);
		Task<IMessage> Handle(IMessage message);
	}
}