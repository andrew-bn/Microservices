using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microhandlers.Core.Infrastructure
{
    public interface IServicesContainer
    {
	    bool TryToResolve(Type type, out object service);
    }
}
