using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMicroservicesHost
    {
	    void Initialize();
		IMicroservice DefaultMicroservice { get; set; }
		dynamic DynamicProxy { get; }
		Task<IMessage> Handle(IMessage message);
    }
}