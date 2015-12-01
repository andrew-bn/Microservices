using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core.Messaging
{
	public class ObjectBasedMessage : IMessage
	{
		public object UnderlyingObject { get; }
		public ObjectBasedMessage(object message)
		{
			UnderlyingObject = message;
		}

		public IMessage this[string parameterName]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IDictionary<string, IMessageSchema> Parameters
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public ParameterType Type
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public object Value
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public object ValueAs(Type type)
		{
			throw new NotImplementedException();
		}
	}
}
