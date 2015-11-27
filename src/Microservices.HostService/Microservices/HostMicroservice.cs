using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core;

namespace Microservices.HostService.Microservices
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class HostMicroservice
    {
        public HostMicroservice()
        {
        }
		
	    public async Task Index(IMessageContext context)
	    {
			context.Request.ReadParameter<dynamic>(new RequestParameter(0, "param2"));
			await context.Response.WriteString("Hello from HostMicroservice!");
		}
    }
}
