using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Objects;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microhandlers.HandlersDiscovery.Microservice
{
    public class MicroserviceDiscoverer
    {
        public IEnumerable<IMessageHandler> Discover(ILibraryManager libraryManager)
        {
            return Discover(libraryManager, 
                t => t.Name.EndsWith("Microservice"),
                m=> m.Name != nameof(ToString) && m.Name != nameof(GetHashCode) && m.Name != nameof(Equals) &&
                    m.Name != nameof(GetType),
                (t,m)=>$"{t.Name.Replace("Microservice","")}.{m.Name}");
        }

        public IEnumerable<IMessageHandler> Discover(ILibraryManager libraryManager,
            Func<Type, bool> condition,
            Func<MethodInfo, bool> useMethod,
            Func<Type, MethodInfo, MessageName> getMessageName)
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            foreach (var l in GetLibraries(libraryManager))
            {
                foreach (var an in GetAssemblyNames(l))
                {
                    var asm = Assembly.Load(an);
                    if (asm.IsDynamic) continue;
                    foreach (var t in asm.GetTypes())
                    {
                        if (t.Namespace == null || !condition(t)) continue;
                        foreach (var h in GetHandlersFromType(t, useMethod, getMessageName))
                            yield return h;
                    }
                }
            }
        }

        private IEnumerable<IMessageHandler> GetHandlersFromType(Type type, Func<MethodInfo, bool> useMethod, Func<Type, MethodInfo, MessageName> getMessageName)
        {
            return from m in type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                   where m.GetCustomAttribute<CompilerGeneratedAttribute>() == null && useMethod(m)
                   select new MicroserviceMessageHandler(getMessageName(type,m), type, m);
        }

        #region internal
        private static IEnumerable<AssemblyName> GetAssemblyNames(Library l)
        {
            return l.Assemblies.Where(asm =>
                !asm.Name.StartsWith("Microsoft") &&
                !asm.Name.StartsWith("System"));
        }

        private static IEnumerable<Library> GetLibraries(ILibraryManager libraryManager)
        {
            return libraryManager.GetLibraries()
                .Where(lbl => !lbl.Name.StartsWith("Microsoft"))
                .Where(lbl => !lbl.Name.StartsWith("System"));
        }
        #endregion internal

    }
}
