using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public class MicroserviceHolder
	{
		private readonly IServiceProvider _serviceProvider;
		public Type Type { get; }
		public object Instance { get; }
		public string Name { get; }
		public SynchronizationContext SynchronizationContext { get; }
		public MicroserviceHolder(Type type, IServiceProvider serviceProvider, SynchronizationContext synchronizationContext)
		{
			_serviceProvider = serviceProvider;
			Type = type;
			SynchronizationContext = synchronizationContext;
			Instance = InstantiateMicroservice(type);
			Name = (type.Namespace ?? "").Substring((type.Namespace ?? "")
					.IndexOf(".Microservices", StringComparison.Ordinal)).Trim('.') + "." + type.Name;
		}

		public async Task Call(string method, IMessageContext messageContext)
		{
			var methodInfo = Type.GetMethod(method, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

			var parameters = new List<object>();
			var skipped = 0;
			foreach (var p in methodInfo.GetParameters())
			{
				object value = null;
				if (p.IsRetval) continue;

				if (p.HasDefaultValue)
					value = p.DefaultValue;

				if (p.ParameterType == typeof (IMessageContext))
				{
					value = messageContext;
					skipped++;
				}
				else
					value = messageContext.Request.ReadParameter(p.ParameterType,
						new RequestParameter(p.Position - skipped, p.Name)) ?? value;


				parameters.Add(value);
			}

			var task = (Task)methodInfo.Invoke(Instance, parameters.ToArray());
			await task;
			var result = task.GetType().GetProperty("Result").GetValue(task);
			await messageContext.Response.WriteResult(result);
		}

		private object InstantiateMicroservice(Type type)
		{
			var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)[0];
			return ctor.Invoke(ctor.GetParameters().Select(p => _serviceProvider.GetService(p.ParameterType)).ToArray());
		}
	}
}