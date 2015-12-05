using System.IO;
using System.Reflection;

namespace System.Resources
{
    public static class ContentManagerExtension
    {
		public static string Content<T>(this T instance, string resource)
		{

			return ContentFor<T>(resource);
		}
		public static string ContentFor<T>(string resourceName)
		{
			var assembly = typeof(T).GetTypeInfo().Assembly;
			foreach (var a in assembly.GetManifestResourceNames())
			{
				var res = a.ToLower();
				res = res.ToLower().Substring(res.IndexOf(".content") + ".content".Length + 1);
				res = res.Substring(0, res.LastIndexOf("."));
				if (resourceName.ToLower() == res.ToLower())
				{
					using (var stream = assembly.GetManifestResourceStream(a))
					using (var reader = new StreamReader(stream))
					{
						return reader.ReadToEnd();
					}
				}
			}
			return null;
		}
	}
}
