using System;
using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMessageHandlersHost
    {
	    Task Initialize();
		Task<IMessage> Handle(IMessage message);
		void Register(IMessageHandler handler);
		void Unregister(string name);
		void AddDependency<T>(T implementation);
		object ResolveDependency(Type type);
	}
}