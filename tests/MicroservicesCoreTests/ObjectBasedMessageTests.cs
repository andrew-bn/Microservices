//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microservices.Core;
//using Xunit;

//namespace MicroservicesCoreTests
//{
//    public class ObjectBasedMessageTests
//    {
//		[Theory]
//		[InlineData(4), InlineData("stringValue"), InlineData(2.3), 
//		InlineData(2.3D), InlineData(true)]
//		public void should_successfuly_wrap_primitive_type(object value)
//		{
//			var msg = new ObjectBasedMessage(value.GetType(), "message.name", value, null);

//			Assert.Equal(value, msg.Value);
//			Assert.Equal(value, msg.ValueAs(value.GetType()));
//		}

//		[Theory]
//		[InlineData(4, typeof(short))]
//		[InlineData(4, typeof(string))]
//		[InlineData(4, typeof(byte))]
//		[InlineData("4", typeof(int))]
//		public void should_successfuly_change_types(object value, Type type)
//	    {
//			var msg = new ObjectBasedMessage(value.GetType(), "message.name", value, null);
//			Assert.Equal(value.ToString(), msg.ValueAs(type).ToString());
//		}

//		[Fact]
//		public void should_throw_invalid_cast_exception_if_invalid_conversion()
//		{
//			var value = 3.4;
//			var type = typeof (DateTime);
//			var msg = new ObjectBasedMessage(value.GetType(), "message.name", value, null);
//			Assert.Throws<InvalidCastException>(() => msg.ValueAs(type));
//		}
//	}
//}
