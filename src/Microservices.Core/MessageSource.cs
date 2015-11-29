using System.Threading.Tasks;

namespace Microservices.Core
{
	public interface IMessageSource
	{
		IMicroservicesHost Host { get; }
	}
    public abstract class MessageSource: IMessageSource
	{
        public IMicroservicesHost Host { get; }

        protected MessageSource(IMicroservicesHost host)
        {
            Host = host;
        }

        protected async Task Process(IMessageContext messageContext)
        {
            await Host.Process(messageContext);
        }
    }
}