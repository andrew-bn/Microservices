using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Microservices.Core.Messaging
{
	public class ObjectBasedMessage : TypeBasedSchema, IMessage
	{
		public object UnderlyingObject { get; }

		public object Value
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IMessage this[string parameterName]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ObjectBasedMessage(Type type, string name, object message)
			: base(name, type)
		{
			UnderlyingObject = message;
		}

		public object ValueAs(Type type)
		{
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
