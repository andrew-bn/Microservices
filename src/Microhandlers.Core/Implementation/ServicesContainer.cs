using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Infrastructure;

namespace Microhandlers.Core.Implementation
{
    public class ServicesContainer: IServicesContainer
    {
        private readonly IServiceProvider _serviceProvider;

        public ServicesContainer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
