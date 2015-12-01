using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core.Messaging;
using Microsoft.AspNet.Routing;

namespace Microservices.AspNet5Source
{
    public class RoutingBasedMessageSchema: IMessageSchema
    {
	    private readonly RouteContext _routContext;

	    public RoutingBasedMessageSchema(RouteContext routContext)
	    {
		    _routContext = routContext;
	    }

	    public string Name { get; }
	    public ParameterType Type { get; }
	    public IEnumerable<IMessageSchema> Parameters { get; }
    }
}
