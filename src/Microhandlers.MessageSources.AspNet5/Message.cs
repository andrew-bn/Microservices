using System;
using System.Collections.Generic;
using System.IO;
using Microhandlers.Core.Message;
using Microhandlers.Core.Objects;
using Newtonsoft.Json.Linq;

namespace Microhandlers.MessageSources.AspNet5
{
	public class Message : IMessage
	{
		private readonly IMessageItem _item;
		public MessageName Name { get; }

		public ItemType Type
		{
			get { return _item.Type; }
		}

		public IMessageItem this[InsensitiveString propertyName]
		{
			get { return _item.ValueAsObject()[propertyName]; }
		}

		public Message(MessageName name, IMessageItem item)
		{
			_item = item;
			Name = name;
		}

		public bool ValueAsBool()
		{
			return _item.ValueAsBool();
		}

		public string ValueAsString()
		{
			return _item.ValueAsString();
		}

		public IItemObject ValueAsObject()
		{
			return _item.ValueAsObject();
		}

		public IEnumerable<IMessageItem> ValueAsArray()
		{
			return _item.ValueAsArray();
		}
	}
}
