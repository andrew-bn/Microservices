using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Message;
using Microhandlers.Core.Objects;

namespace Microhandlers.HandlersDiscovery.Microservice
{
    public class MicroserviceMessageHandler: IMessageHandler
    {
        private readonly Type _type;
        private readonly MethodInfo _method;

        public MessageName Name { get; }

        public MicroserviceMessageHandler(MessageName name, Type type, MethodInfo method)
        {
            Name = name;
            _type = type;
            _method = method;
        }

        public Task<IMessage> Handle(IMessage message, IMessageDeserializer messageDeserializer, IServicesContainer servicesContainer)
        {
            var parameters = CollectParameters(message, messageDeserializer, servicesContainer);

            var instance = Activator.CreateInstance(_type);

            _method.Invoke(instance, null);
            return null;
            //throw new NotImplementedException();
        }

        private List<object> CollectParameters(IMessage message, IMessageDeserializer messageDeserializer, IServicesContainer sequence)
        {
            return null;
        }

        /*
            private List<object> CollectParameters(IMessageHandlersHost host, IMessage message, IHandlersQueue sequence)
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
                    else if (p.ParameterType == typeof(ICookies))
                    {
                        value = message.Cookies;
                        skipped++;
                    }
                    else if (p.ParameterType == typeof(IMessageHandlersHost))
                    {
                        value = host;
                        skipped++;
                    }
                    else if (p.ParameterType == typeof(IMessage))
                    {
                        value = message;
                        skipped++;
                    }
                    else if (p.ParameterType == typeof(IHandlersQueue))
                    {
                        value = sequence;
                        skipped++;
                    }
                    else if (p.ParameterType == typeof(IMessageHandler))
                    {
                        value = this;
                        skipped++;
                    }
                    else if (p.ParameterType == typeof(object))
                    {
                        value = host.CreateDynamicProxy();
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
            }*/
        }
}
