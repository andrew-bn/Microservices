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
		public IMessageHandler[] Handlers { get; }
		public IMessageHandler CatchAll { get; }
		public IMessageHandler Initializer { get; }
		public IMicroserviceEvent[] Events { get; }
		public IMicroservicesHost MicroservicesHost { get; }
		public Microservice(IMicroservicesHost microservicesHost, string microserviceName, Type type, IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
			Type = type;
			Instance = InstantiateMicroservice(type);
			Name = microserviceName;
			MicroservicesHost = microservicesHost;
		}

		public async Task<IMessage> Invoke(IMessage message)
		{
			var methodInfo = Type.GetMethod(message.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

			var parameters = CollectParameters(message, methodInfo);

			var task = (Task)methodInfo.Invoke(Instance, parameters.ToArray());
			await task;
			var result = task.GetType().GetProperty("Result").GetValue(task);
			return result.AsMessage();
		}

		private List<object> CollectParameters(IMessage message, MethodInfo methodInfo)
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
					value = srv;
					skipped++;
				}
				else if (p.ParameterType == typeof(IMessage))
				{
					value = message;
					skipped++;
				}
				else if (p.ParameterType == typeof(IMicroservicesHost))
				{
					value = MicroservicesHost;
					skipped++;
				}
				else
					value = message[p.Name].ValueAs(p.ParameterType)??value;

				parameters.Add(value);
			}

			return parameters;
		}

		private object InstantiateMicroservice(Type type)
		{
			var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)[0];
			return ctor.Invoke(ctor.GetParameters().Select(p =>
			{
				return _serviceProvider.GetService(p.ParameterType);
			}).ToArray());
		}
	}
}