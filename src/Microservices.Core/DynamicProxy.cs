using System.Dynamic;
using System.Linq;
using System.Reflection;

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
			var cookie = args.FirstOrDefault(a => a != null && typeof(ICookies).GetTypeInfo().IsAssignableFrom(a.GetType().GetTypeInfo()));
			var dynMsg = new DynamicCallBasedMessage(_message.ToLower(), binder.CallInfo.ArgumentNames.ToArray(), args, (ICookies)cookie);
			if (binder.ReturnType.GetReturnType() == typeof (IMessage))
				result = _host.Handle(dynMsg);
			else
				result = _host.Handle(dynMsg)
				.ContinueWith(t =>
				{
					return t.Result.ValueAs(binder.ReturnType.GetReturnType());
				});
			return true;
		}
	}
}
