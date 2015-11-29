using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
    public interface IMicroservicesFactory
    {
		List<IMicroservice> LocateMicroservices(IMicroservicesHost microservicesHost);
    }
}
