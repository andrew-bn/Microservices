using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public class Microservice : IMicroservice
	{
		private readonly IServiceProvider _serviceProvider;
		public Type Type { get; }
		public object Instance { get; }
		public string Name { get; }
		public Microservice(string microserviceName, Type type, IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
			Type = type;
			Instance = InstantiateMicroservice(type);
			Name = microserviceName;
		}

		public async Task Invoke(string method, IMessageContext messageContext)
		{
			var methodInfo = Type.GetMethod(method, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

			var parameters = CollectParameters(messageContext, methodInfo);

			var task = (Task)methodInfo.Invoke(Instance, parameters.ToArray());
			await task;
			var result = task.GetType().GetProperty("Result").GetValue(task);
			await messageContext.Response.WriteResult(result);
		}

		private List<object> CollectParameters(IMessageContext messageContext, MethodInfo methodInfo)
		{
			var parameters = new List<object>();
			var skipped = 0;
			foreach (var p in methodInfo.GetParameters())
			{
				object value = null;
				if (p.IsRetval) continue;

				if (p.HasDefaultValue)
					value = p.DefaultValue;

				var srv = _serviceProvider.GetService(p.ParameterType);
				if (srv != null)
				{
					value = messageContext;
					skipped++;
				}
				else if (p.ParameterType == typeof (IMessageContext))
				{
					value = messageContext;
					skipped++;
				}
				else if (p.ParameterType == typeof(IMessageRequest))
				{
					value = messageContext.Request;
					skipped++;
				}
				else if (p.ParameterType == typeof(IMessageResponse))
				{
					value = messageContext.Response;
					skipped++;
				}
				else
					value = messageContext.Request.ReadParameter(p.ParameterType,
						new RequestParameter(p.Position - skipped, p.Name)) ?? value;

				parameters.Add(value);
			}
			return parameters;
		}

		private object InstantiateMicroservice(Type type)
		{
			var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)[0];
			return ctor.Invoke(ctor.GetParameters().Select(p => _serviceProvider.GetService(p.ParameterType)).ToArray());
		}
	}
}