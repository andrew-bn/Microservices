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

        [Fact]
        public void equality_should_be_not_casesensitive()
        {
            var m1 = (MessageName) "Message";
            var m2 = (MessageName) "mEssage";
            Assert.True(m1.Equals(m2));
        }

        [Fact]
        public void message_name_should_not_be_lowered()
        {
            var m1 = (MessageName)"Message";
            Assert.Equal(m1.ToString(), "Message");
        }
    }
}
