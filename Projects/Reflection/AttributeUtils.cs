// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Reflection;

namespace VisualScriptTool.Reflection
{
	public static class AttributeUtils
	{
		public static T GetAttribute<T>(Type Type) where T : Attribute
		{
			object[] attributes = Type.GetCustomAttributes(typeof(T), true);

			if (attributes == null || attributes.Length == 0)
				return null;

			return (T)attributes[0];
		}

		public static T GetAttribute<T>(MemberInfo Member) where T : Attribute
		{
			object[] attributes = Member.GetCustomAttributes(typeof(T), true);

			if (attributes == null || attributes.Length == 0)
				return null;

			return (T)attributes[0];
		}

		public static T GetAttribute<T>(object[] Attributes) where T : Attribute
		{
			if (Attributes == null)
				return null;

			for (int i = 0; i < Attributes.Length; ++i)
				if (Attributes[i] is T)
					return (T)Attributes[i];

			return null;
		}
	}
}
