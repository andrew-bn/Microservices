using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DotLiquid;
using Microservices.Core;
using System.Resources;

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
		
		public static HtmlMessage Liquid<T>(this T instance, string resourceName, object model)
		{
			var content = instance.Content(resourceName);
			if (string.IsNullOrEmpty(content))
				return new HtmlMessage("");

			var template = Template.Parse(content);
			return new HtmlMessage(template.Render(Hash.FromAnonymousObject(new { model })));
		}
		
	}
}
