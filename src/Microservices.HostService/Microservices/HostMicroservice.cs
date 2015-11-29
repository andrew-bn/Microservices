using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core;

namespace Microservices.HostService.Microservices
{
    public class HostMicroservice
    {
	    public event EventHandler<int> MyEvent;

	    public async Task<int> Index(dynamic services, int param1)
	    {
		    await services.Namecheap.Domain.Dns.Add("asdf", 23);
			OnMyEvent(param1);
		    return param1;
	    }

	    protected virtual void OnMyEvent(int obj)
	    {
		    MyEvent?.Invoke(this, obj);
	    }
    }
}
