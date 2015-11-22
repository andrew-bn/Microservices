using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMessageDestination
    {
	    void Initialize();
        Task Process(IMessageContext messageContext);
    }
}