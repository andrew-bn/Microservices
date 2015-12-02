﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microservices.Core
{
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
						if (t.Namespace != null && t.Name.EndsWith("Microservice"))
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
			var microserviceName = type.Name.ToLower();
			microserviceName = microserviceName.Substring(0, microserviceName.IndexOf("microservice", StringComparison.Ordinal));
			
			foreach (var m in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
			{

				if (m.ReturnType == typeof(Task) || 
					(m.ReturnType.GetGenericArguments().Length > 0 && m.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)) &&
					m.GetCustomAttribute<CompilerGeneratedAttribute>() == null)
				{
					var catchPattern = $"{microserviceName}.{m.Name}".ToLower();

					handlers.Add(new MicroserviceBasedMessageHandler(catchPattern, microserviceInstance, m));
				}
			}
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
