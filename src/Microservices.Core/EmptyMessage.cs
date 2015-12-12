﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
    public class EmptyMessage: IMessage
    {
	    public MessageName Name { get; }
	    public ParameterType Type { get {return ParameterType.Void;} }
	    public IEnumerable<IMessageParameterSchema> Parameters => null;
		public IMessageValue this[string parameterName] => null;

		public EmptyMessage(string name)
	    {
		    Name = name;
	    }
		
	    public object Value => null;

		public ICookies Cookies
		{
			get { return null; }
		}

		public object ValueAs(Type type)
	    {
		    return null;
	    }

		public string ToResponseString()
		{
			return string.Empty;
		}
	}
}
