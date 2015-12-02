using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
    public class EmptyMessage: IMessage
    {
	    public string Name { get; }
	    public ParameterType Type { get {return ParameterType.Void;} }
	    public IEnumerable<IMessageSchema> Parameters => null;
		public IMessage this[string parameterName] => null;

		public EmptyMessage(string name)
	    {
		    Name = name;
	    }
		
	    public object Value => null;

	    public object ValueAs(Type type)
	    {
		    return null;
	    }
    }
}
