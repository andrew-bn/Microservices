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
            var instance = Activator.CreateInstance(_type);
            _method.Invoke(instance, null);
            return null;
            //throw new NotImplementedException();
        }
    }
}
