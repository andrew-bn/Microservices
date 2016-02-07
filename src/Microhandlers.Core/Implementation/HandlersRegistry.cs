using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Objects;

namespace Microhandlers.Core.Implementation
{
    public class HandlersRegistry: IHandlersRegistry
    {
        private ConcurrentDictionary<MessageName, IMessageHandler> _handlers = new ConcurrentDictionary<MessageName, IMessageHandler>();
        public IMessageHandler First(MessageName name)
        {
            throw new NotImplementedException();
        }
    }
}
