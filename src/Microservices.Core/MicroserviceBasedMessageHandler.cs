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
		public IMessageSchema Message { get; }
		public IMessageSchema Response { get; }
		public string Name { get; }
		private readonly object _instance;
		private readonly MethodInfo _method;

		public MicroserviceBasedMessageHandler(string name, object instance, MethodInfo method)
		{
			_instance = instance;
			_method = method;
			Name = name;
			Message = new MethodMessageSchema(string.Empty, method);
			if (method.ReturnType.GetGenericArguments().Length > 0 && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
				Response = new TypeBasedSchema(string.Empty, method.ReturnType.GetGenericArguments()[0]);
			else Response = new TypeBasedSchema(string.Empty, typeof(void));

		}

		public async Task<IMessage> Handle(IMessageHandlersHost host, IMessage message, IHandlersSequence sequence)
		{
			var parameters = CollectParameters(host, message, sequence);
			object result = _method.Invoke(_instance, parameters.ToArray());
			if (typeof(Type).IsAssignableFrom(_method.ReturnType))
			{
				await ((Task)result);
				result = result.GetType().GetProperty("Result").GetValue(result);
			}
			if (typeof(IMessage).IsAssignableFrom(result.GetType()))
				return (IMessage)result;
			return new ObjectBasedMessage(result.GetType(), string.Empty, result, message.Cookies);
		}

		private List<object> CollectParameters(IMessageHandlersHost host, IMessage message, IHandlersSequence sequence)
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
				else if (p.ParameterType == typeof(IHandlersSequence))
				{
					value = sequence;
					skipped++;
				}
				else if (p.ParameterType == typeof(IMessageHandler))
				{
					value = this;
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