using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FubuExample.Web.Configuration
{
	public static class TypeExtensions
	{
		public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
		{
			return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
		}

		public static IEnumerable<PropertyInfo> PropertiesWhere(this Type type, Func<PropertyInfo, bool> predicate)
		{
			return type.GetPublicProperties().Where(predicate);
		}

		public static void EachProperty(this Type type, Action<PropertyInfo> action)
		{
			type
				.GetPublicProperties()
				.Each(action);
		}

		public static MethodInfo GetExecuteMethod(this Type t)
		{
			return t.GetMethod("Execute", BindingFlags.Instance | BindingFlags.Public);
		}
	}
}