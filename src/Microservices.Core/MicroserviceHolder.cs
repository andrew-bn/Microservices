using System;
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
			Name = (type.Namespace??"").Substring((type.Namespace??"")
					.IndexOf(".Microservices", StringComparison.Ordinal)).Trim('.') + "." + type.Name;
		}

		public Task Call(string method, IMessageContext messageContext)
		{
			return (Task)Type.GetMethod(method, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)
				.Invoke(Instance, new object[] { messageContext });
		}

		private object InstantiateMicroservice(Type type)
		{
			var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)[0];
			return ctor.Invoke(ctor.GetParameters().Select(p => _serviceProvider.GetService(p.ParameterType)).ToArray());
		}
	}
}