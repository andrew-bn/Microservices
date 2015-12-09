using System;
using System.Collections.Generic;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
	public class DynamicCallBasedMessage: IMessage
	{
		public string Name { get; }
		public ParameterType Type { get; }
		public IEnumerable<IMessageSchema> Parameters { get; }

		public IMessage this[string parameterName]
		{
			get { throw new NotImplementedException(); }
		}

		public object Value { get; }
		public object ValueAs(Type type)
		{
			throw new NotImplementedException();
		}

		public string ToResponseString()
		{
			throw new NotImplementedException();
		}

		public ICookies Cookies { get; }


		public DynamicCallBasedMessage(string name, string[] arguments, object[] values)
		{
			Name = name;
		}
	}
}