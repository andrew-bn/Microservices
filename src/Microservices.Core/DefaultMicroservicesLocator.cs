using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microservices.Core
{
	public class DefaultMicroservicesLocator : IMicroservicesLocator
	{
		private ILibraryManager _libraryManager;
		public DefaultMicroservicesLocator(ILibraryManager libraryManager)
		{
			_libraryManager = libraryManager;
		}

		public virtual List<Type> FindMicroservices()
		{
			var result = new List<Type>();
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
							result.Add(t);
					}
				}
			}
			return result;
		}
	}
}
