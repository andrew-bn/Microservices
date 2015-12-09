using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core;
using Xunit;

namespace MicroservicesCoreTests.Tests
{
    public class DynamicCallBasedMessageTests
	{
		[Fact]
		public void after_ctor_message_name_is_valid()
		{
			var msg = new DynamicCallBasedMessage("message.name",new string[0], new object[0]);
			Assert.Equal("message.name",msg.Name);
		}

		[Fact]
		public void should_expose_passed_to_constructor_arguments()
		{
			var msg = new DynamicCallBasedMessage("message.name",new [] {"param1","param2"}, new object[] {null,null});
			var parameters = msg.Parameters.ToArray();

			Assert.Equal(2, parameters.Length);
			Assert.Equal("param1", parameters[0].Name);
			Assert.Equal("param2", parameters[1].Name);
		}


	}
}
