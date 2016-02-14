//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microservices.Core;
//using Microservices.Core.Messaging;
//using Xunit;

//namespace MicroservicesCoreTests.Tests
//{
//    public class DynamicCallBasedMessageTests
//	{
//		[Fact]
//		public void after_ctor_message_name_is_valid()
//		{
//			var msg = CreateMessage("message.name");
//			Assert.Equal("message.name",msg.Name);
//		}

//		[Fact]
//		public void should_expose_passed_to_constructor_arguments()
//		{
//			var msg = CreateMessage("message.name",new [] {"param1","param2"}, new object[] {5, "second"});
//			var parameters = msg.Parameters.ToArray();

//			Assert.Equal(2, parameters.Length);
//			Assert.Equal("param1", parameters[0].ParameterName);
//			Assert.Equal("param2", parameters[1].ParameterName);
//		}


//		[Fact]
//		public void arguments_exposed_via_indexer_should_be_not_null()
//		{
//			var msg = CreateMessage("message.name", new[] { "param1", "param2" }, new object[] { 5, "second" });
//			var p1 = msg["param1"];
//			var p2 = msg["param2"];

//			Assert.NotNull(p1);
//			Assert.NotNull(p2);
//		}


//		[Fact]
//		public void arguments_exposed_via_indexer_should_be_of_type_objectbasedmessage()
//		{
//			var msg = CreateMessage("message.name", new[] { "param1", "param2" }, new object[] { 5, "second" });
//			var p1 = msg["param1"];
//			var p2 = msg["param2"];

//			Assert.True(p1 is ObjectBasedMessage);
//			Assert.True(p2 is ObjectBasedMessage);
//		}


//		private IMessage CreateMessage(string name, string[] args = null, object[] values=null,ICookies cookies = null)
//		{
//			return new DynamicCallBasedMessage("message.name", args ?? new string[0], values ?? new object[0], cookies);
//		}
//	}
//}
