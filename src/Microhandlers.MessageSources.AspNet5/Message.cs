using System;
using System.Collections.Generic;
using Microhandlers.Core.Message;
using Microhandlers.Core.Objects;

namespace Microhandlers.MessageSources.AspNet5
{
    public class Message: IMessage
    {
        public MessageName Name { get; }

        string IMessageItem.Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ItemType Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Message(MessageName name)
        {
            Name = name;
        }

        public bool ValueAsBool()
        {
            throw new NotImplementedException();
        }

        public string ValueAsString()
        {
            throw new NotImplementedException();
        }

        public IItemObject ValueAsObject()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMessageItem> ValueAsArray()
        {
            throw new NotImplementedException();
        }
    }
}
