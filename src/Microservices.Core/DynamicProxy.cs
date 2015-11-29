using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public class DynamicMessage : IMessage
	{
		public string Microservice { get; set; }

		public string MessageName { get; set; }
		public Dictionary<string,object> Parameters { get; set; }

		public bool IsObject
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public object Value
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public MessageObjectType ObjectType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IMessageObject this[int parameterIndex]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IMessageObject this[string parameterName]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public object ReadParameter(Type type, RequestParameter parameter)
		{
			return null;
		}

		public T ValueAs<T>()
		{
			throw new NotImplementedException();
		}

		public object ValueAs(Type type)
		{
			throw new NotImplementedException();
		}
	}
	public class DynamicMessageContext : IMessageContext
	{
		public IMicroservicesHost Host { get; set; }
		public IMessage Request { get; set; }
		public IMessageResponse Response { get; set; }
		public IMessageSource Source { get; set; }
	}

	public class DynamicProxy: DynamicObject, IMessageSource
	{
		private string _microservice = "";
		private string _lastPart = "";
		public IMicroservicesHost Host { get; }

	    public DynamicProxy(IMicroservicesHost host)
	    {
		    Host = host;
	    }

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (_microservice.Length != 0)
				_microservice += ".";
			_microservice += _lastPart;
			_lastPart = binder.Name;
			result = this;
			return true;
		}

		public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
		{
			var message = new DynamicMessage();
			message.Microservice = _microservice;
			message.MessageName = _lastPart;
			message.Parameters = new Dictionary<string, object>();
			var i = 0;
			foreach(var a in binder.CallInfo.ArgumentNames)
			{
				message.Parameters.Add(a, args[i]);
				i++;
			}
			result = Host.Process(new DynamicMessageContext
			{
				Host = this.Host,
				Request = message,
				Response = null,
				Source = this
			});
			return true;
		}
	}
}
