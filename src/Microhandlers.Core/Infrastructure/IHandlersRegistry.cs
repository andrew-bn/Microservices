using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Objects;

namespace Microhandlers.Core.Infrastructure
{
    public interface IHandlersRegistry
    {

        IMessageHandler First(MessageName name);
    }
}
