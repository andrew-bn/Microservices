using System;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public interface IMicroserviceParameter
	{
		string Name { get; }
		Type Type { get; }
	}

	public interface IMicroserviceEvent
	{
		string Name { get; }
		Type EventArgs { get; }
	}
	public interface IMessageHandler
	{
		string Name { get; }
		IMicroserviceParameter[] Parameters { get; }
		
	}
	public interface IMicroservice
	{
		object Instance { get; }
		string Name { get; }
		IMessageHandler[] Handlers { get; }
		IMessageHandler CatchAll { get; }
		IMessageHandler Initializer { get; }
		IMicroserviceEvent[] Events { get; }
		Task<IMessage> Invoke(IMessage message);
	}
}