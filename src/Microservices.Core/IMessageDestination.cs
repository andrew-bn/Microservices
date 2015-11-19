using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMessageDestination
    {
        Task Process(MessageContext messageContext);
    }
}