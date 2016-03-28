using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microhandlers.Core.Objects
{
    public struct InsensitiveString
    {
	    private readonly string _value;
	    private readonly string _lower;
	    private InsensitiveString(string value)
	    {
		    _value = value;
		    _lower = _value.ToLower();
	    }

	    public static implicit operator string(InsensitiveString value)
		{
			return value._value;
		}

		public static implicit operator InsensitiveString(string value)
		{
			return new InsensitiveString(value);
		}
		
		public static bool operator ==(InsensitiveString left, InsensitiveString right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(InsensitiveString left, InsensitiveString right)
		{
			return !left.Equals(right);
		}


		public override bool Equals(object obj)
		{
			if (obj is InsensitiveString)
				return Equals((InsensitiveString)obj);

			var s = obj as string;
			return s != null && Equals((InsensitiveString)s);
		}


		public bool Equals(InsensitiveString other)
		{
			return string.Equals(_lower, other._lower);
		}

		public override int GetHashCode()
		{
			return _lower?.GetHashCode() ?? 0;
		}
	}
}
