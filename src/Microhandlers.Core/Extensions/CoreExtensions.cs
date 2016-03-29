using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Microhandlers.Core.Extensions
{
    public static class CoreExtensions
    {
	    private static Type[] _integerTypes = new[]
	    {
			typeof(int), typeof(uint),
			typeof(short), typeof(ushort),
			typeof(byte), typeof(sbyte),
			typeof(long), typeof(ulong),
		};

		private static Type[] _floatTypes = new[]
	    {
			typeof(decimal), typeof(float), typeof(double)
		};

		public static IEnumerable<Type> IntegerTypes(this Type type)
		{
			return _integerTypes;
		}

		public static IEnumerable<Type> FloatTypes(this Type type)
		{
			return _floatTypes;
		}

		public static bool IsInteger(this Type type)
		{
			return IsPrimitive(type) && IntegerTypes(type).Contains(type);
		}

		public static bool IsFloat(this Type type)
		{
			return IsPrimitive(type) && FloatTypes(type).Contains(type);
		}

		public static bool IsString(this Type type)
		{
			return type == typeof(string);
		}

		public static bool IsBool(this Type type)
		{
			return type == typeof(bool);
		}

		public static bool IsArray(this Type type)
		{
			return type.IsArray;
		}

		public static bool IsNumberic(this Type type)
		{
			return IsInteger(type) || IsFloat(type);
		}

		public static bool IsPrimitive(this Type type)
		{
			return type.GetTypeInfo().IsPrimitive || type == typeof(string) ||
				   type == typeof(decimal) || type == typeof(DateTime) ||
				   type == typeof(DateTimeOffset) || type == typeof(TimeSpan);
		}
		
		public static bool IsTask(this Type type)
		{
			return typeof(Task).IsAssignableFrom(type);
		}

		public static bool IsGenericType(this Type type)
		{
			return type.GetGenericArguments().Length > 0;
		}

		public static Type GetReturnType(this Type type)
		{
			var isTask = type.IsTask();
			if (isTask && type.IsGenericType())
				return type.GetGenericArguments()[0];
			if (isTask)
				return typeof(void);

			return type;
		}
	}
}
