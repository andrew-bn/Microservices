using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DotLiquid;
using Microservices.Core;

namespace Microservices.LiquidEngine
{
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public static class LiquidRenderer
	{
		static LiquidRenderer()
		{
			Template.AllowAllTypes = true;
		}
		public static HtmlMessage Content<T>(this T instance, string resource, object model)
		{
			return ContentFor<T>(resource, model);
		}
		public static HtmlMessage ContentFor<T>(string resourceName, object model)
		{
			var assembly = typeof(T).GetTypeInfo().Assembly;
			foreach (var a in assembly.GetManifestResourceNames())
			{
				var res = a.ToLower();
				res = res.ToLower().Substring(res.IndexOf(".content") + ".content".Length + 1);
				res = res.Substring(0, res.LastIndexOf("."));
				if (resourceName.ToLower() == res.ToLower())
				{
					using (Stream stream = assembly.GetManifestResourceStream(a))
					using (StreamReader reader = new StreamReader(stream))
					{
						string result = reader.ReadToEnd();
						Template template = Template.Parse(result);
						var content = template.Render(Hash.FromAnonymousObject(new { model }));
						return content;
					}
				}
			}
			return new HtmlMessage("");
		}
	}
}
