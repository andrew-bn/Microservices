//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microservices.Core;
//using Xunit;

//namespace MicroservicesCoreTests
//{
//	public class ExtensionTests
//	{
//		#region GetReturnType
//		[Fact]
//		public void getreturntype_should_return_valid_type_for_task()
//		{
//			var result = typeof(Task<int>).GetReturnType();
//			Assert.Equal(typeof(int), result);
//		}

//		[Fact]
//		public void getreturntype_should_return_void_type_for_void_task()
//		{
//			var result = typeof(Task).GetReturnType();
//			Assert.Equal(typeof(void), result);
//		}

//		[Fact]
//		public void getreturntype_should_return_valid_type_for_non_task()
//		{
//			var result = typeof(int).GetReturnType();
//			Assert.Equal(typeof(int), result);
//		}
//		#endregion GetReturnType

//		#region isgenerictype

//		[Fact]
//		public void isgenerictype_should_return_true_for_generic_type()
//		{
//			Assert.True(typeof(IEnumerable<int>).IsGenericType());
//		}

//		[Fact]
//		public void isgenerictype_should_return_false_for_non_generic_type()
//		{
//			Assert.False(typeof(Task).IsGenericType());
//		}
//		#endregion isgenerictype

//		#region istask

//		[Fact]
//		public void istask_should_return_true_for_task()
//		{
//			Assert.True(typeof(Task).IsTask());
//		}

//		[Fact]
//		public void istask_should_return_false_for_non_task()
//		{
//			Assert.False(typeof(bool).IsTask());
//		}
//		#endregion istask

//	}
//}
