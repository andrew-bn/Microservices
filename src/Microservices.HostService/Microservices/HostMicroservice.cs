using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core;

namespace Microservices.HostService.Microservices
{
    public class HostMicroservice
    {
	    public async Task<int> Index(int param1)
	    {
		    return param1;
	    }
    }
}
