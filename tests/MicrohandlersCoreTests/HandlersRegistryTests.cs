using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microhandlers.Core.Implementation;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Message;
using Microhandlers.Core.Objects;
using Microhandlers.Core.Exceptions;
namespace MicrohandlersCoreTests
{
    public class HandlersRegistryTests
    {
        private class FakeMsgHandler : IMessageHandler
        {
            public MessageName Name { get; }

            public FakeMsgHandler(MessageName name)
            {
                Name = name;
            }

            public Task<IMessage> Handle(IMessage message, IMessageDeserializer messageDeserializer, IServicesContainer servicesContainer)
            {
                return null;
            }
        }

        [Fact]
        public void should_successfuly_register_handler()
        {
            var registry = new HandlersRegistry();
            registry.Register(new FakeMsgHandler("message"));
        }
        [Fact]
        public void should_throw_error_for_duplicated_handlers()
        {
            var registry = new HandlersRegistry();
            registry.Register(new FakeMsgHandler("message"));
            Assert.Throws<MicroservicesCoreException>(() => registry.Register(new FakeMsgHandler("message")));
        }

        [Fact]
        public void should_return_handler_by_name()
        {
            var handler = new FakeMsgHandler("message");
            var registry = new HandlersRegistry();
            registry.Register(handler);
            var result = registry.First("message");
            Assert.Equal(handler, result);
        }
        [Fact]
        public void first_should_return_handler_by_first_part()
        {
            var handler = new FakeMsgHandler("message");
            var registry = new HandlersRegistry();
            registry.Register(handler);
            var result = registry.First("message.secondpart");
            Assert.Equal(handler, result);
        }

        [Fact]
        public void should_throw_error_if_handler_does_not_exist()
        {
            var registry = new HandlersRegistry();
            Assert.Throws<MicroservicesCoreException>(() => registry.First("message"));
        }

    }
}
