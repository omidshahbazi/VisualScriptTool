// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;

namespace VisualScriptTool.Reflection
{
	public static class TypeUtils
	{
		public static PropertyInfo[] GetProperties(Type Type)
		{
			return GetProperties(Type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		public static PropertyInfo[] GetProperties(Type Type, BindingFlags BindingFlags)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();

			while (Type != null)
			{
				PropertyInfo[] properties = Type.GetProperties(BindingFlags);

				for (int i = 0; i < properties.Length; ++i)
					list.Add(properties[i]);

				Type = Type.BaseType;
			}

			return list.ToArray();
		}

		public static FieldInfo[] GetFields(Type Type)
		{
			return GetFields(Type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		public static FieldInfo[] GetFields(Type Type, BindingFlags BindingFlags)
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
	}
}
