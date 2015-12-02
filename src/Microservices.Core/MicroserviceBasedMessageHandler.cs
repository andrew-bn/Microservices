using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
	public class MicroserviceBasedMessageHandler : IMessageHandler
	{
		private object _instance;
		private MethodInfo _method;
		public MicroserviceBasedMessageHandler(string catchPattern, object instance, MethodInfo method)
		{
			_instance = instance;
			_method = method;
			CatchPattern = catchPattern;
			Message = new MethodMessageSchema(string.Empty, method);
			if (method.ReturnType.GetGenericArguments().Length > 0 && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
				Response = new TypeBasedSchema(string.Empty, method.ReturnType.GetGenericArguments()[0]);
			else Response = new TypeBasedSchema(string.Empty, typeof(void));
		}
		public string CatchPattern { get; }
		public IMessageSchema Message { get; }

		public IMessageSchema Response { get; }

		public async Task<IMessage> Handle(IMessageHandlersHost host, IMessage message)
		{
			var parameters = CollectParameters(host, message);
			var task = (Task)_method.Invoke(_instance, parameters.ToArray());
			await task;
			var result = task.GetType().GetProperty("Result").GetValue(task);
			return new ObjectBasedMessage(result.GetType(), string.Empty, result);
		}
		private List<object> CollectParameters(IMessageHandlersHost host, IMessage message)
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
				else if (p.ParameterType == typeof(IMessageHandlersHost))
				{
					value = host;
					skipped++;
				}
				else if (p.ParameterType == typeof (IMessage))
				{
					value = message;
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