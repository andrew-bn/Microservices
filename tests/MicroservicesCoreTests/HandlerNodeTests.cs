//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microservices.Core;
//using Microservices.Core.Messaging;
//using Xunit;

//namespace MicroservicesCoreTests
//{
//    public class HandlerNodeTests
//    {
//		public class FakeHandler : IMessageHandler
//		{
//			public IMessageTypeSchema Message { get; }

//			public IMessageTypeSchema Response { get; }

//			public Task<IMessage> Handle(IMessageHandlersHost host, IMessage message, IHandlersQueue sequence)
//			{
//				throw new NotImplementedException();
//			}
//		}

//		[Fact]
//		public void successfuly_adding_several_handlers()
//		{
//			var node = new HandlerNode(null, "my.message", new FakeHandler());
//			node.Register("my.message2", new FakeHandler());
//			node.Register("my.message.submessage", new FakeHandler());
//		}

//		[Fact]
//		public void successfuly_collect_execution_sequence()
//		{
//			var h1 = new FakeHandler();
//			var h2 = new FakeHandler();
//			var h3 = new FakeHandler();
//			var node = new HandlerNode(null, "my.message", h1);
//			node.Register("my.message2", h2);
//			node.Register("my.message.submessage", h3);

//			var result = node.CollectHandlersQueue("my.message.submessage");
//			Assert.Equal(h1,result.Next());
//			Assert.Equal(h3, result.Next());
//		}
//	}
//}
