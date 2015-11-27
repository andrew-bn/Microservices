using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMessageResponse
    {
	    Task WriteResult(object result);
    }
}