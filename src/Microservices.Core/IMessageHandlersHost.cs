using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMessageHandlersHost
    {
		string Name { get; }
		string Version { get; }
	    Task Initialize();
		IEnumerable<IMessageHandler> MessageHandlers { get; }
		Task<IMessage> Handle(IMessage message);
		void Register(IMessageHandler handler);
		void Unregister(string name);
		void AddDependency<T>(T implementation);
		object ResolveDependency(Type type);
	}
}