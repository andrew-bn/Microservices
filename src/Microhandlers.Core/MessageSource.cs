using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Message;

namespace Microhandlers.Core
{
    public abstract class MessageSource
    {
        private readonly IHandlersRegistry _registry;
        private readonly IServicesContainer _servicesContainer;

        protected MessageSource(IHandlersRegistry registry, IServicesContainer servicesContainer)
        {
            _registry = registry;
            _servicesContainer = servicesContainer;
        }

        protected Task<IMessage> Handle(IMessage message)
        {
            var handler = _registry.First(message.Name);
            return handler.Handle(message, _servicesContainer);
        }
    }
}
