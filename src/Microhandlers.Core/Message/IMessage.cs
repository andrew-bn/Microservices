using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Objects;

namespace Microhandlers.Core.Message
{
    public interface IMessage: IMessageBody
    {
        MessageName Name { get; }
    }
}
