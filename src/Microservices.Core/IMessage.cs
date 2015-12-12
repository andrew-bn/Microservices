using System;
using System.Collections.Generic;
using Microservices.Core.Messaging;
using Microsoft.AspNet.Http;

namespace Microservices.Core
{
	public interface ICookies
	{
		string this[string key] { get; }
		IEnumerable<string> Names { get; }
		void Append(string key, string value);
		void Delete(string key);
	}

	public interface IMessageValue : IMessageSchema
	{
		IMessageValue this[string parameterName] { get; }
		object Value { get; }
		object ValueAs(Type type);
		string ToResponseString();
	}

	public interface IMessage : IMessageValue
	{
		ICookies Cookies { get; }
	}
}