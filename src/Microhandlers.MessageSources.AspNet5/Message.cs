using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Message;
using Microhandlers.Core.Objects;

namespace Microhandlers.Sources.AspNet5
{
    public class Message: IMessage
    {
        public MessageName Name { get; }

        public Message(MessageName name)
        {
            Name = name;
        }

        
    }
}
