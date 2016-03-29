using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Message;

namespace Microhandlers.Core.Infrastructure
{
	public interface IProtocolBinarySerializer
	{
		IMessageItem Deserialize(byte[] data);
		byte[] SerializeToArray(IMessageItem message);
	}

	public interface IProtocolStringSerializer
    {
	    IMessageItem Deserialize(string data);
		string SerializeToString(IMessageItem message);
	}
}
