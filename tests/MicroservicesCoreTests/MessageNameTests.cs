//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microservices.Core;
//using Xunit;

//namespace MicroservicesCoreTests
//{
//    public class MessageNameTests
//    {
//		[Fact]
//		public void implicitly_convertable_to_string()
//		{
//			var name = "message.name";
//			var msg = new MessageName(name);
//			string strMsg = msg;

//			Assert.Equal(name, strMsg);
//		}

//		[Fact]
//		public void implicitly_convertable_from_string()
//		{
//			var name = "message.name";
//			MessageName msg = name;
//			string strMsg = msg;

//			Assert.Equal(name, strMsg);
//		}

//		[Fact]
//		public void should_properly_calculate_next_handler_name()
//		{
//			var name = "my.super.message.name";
//			string result = ((MessageName)name).GetNextHandlerName("my.super");
//			Assert.Equal("my.super.message", result);
//		}

//		[Fact]
//		public void should_return_empty_message_name_if_no_next_handler()
//		{
//			var name = "my.super.message.name";
//			var result = ((MessageName)name).GetNextHandlerName(name);
//			Assert.Equal(MessageName.Empty, result);
//		}
//		[Fact]
//		public void should_return_empty_message_name_if_current_handler_does_not_match_message()
//		{
//			var name = "my.super.message.name";
//			var result = ((MessageName)name).GetNextHandlerName("my.super2.message.name");
//			Assert.Equal(MessageName.Empty, result);
//		}
//		[Fact]
//		public void should_return_first_part_if_current_handler_name_is_empty()
//		{
//			var name = "my.super.message.name";
//			var result = ((MessageName)name).GetNextHandlerName("");
//			Assert.Equal("my", result);
//		}

//		[Fact]
//		public void tostring_should_return_message_name()
//		{
//			var name = "my.super.message.name";
//			var result = ((MessageName)name);
//			Assert.Equal(name, result.ToString());
//		}
//	}
//}
