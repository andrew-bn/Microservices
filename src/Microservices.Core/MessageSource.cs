using System.Threading.Tasks;

namespace Microservices.Core
{
	public interface IMessageSource
	{
		IMessageHandlersHost Host { get; }
	}
    public abstract class MessageSource: IMessageSource
	{
        public IMessageHandlersHost Host { get; }

        protected MessageSource(IMessageHandlersHost host)
        {
            Host = host;
        }

        protected Task<IMessage> Handle(IMessage message)
        {
            return Host.Handle(Host, message);
        }
    }
}