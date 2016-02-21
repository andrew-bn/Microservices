using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microhandlers.Core.Implementation;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Message;
using Microhandlers.Core.Objects;

namespace MicrohandlersCoreTests
{
    public class HandlersRegistryTests
    {
        private class FakeMsgHandler : IMessageHandler
        {
            public MessageName Name { get; }

            public Task<IMessage> Handle(IMessage message, IServicesContainer servicesContainer)
            {
                return null;
            }
        }

        [Fact]
        public void should_successfuly_register_handler()
        {
            var registry = new HandlersRegistry();
            registry.Register(new FakeMsgHandler());
        }

    }
}
