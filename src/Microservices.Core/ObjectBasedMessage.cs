using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Microservices.Core
{
	public class ObjectBasedMessage : TypeBasedSchema, IMessage
	{
		public object UnderlyingObject { get; }

		public object Value => UnderlyingObject;

		public ICookies Cookies { get; }

		public MessageName Name { get; }

		public IMessage this[string parameterName]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ObjectBasedMessage(Type type, MessageName name, object message, ICookies cookies)
			: base(name, type)
		{
			UnderlyingObject = message;
			Cookies = cookies;
		}

		public object ValueAs(Type type)
		{
			if (UnderlyingObject == null)
				return UnderlyingObject;
			if (type == UnderlyingObject.GetType())
				return UnderlyingObject;

			throw new NotImplementedException();
		}

		public string ToResponseString()
		{
			using (var sw = new StringWriter())
			{
				JsonSerializer.Create().Serialize(sw, UnderlyingObject);
				return sw.ToString();
			}
		}
	}
}
