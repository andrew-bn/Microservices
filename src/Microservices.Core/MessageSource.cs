using System.Threading.Tasks;

namespace Microservices.Core
{
    public abstract class MessageSource
    {
        private readonly IMessageDestination _destination;

        protected MessageSource(IMessageDestination destination)
        {
            _destination = destination;
        }

        protected async Task Process(IMessageContext messageContext)
        {
            await _destination.Process(messageContext);
        }
    }
}