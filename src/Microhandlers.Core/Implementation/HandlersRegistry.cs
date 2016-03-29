using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Exceptions;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Objects;

namespace Microhandlers.Core.Implementation
{
    public class HandlersRegistry: IHandlersRegistry
    {
        private readonly ConcurrentDictionary<MessageName, IMessageHandler> _handlers = new ConcurrentDictionary<MessageName, IMessageHandler>();

        public IMessageHandler First(MessageName name)
        {
	        string search = "";
	        foreach (var p in name.Parts)
	        {
				if (search.Length == 0)
					search += p;
				else search += "." + p;

				IMessageHandler result;
		        if (_handlers.TryGetValue(search, out result))
			        return result;
	        }

			throw new MicroservicesCoreException($"Handler '{name}' not found");
        }

        public void Register(IMessageHandler messageHandler)
        {
            if (!_handlers.TryAdd(messageHandler.Name, messageHandler))
                throw new MicroservicesCoreException($"Handler '{messageHandler.Name}' already exists");
        }
    }
}
