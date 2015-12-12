using System;
using System.Collections.Generic;
using System.Linq;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
	public class DynamicCallBasedMessage: IMessage
	{

		private static object _null = new object();
		public MessageName Name { get; }
		public ParameterType Type { get; }
		public IEnumerable<IMessageParameterSchema> Parameters => _parameters;

		public IMessageValue this[string parameterName] => _parameters.FirstOrDefault(p => p.ParameterName == parameterName);

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

		private string[] _arguments;
		private object[] _values;
		private ObjectBasedMessage[] _parameters;
		public DynamicCallBasedMessage(string name, string[] arguments,  object[] values, ICookies cookies)
		{
			Name = name;
			_arguments = arguments;
			_values = values;
			Cookies = cookies;
			_parameters = new ObjectBasedMessage[arguments.Length];
			for (int i = 0; i < arguments.Length; i++)
				_parameters[i] = new ObjectBasedMessage((values[i] ?? _null).GetType(), arguments[i], values[i], cookies);
			
		}
	}
}