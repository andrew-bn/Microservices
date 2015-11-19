using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
    public class MicroservicesHost: IMessageDestination
    {
		public async Task Process(MessageContext messageContext)
		{
		    await messageContext.Response.WriteString("hello!");
		}
	}
}
