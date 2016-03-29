using System;
using System.Collections.Generic;
using Microhandlers.Core.Extensions;

namespace Microhandlers.Core.Message
{
	public class ObjectBasedMessageItem : IMessageItem
	{
		private readonly Type _type;
		private readonly object _message;

		public ItemType Type
		{
			get
			{
				if (_message == null) return ItemType.Null;
				if (_type.IsInteger()) return ItemType.IntegerNumber;
				if (_type.IsFloat()) return ItemType.FloatNumber;
				if (_type.IsString()) return ItemType.String;
				if (_type.IsBool()) return ItemType.Boolean;
				if (_type.IsArray()) return ItemType.Array;
				return ItemType.Object;
			}
		}

		public ObjectBasedMessageItem(Type type, object message)
		{
			_type = type;
			_message = message;
		}

		public bool ValueAsBool()
		{
			return (bool) _message;
		}

		public string ValueAsString()
		{
			return _message?.ToString();
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