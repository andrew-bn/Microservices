using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
	public class MicroserviceBasedMessageHandler : IMessageHandler
	{
		public IMessageTypeSchema Message { get; }
		public IMessageTypeSchema Response { get; }
		private readonly object _instance;
		private readonly MethodInfo _method;

		public MicroserviceBasedMessageHandler(object instance, MethodInfo method)
		{
			_instance = instance;
			_method = method;
			Message = new MethodMessageSchema(string.Empty, method);
			if (method.ReturnType.GetGenericArguments().Length > 0 && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
				Response = new TypeBasedSchema(string.Empty, method.ReturnType.GetGenericArguments()[0]);
			else Response = new TypeBasedSchema(string.Empty, typeof(void));

		}

		public async Task<IMessage> Handle(IMessageHandlersHost host, IMessage message, IHandlersQueue sequence)
		{
			var parameters = CollectParameters(host, message, sequence);
			var result = _method.Invoke(_instance, parameters.ToArray());
			if (result == null)
				return null;
			if (typeof(IMessage).IsAssignableFrom(result.GetType()))
				return (IMessage)result;

			if (_method.ReturnType.IsTask())
			{
				await ((Task)result);

				result = _method.ReturnType.IsGenericType() 
							? result.GetType().GetProperty("Result").GetValue(result) 
							: null;
			}

			return new ObjectBasedMessage(result.GetType(), string.Empty, result, message.Cookies);
		}

		private List<object> CollectParameters(IMessageHandlersHost host, IMessage message, IHandlersQueue sequence)
		{
			var parameters = new List<object>();
			var skipped = 0;
			foreach (var p in _method.GetParameters())
			{
				object value = null;
				if (p.IsRetval) continue;

				if (p.HasDefaultValue)
					value = p.DefaultValue;

				var srv = host.ResolveDependency(p.ParameterType);
				if (srv != null)
				{
					value = srv;
					skipped++;
				}
				else if (p.ParameterType == typeof(ICookies))
				{
					value = message.Cookies;
					skipped++;
				}
				else if (p.ParameterType == typeof(IMessageHandlersHost))
				{
					value = host;
					skipped++;
				}
				else if (p.ParameterType == typeof(IMessage))
				{
					value = message;
					skipped++;
				}
				else if (p.ParameterType == typeof(IHandlersQueue))
				{
					value = sequence;
					skipped++;
				}
				else if (p.ParameterType == typeof(IMessageHandler))
				{
					value = this;
					skipped++;
				}
				else if (p.ParameterType == typeof (object))
				{
					value = host.CreateDynamicProxy();
					skipped++;
				}
				else
				{
					var param = message[p.Name];
					if (param == null)
						param = message[(p.Position - skipped).ToString()];
					value = param.ValueAs(p.ParameterType) ?? value;
				}
				parameters.Add(value);
			}
			return parameters;
		}
	}
}