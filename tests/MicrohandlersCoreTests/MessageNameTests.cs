using Microhandlers.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MicrohandlersCoreTests
{
    public class MessageNameTests
    {
        [Fact]
        public void implicitly_convertable_to_string()
        {
            var name = "message.name";
            var msg = new MessageName(name);
            string strMsg = msg;
            Assert.Equal(name, strMsg);
        }

        [Fact]
        public void implicitly_convertable_from_string()
        {
            var name = "message.name";
            var msg = (MessageName)name;
            Assert.Equal(name, msg.ToString());
        }

        [Fact]
        public void empty_should_be_equal_to_stringempty()
        {
            var emptyVal = MessageName.Empty;
            Assert.Equal(string.Empty, emptyVal.ToString());
        }
    }
}
