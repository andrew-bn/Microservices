using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microservices.Core.Messaging;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microservices.Core
{
	public class MicroserviceBasedMessageHandler : IMessageHandler
	{
		private object _instance;
		private MethodInfo _method;
		public MicroserviceBasedMessageHandler(object instance, MethodInfo method)
		{
			_instance = instance;
			_method = method;
			CatchPattern = $"{method.DeclaringType.Namespace.ToLower()}.{method.DeclaringType.Name.ToLower()}.{method.Name.ToLower()}";
        }
		public string CatchPattern { get; set; }
		public IMessageSchema Message { get; set; }

		public IMessageSchema Response { get; set; }

		public Task<IMessage> Handle(IMessage message)
		{
			//message.Parameters.Select(p=>p.)
			//_method.Invoke(_instance);
			throw new NotImplementedException();
		}
	}
	public class MicroservicesProvider : IMessageHandlersProvider
	{
		private readonly ILibraryManager _libraryManager;
		private readonly IServiceProvider _serviceProvider;
		private readonly IMessageHandlersHost _messageHanldersHost;
		public MicroservicesProvider(ILibraryManager libraryManager, IMessageHandlersHost messageHanldersHost, IServiceProvider serviceProvider)
		{
			_libraryManager = libraryManager;
			_serviceProvider = serviceProvider;
			_messageHanldersHost = messageHanldersHost;
		}

		public virtual void ProvideMessageHandlers()
		{
			var result = new List<IMessageHandler>();
			foreach (var l in _libraryManager.GetLibraries()
								.Where(lbl => !lbl.Name.StartsWith("Microsoft"))
							.Where(lbl => !lbl.Name.StartsWith("System")))
			{
				foreach (var an in l.Assemblies.Where(asm=>
											!asm.Name.StartsWith("Microsoft") &&
											!asm.Name.StartsWith("System")))
				{
					var asm = Assembly.Load(an);
					if (asm.IsDynamic) continue;
					foreach(var t in asm.GetTypes())
					{
						if (t.Namespace != null && (t.Namespace.Contains(".Microservices.") || t.Namespace.EndsWith(".Microservices"))
								&& t.Name.EndsWith("Microservice"))
							result.AddRange(CreateMessageHandlers(t, _messageHanldersHost));
					}
				}
			}

			foreach (var mh in result)
				_messageHanldersHost.Register(mh);

		}

		protected virtual IEnumerable<IMessageHandler> CreateMessageHandlers(Type type, IMessageHandlersHost microservicesHost)
		{
			var handlers = new List<IMessageHandler>();
			var microserviceInstance = Activator.CreateInstance(type);

			foreach (var m in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
				handlers.Add(new MicroserviceBasedMessageHandler(microserviceInstance, m));
			
			return handlers;
		}

		protected virtual string ExtractMicroserviceName(Type type)
		{
			var name = type.Name;
			if (name.EndsWith("Microservice"))
				name = name.Substring(0, name.Length - "Microservice".Length);
			if ((type.Namespace ?? "").Contains(".Microservices."))
				name = type.Namespace.Substring(type.Namespace.IndexOf(".Microservices.") + 13) + name;

			return name.ToLower();
		}
	}
}
