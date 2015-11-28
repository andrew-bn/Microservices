using System.Threading.Tasks;

namespace Microservices.Core
{
    public abstract class MessageSource
    {
        protected IMicroservicesDispatcher Dispatcher { get; }

        protected MessageSource(IMicroservicesDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        protected async Task Process(IMessageContext messageContext)
        {
            await Dispatcher.Process(messageContext);
        }
    }
}