using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Objects;
using Xunit;

namespace MicrohandlersCoreTests
{
    public class NotCaseSensitiveStringTests
    {
	    [Fact]
	    public void same_strings_should_be_equal()
	    {
		    var str1 = "string1";
		    var str2 = "string1";

			Assert.Equal((InsensitiveString)str1, (InsensitiveString)str2);
	    }

		[Fact]
		public void same_strings_with_different_cases_should_be_equal()
		{
			var str1 = "string1";
			var str2 = "sTrInG1";

			Assert.Equal((InsensitiveString)str1, (InsensitiveString)str2);
		}


		[Fact]
		public void same_strings_should_be_same()
		{
			var str1 = "string1";
			var str2 = "string1";

			Assert.True((InsensitiveString)str1 == (InsensitiveString)str2);
		}

		[Fact]
		public void same_strings_with_different_cases_should_be_same()
		{
			var str1 = "string1";
			var str2 = "sTrInG1";

			Assert.True((InsensitiveString)str1 ==(InsensitiveString)str2);
		}

		[Fact]
		public void same_strings_should_have_same_hashcode()
		{
			var str1 = "string1";
			var str2 = "string1";

			Assert.True(((InsensitiveString)str1).GetHashCode() == ((InsensitiveString)str2).GetHashCode());
		}

		[Fact]
		public void same_strings_with_different_cases_should_have_same_hashcode()
		{
			var str1 = "string1";
			var str2 = "sTrInG1";

			Assert.True(((InsensitiveString)str1).GetHashCode() == ((InsensitiveString)str2).GetHashCode());
		}

		[Fact]
		public void same_string_should_be_equal_if_one_is_notcasesensitive()
		{
			var str1 = "string1";
			var str2 = "string1";

			Assert.Equal((InsensitiveString)str1, str2);
		}

		[Fact]
		public void same_strings_with_different_cases_should_not_be_equal_if_one_is_notcasesensitive()
		{
			var str1 = "string1";
			var str2 = "sTrInG1";

			Assert.NotSame(str1, (InsensitiveString)str2);
			Assert.NotSame((InsensitiveString)str1, str2);
		}

	}
}
