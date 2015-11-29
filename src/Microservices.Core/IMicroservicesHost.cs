using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMicroservicesHost
    {
	    void Initialize();
		IMicroservice DefaultMicroservice { get; set; }
		IMicroservicesCaller MicroservicesCaller { get; }
		Task Process(IMessageContext messageContext);
    }
}