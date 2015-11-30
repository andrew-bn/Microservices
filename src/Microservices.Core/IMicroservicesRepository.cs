using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMicroservicesRepository
    {
	    void Register(IMessageHandlersHost host, IMicroservice microservice);
    }
}
