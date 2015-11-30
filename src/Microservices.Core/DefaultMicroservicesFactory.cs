using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microservices.Core
{
	public class MessageHandlersLocator : IMicroservicesFactory
	{
		private readonly ILibraryManager _libraryManager;
		private readonly IServiceProvider _serviceProvider;

		public MessageHandlersLocator(ILibraryManager libraryManager, IMessageHandlersHost messageHanldersHost, IServiceProvider serviceProvider)
		{
			_libraryManager = libraryManager;
			_serviceProvider = serviceProvider;
		}

		public virtual List<IMicroservice> LocateMicroservices(IMessageHandlersHost microservicesHost)
		{
			var result = new List<IMicroservice>();
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
							result.Add(CreateMicroservice(t, microservicesHost));
					}
				}
			}
			return result;
		}

		protected virtual IMicroservice CreateMicroservice(Type type, IMessageHandlersHost microservicesHost)
		{
			return new Microservice(microservicesHost, ExtractMicroserviceName(type), type, _serviceProvider);
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
