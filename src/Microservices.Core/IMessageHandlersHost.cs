using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMessageHandlersHost: IMessageHandler
	{
		string HostName { get; }
		string Version { get; }
	    Task Initialize();
		void AddDependency<T>(T implementation);
		object ResolveDependency(Type type);
	}
}