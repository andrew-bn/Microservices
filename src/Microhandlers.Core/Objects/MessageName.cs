using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microhandlers.Core.Objects
{
    public struct MessageName: IEnumerable<MessageName>
    {
        private class MessageNameEnumerator : IEnumerator
        {
            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public object Current { get; }
        }

        private readonly string _name;
        private string[] _parts;
        public static MessageName Empty = new MessageName(string.Empty);

        public MessageName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            _name = name.ToLower();
            _parts = _name.Split('.');
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

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<MessageName> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion IEnumerable
    }
}
