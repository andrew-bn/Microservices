using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Message;
using Microhandlers.Core.Objects;

namespace Microhandlers.Core.Infrastructure
{
    public interface IMessageHandler
    {
        MessageName Name { get; }
        Task<IMessage> Handle(IMessage message, IServicesContainer servicesContainer);
    }
}
