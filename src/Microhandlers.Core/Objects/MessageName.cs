using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microhandlers.Core.Objects
{
    public struct MessageName
    {
        private readonly InsensitiveString _name;
        private InsensitiveString[] _parts;
        public static MessageName Empty = new MessageName(string.Empty);

        public MessageName(InsensitiveString name)
        {
            _name = name;
            _parts = _name.Split('.');
        }

		public IEnumerable<InsensitiveString> Parts { get { return _parts; } }

        public override string ToString()
        {
            return _name;
        }

        public override bool Equals(object obj)
        {
            if (obj is MessageName)
                return Equals((MessageName)obj);

            var s = obj as string;
            return s != null && Equals(s);
        }

        public bool Equals(MessageName other)
        {
            return _name.Equals(other._name);
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

		public static implicit operator string(MessageName value)
		{
			return value._name;
		}

		public static implicit operator MessageName(string value)
		{
			return new MessageName(value);
		}

		public static implicit operator InsensitiveString(MessageName value)
		{
			return value._name;
		}

		public static implicit operator MessageName(InsensitiveString value)
		{
			return new MessageName(value);
		}
	}
}
