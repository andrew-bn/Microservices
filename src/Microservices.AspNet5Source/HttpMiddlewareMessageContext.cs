using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microservices.Core;
using Microservices.Core.Messaging;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microservices.AspNet5Source
{

	public class HttpJsonMessage : IMessage
	{
		private const string MicroserviceNameKey = "microservice";
		private const string DefaultMicroserviceName = "host";

		private const string MicroserviceMethodNameKey = "method";
		private const string DefaultMicroserviceMethodName = "index";

		private readonly RouteContext _routeContext;
		private JToken _jsonRequest;

		public HttpJsonMessage(RouteContext routeContext)
		{
			_routeContext = routeContext;
			var microservice = routeContext.RouteData.Values[MicroserviceNameKey]?.ToString() ?? DefaultMicroserviceName;
			var messageName = routeContext.RouteData.Values[MicroserviceMethodNameKey]?.ToString() ?? DefaultMicroserviceMethodName;
			Name = microservice + "." + messageName;
		}
		public HttpJsonMessage(string name, JToken jsonRequest)
		{
			Name = name;
			_jsonRequest = jsonRequest;
		}
		public string Name { get; }

		public ParameterType Type
		{
			get
			{
				if (_jsonRequest.Type == JTokenType.Float)
					return ParameterType.Real;
				if (_jsonRequest.Type == JTokenType.Boolean)
					return ParameterType.Boolean;
				if (_jsonRequest.Type == JTokenType.Integer)
					return ParameterType.Integer;
				if (_jsonRequest.Type == JTokenType.Object)
					return ParameterType.Object;
				if (_jsonRequest.Type == JTokenType.Array)
					return ParameterType.Array;

				return ParameterType.String;
			}
		}

		public object Value
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IEnumerable<IMessageSchema> Parameters
		{
			get
			{
				throw new NotImplementedException();
			}

		}

		public IMessage this[string parameterName]
		{
			get
			{
				return new HttpJsonMessage(parameterName, _jsonRequest[parameterName]);
			}
		}

		internal async Task Prepare()
		{
			var body = await ReadBody(_routeContext.HttpContext);
			var jr = new JsonTextReader(new StringReader(body));
			_jsonRequest = (JObject)JsonSerializer.Create().Deserialize(jr);
		}

		private async Task<string> ReadBody(HttpContext context)
		{
			if (!context.Request.ContentLength.HasValue)
				return string.Empty;

			var data = new byte[context.Request.ContentLength.Value];
			var done = 0;

			while (done < data.Length)
				done += await context.Request.Body.ReadAsync(data, done, data.Length - done);

			return Encoding.UTF8.GetString(data);
		}

		public object ValueAs(Type type)
		{
			return _jsonRequest.ToObject(type);
		}
	}
}
