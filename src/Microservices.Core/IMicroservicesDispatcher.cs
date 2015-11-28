using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMicroservicesDispatcher
    {
	    void Initialize();
        Task Process(IMessageContext messageContext);
    }
}