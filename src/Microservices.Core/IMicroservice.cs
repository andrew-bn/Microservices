using System.Threading.Tasks;

namespace Microservices.Core
{
	
	public interface IMicroservice
	{
		object Instance { get; }
		string Name { get; }

		Task Invoke(string method, IMessageContext messageContext);
	}
}