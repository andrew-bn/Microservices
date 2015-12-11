using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
	public class HtmlMessage : IMessage
	{

		public readonly string Message;
		public HtmlMessage(string message, ICookies cookies = null)
		{
			Message = message;
			Cookies = cookies;
		}

		public IMessage this[string parameterName]
		{
			get { throw new ArgumentException("Message has no parameters"); }
		}

		public ICookies Cookies { get; }

		public MessageName Name => string.Empty;

		public IEnumerable<IMessageParameterSchema> Parameters
		{
			get
			{
				throw new ArgumentException("Message has no parameters");
			}
		}

		public ParameterType Type => ParameterType.String;

		public object Value => Message;

		public string ToResponseString()
		{
			return Message;
		}

		public object ValueAs(Type type)
		{
			if (type == typeof(string))
				return Message;
			throw new ArgumentException("Unable to cast", nameof(type));
		}

		public static implicit operator HtmlMessage(string message)
		{
			return new HtmlMessage(message);
		}
		public static implicit operator string(HtmlMessage message)
		{
			return message.Message;
		}
	}
}
