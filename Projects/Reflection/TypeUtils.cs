// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace VisualScriptTool.Reflection
{
	public static class TypeUtils
	{
		public static Type[] GetDrievedTypesOf<T>() where T : class
		{
			Type baseClassType = typeof(T);

			List<Type> retTypes = new List<Type>();

			Type[] types = Assembly.GetCallingAssembly().GetTypes();

			for (int i = 0; i < types.Length; ++i)
			{
				Type type = types[i];

				if (type.IsSubclassOf(baseClassType))
					retTypes.Add(type);
			}

			if (retTypes.Count == 0)
				return null;

			return retTypes.ToArray();
		}

		public static PropertyInfo[] GetAllProperties(this Type Type)
		{
			return GetAllProperties(Type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		public static PropertyInfo[] GetAllProperties(this Type Type, BindingFlags BindingFlags)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();

			//while (Type != null)
			//{
				PropertyInfo[] properties = Type.GetProperties(BindingFlags);

				for (int i = 0; i < properties.Length; ++i)
				{
					if (list.Contains(properties[i]))
						continue;

					list.Add(properties[i]);
				}

				//Type = Type.BaseType;
			//}

			return list.ToArray();
		}

		public static FieldInfo[] GetAllFields(this Type Type)
		{
			return GetAllFields(Type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		public static FieldInfo[] GetAllFields(this Type Type, BindingFlags BindingFlags)
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
			if (Type == typeof(string))
				return string.Empty;

			if (Type == typeof(bool))
				return false;

			if (Type == typeof(int))
				return 0;

			if (Type == typeof(float))
				return 0.0F;

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
