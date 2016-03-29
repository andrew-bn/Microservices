using System;
using System.Collections.Generic;
using Microhandlers.Core.Message;
using Microhandlers.Core.Objects;
using Newtonsoft.Json.Linq;

namespace Microhandlers.MessageSources.AspNet5
{
	internal class MessageItem : IMessageItem
	{
		private readonly JToken _token;

		public MessageItem(JToken token)
		{
			_token = token;
		}

		public ItemType Type
		{
			get
			{
				switch (_token.Type)
				{
					case JTokenType.Array: return ItemType.Array;
					case JTokenType.Boolean: return ItemType.Boolean;
					case JTokenType.Object: return ItemType.Object;
					case JTokenType.Null: return ItemType.Null;
					case JTokenType.Integer: return ItemType.IntegerNumber;
					case JTokenType.Float: return ItemType.FloatNumber;
					default: return ItemType.String;
				}
			}
		}

		public IEnumerable<IMessageItem> ValueAsArray()
		{
			throw new NotImplementedException();
		}

		public bool ValueAsBool()
		{
			return _token.Value<bool>();
		}

		public IItemObject ValueAsObject()
		{
			throw new NotImplementedException();
		}

		public string ValueAsString()
		{
			return _token.Value<string>();
		}
	}
}