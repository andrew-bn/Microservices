using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Message;
using Microhandlers.MessageSources.AspNet5;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microhandlers.Protocol.Json
{
	public class MicroJsonSerializer: IProtocolStringSerializer
	{
		public IMessageItem Deserialize(string data)
		{
			return new MessageItem(JToken.Parse(data));
		}

		public string SerializeToString(IMessageItem message)
		{
			var sb = new StringBuilder();
			Serialize(message, sb);
			return sb.ToString();
		}

		private static void Serialize(IMessageItem message, StringBuilder sb)
		{
			switch (message.Type)
			{
				case ItemType.Boolean:
					sb.Append(message.ValueAsBool() ? "true" : "false");
					break;
				case ItemType.IntegerNumber:
				case ItemType.FloatNumber:
					sb.Append(message.ValueAsString());
					break;
				case ItemType.String:
					sb.Append(JsonConvert.ToString(message.ValueAsString()));
					break;
				case ItemType.Null:
					sb.Append("null");
					break;
				case ItemType.Object:
					SerializeAsObject(message, sb);
					break;
				case ItemType.Array:
					SerializeAsArray(message, sb);
					break;
			}
		}

		private static void SerializeAsObject(IMessageItem message, StringBuilder sb)
		{
			sb.Append("{");
			var obj = message.ValueAsObject();
			var comma = false;
			foreach (var i in obj.Enumerate())
			{
				if (comma)
					sb.Append(",");
				comma = true;
				sb.AppendFormat("\"{0}\":", i.Key);

				Serialize(i.Value, sb);
			}
			sb.Append("}");
		}

		private static void SerializeAsArray(IMessageItem message, StringBuilder sb)
		{
			sb.Append("[");
			var obj = message.ValueAsObject();
			var comma = false;
			foreach (var i in obj.Enumerate())
			{
				if (comma)
					sb.Append(",");

				comma = true;
				Serialize(i.Value, sb);
			}
			sb.Append("]");
		}
	}
}
