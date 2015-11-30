using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMessageHandlersHost
    {
		Task<IMessage> Handle(IMessage message);
		Task Register(IMessageHandler handler);
		Task Unregister(string name);
    }
}