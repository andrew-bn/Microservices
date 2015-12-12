using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Microservices.Core
{
    public static class Extensions
    {
	    public static bool IsPrimitive(this Type type)
	    {
		    return type.GetTypeInfo().IsPrimitive || type == typeof (string) ||
		           type == typeof (decimal) || type == typeof (DateTime) ||
		           type == typeof (DateTimeOffset) || type == typeof (TimeSpan);
	    }

	    public static ParameterType ToParameterType(this Type type)
	    {
			var result = ParameterType.Object;
			if (type == typeof(string) || 
				type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan))
				result = ParameterType.String;
			if (type == typeof(bool))
				result = ParameterType.Integer;
			if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
				result = ParameterType.Real;
			if (type == typeof(int) || type == typeof(long) || type == typeof(short) || type == typeof(byte) ||
				type == typeof(uint) || type == typeof(ulong) || type == typeof(ushort))
				result = ParameterType.Integer;
			if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type))
				result = ParameterType.Array;

		    return result;
	    }

	    public static object ChangeType(this object value, Type type)
	    {
		    if (value == null)
			    return null;
		    return Convert.ChangeType(value, type);
	    }
    }
}
