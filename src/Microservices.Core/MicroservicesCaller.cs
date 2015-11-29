using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public interface IMicroservicesCaller : IMessageSource
	{
		dynamic Service { get; }
	}

	public class MicroservicesCaller: DynamicObject, IMicroservicesCaller
	{
		public dynamic Service => this;
		public IMicroservicesHost Host { get; }

	    public MicroservicesCaller(IMicroservicesHost host)
	    {
		    Host = host;
	    }

		
	}
}
