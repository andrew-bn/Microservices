using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core;

namespace Microservices.HostService.Microservices
{
    public class HostMicroservice
    {
		private IMessageHandlersHost _host;
		public HostMicroservice(IMessageHandlersHost host)
		{
			_host = host;
		}
		public event EventHandler<int> MyEvent;

	    public async Task<int> Index(int param1)
	    {
			//await _host.DynamicProxy.Namecheap.Domain.Dns.Add(type:"MX",host:"@",data:"example.com",priority:23);
			//await _host.DynamicProxy.Namecheap.Domain.Dns.RecordAdded.Subscribe((EventHandler<int>)OnRecordAdded);
			//OnMyEvent(param1);
		    return param1;
	    }
		private async void OnRecordAdded(object sender, int id)
		{

		}
	    protected virtual void OnMyEvent(int obj)
	    {
		    MyEvent?.Invoke(this, obj);
	    }
    }
}
