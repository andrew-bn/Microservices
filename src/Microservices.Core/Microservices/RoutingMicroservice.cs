using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core.Microservices
{
    public class RoutingMicroservice
    {
		[Initializer]
	    public async Task Initialize(IMicroservicesHost host, IMicroservice microservice)
		{
			host.DefaultMicroservice = microservice;
		}

		[CatchAll]
	    public async Task Route(IMessageContext context)
	    {
		    
	    }
    }
}
