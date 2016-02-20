using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microhandlers.Core.Exceptions
{
    public class MicroservicesCoreException: Exception
    {
        public MicroservicesCoreException(string msg) : base(msg)
        {
            
        }
    }
}
