using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Core;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microservices.AspNet5Source
{
	public class HttpMiddlewareMessageContext : IMessageContext, IMessageResponse, IMessageRequest
	{
		private const string MicroserviceNameKey = "microservice";
		private const string DefaultMicroserviceName = "host";

		private const string MicroserviceMethodNameKey = "method";
		private const string DefaultMicroserviceMethodName = "index";

		private JObject _jsonRequest;
		private readonly RouteContext _routeContext;
		public IMessageResponse Response { get; }

		internal async Task Prepare()
		{
			var body = await ReadBody(_routeContext.HttpContext);
			var jr = new JsonTextReader(new StringReader(body));
			var _jsonRequest = (JObject)JsonSerializer.Create().Deserialize(jr);
		}

		public IMessageRequest Request { get; }
		public MessageSource Source { get; }

		public string MicroserviceName { get; }
		public string MicroserviceMethod { get; }

		public HttpMiddlewareMessageContext(MessageSource source, RouteContext routeContext)
		{
			_routeContext = routeContext;
			Response = this;
			Request = this;
			Source = source;

			MicroserviceName = routeContext.RouteData.Values[MicroserviceNameKey]?.ToString() ?? DefaultMicroserviceName;
			MicroserviceMethod = routeContext.RouteData.Values[MicroserviceMethodNameKey]?.ToString() ?? DefaultMicroserviceMethodName;

		}

		public Task WriteString(string str)
		{
			return _routeContext.HttpContext.Response.WriteAsync(str);
		}

		public T ReadParameter<T>(RequestParameter parameter)
		{
			return _jsonRequest.Value<T>(parameter.Name);
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
	}
}
