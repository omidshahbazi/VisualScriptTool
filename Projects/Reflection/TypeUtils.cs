// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace VisualScriptTool.Reflection
{
	public static class TypeUtils
	{
		public static Type[] GetDrievedTypesOf<T>(bool AllAssemblies = false) where T : class
		{
			List<Type> retTypes = new List<Type>();

			List<Assembly> assemblies = new List<Assembly>();
			if (AllAssemblies)
				assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
			else
				assemblies.Add(Assembly.GetCallingAssembly());

			for (int i = 0; i < assemblies.Count; ++i)
				retTypes.AddRange(assemblies[i].GetDrievedTypesOf<T>());

			return retTypes.ToArray();
		}

		public static Type[] GetDrievedTypesOf<T>(this Assembly A) where T : class
		{
			List<Type> retTypes = new List<Type>();

			Type[] allTypes = A.GetTypes();
			for (int i = 0; i < allTypes.Length; ++i)
			{
				Type type = allTypes[i];

				if (type.IsSubclassOf(typeof(T)))
					retTypes.Add(type);
			}

			return retTypes.ToArray();
		}

		public static PropertyInfo[] GetProperties(this Type Type)
		{
			return GetProperties(Type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		public static PropertyInfo[] GetProperties(this Type Type, BindingFlags BindingFlags)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();

			PropertyInfo[] properties = Type.GetProperties(BindingFlags);

			for (int i = 0; i < properties.Length; ++i)
			{
				if (list.Contains(properties[i]))
					continue;

				list.Add(properties[i]);
			}

			return list.ToArray();
		}

		public static FieldInfo[] GetFields(this Type Type)
		{
			return GetFields(Type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		public static FieldInfo[] GetFields(this Type Type, BindingFlags BindingFlags)
		{
			List<FieldInfo> list = new List<FieldInfo>();

			while (Type != null)
			{
				FieldInfo[] fields = Type.GetFields(BindingFlags);

				for (int i = 0; i < fields.Length; ++i)
					list.Add(fields[i]);

				Type = Type.BaseType;
			}

			return list.ToArray();
		}

		public static object GetDefaultValue(this Type Type)
		{
			if (Type == typeof(bool))
				return false;

			if (Type == typeof(byte) ||
				Type == typeof(sbyte) ||
				Type == typeof(short) ||
				Type == typeof(ushort) ||
				Type == typeof(int) ||
				Type == typeof(uint) ||
				Type == typeof(long) ||
				Type == typeof(ulong) ||
				Type == typeof(float) ||
				Type == typeof(double) ||
				Type == typeof(decimal))
				return 0;

			if (Type == typeof(char))
				return '\0';

			if (Type == typeof(string))
				return string.Empty;

			if (Type == typeof(object))
				return "null";

			if (Type.IsEnum)
				return Enum.GetValues(Type).GetValue(0);

			if (Type.IsGenericType && Type.GetGenericTypeDefinition() == typeof(Nullable<>))
				Type = Type.GetProperty("Value").PropertyType;

			return Type.IsValueType ? Activator.CreateInstance(Type) : null;
		}

		public static Type GetArrayElementType(this Type Type)
		{
			return (Type.HasElementType ? Type.GetElementType() : null);
		}

		public static Type GetListElementType(this Type Type)
		{
			while (Type != null)
			{
				if (Type.GetGenericArguments().Length != 0)
					return Type.GetGenericArguments()[0];

				Type = Type.BaseType;
			}

			return null;
		}

		public static bool IsArray(this Type Type)
		{
			return Type.IsArray;
		}

		public static bool IsList(this Type Type)
		{
			return (Type.GetInterface(typeof(IList).FullName) != null);
		}

		public static bool IsArrayOrList(this Type Type)
		{
			return (Type.IsArray() || Type.IsList());
		}
	}
}
