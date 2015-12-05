using System;
using System.Collections.Generic;
using Microservices.Core;
using Microsoft.AspNet.Http;

namespace Microservices.AspNet5Source
{
	public class HttpBasedCookies : ICookies
	{
		private readonly IResponseCookies _respCookies;
		private Dictionary<string,string> _values = new Dictionary<string, string>();
		public HttpBasedCookies(IReadableStringCollection cookies, IResponseCookies respCookies)
		{
			_respCookies = respCookies;
			foreach (var c in cookies)
				this[c.Key.ToLower()] = c.Value;
		}

		public string this[string key]
		{
			get
			{
				string result;
				_values.TryGetValue(key.ToLower(), out result);
				return result;
			}
			set
			{
				key = key.ToLower();
				if (_values.ContainsKey(key.ToLower()))
					_values[key] = value;
				else
					_values.Add(key, value);
			}
		}

		public IEnumerable<string> Names => _values.Keys;

		public void Append(string key, string value)
		{
			_respCookies.Append(key,value);
			this[key] = value;
		}

		public void Delete(string key)
		{
			_respCookies.Delete(key);
			key = key.ToLower();
			if (_values.ContainsKey(key))
				_values.Remove(key);
		}
	}
}