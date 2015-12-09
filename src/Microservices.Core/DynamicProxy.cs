using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public class DynamicProxy : DynamicObject
	{
		private readonly IMessageHandlersHost _host;

		private string _message = string.Empty;

		public DynamicProxy(IMessageHandlersHost host)
		{
			_host = host;
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (_message.Length > 0)
				_message += ".";

			_message += binder.Name;
			result = this;
			return true;
		}

		public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
		{
			result = _host.Handle(new DynamicCallBasedMessage(_message.ToLower(), binder.CallInfo.ArgumentNames.ToArray(), args));
			return true;
		}
	}
}
