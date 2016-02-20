using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microhandlers.Core.Objects
{
    public struct MessageName
    {
        private readonly string _name;
        public static MessageName Empty = new MessageName(string.Empty);

        public MessageName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            _name = name.ToLower();
        }

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
            return string.Equals(_name, other._name);
        }

        public override int GetHashCode()
        {
            return _name?.GetHashCode() ?? 0;
        }

        public static implicit operator string(MessageName value)
        {
            return value._name;
        }

        public static implicit operator MessageName(string value)
        {
            return new MessageName(value);
        }
    }
}
