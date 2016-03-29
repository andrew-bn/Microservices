using System;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Objects;

namespace Microhandlers.Core.Message
{
    public class ObjectBasedMessage: ObjectBasedMessageItem, IMessage
    {
		public MessageName Name { get; }

		public ObjectBasedMessage(Type type, MessageName messageName, object message)
			: base(type,message)
	    {
		    Name = messageName;
	    }
	}
}
