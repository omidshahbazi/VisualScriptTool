// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public class DefaultStrategy : IStrategy
	{
		bool IStrategy.CanHandle(Type Type)
		{
			return (!Type.Module.Name.StartsWith("System."));
        }

		bool IStrategy.IsPrimitiveType(Type Type)
		{
			return (Type.IsPrimitive || Type == typeof(string));
		}

		MemberData[] IStrategy.GetMembers(object Instance)
		{
			List<MemberData> members = new List<MemberData>();

			members.AddRange(GetFields(Instance));
			members.AddRange(GetProperties(Instance));

			return members.ToArray();
		}

		private MemberData[] GetProperties(object Instance)
		{
			List<MemberData> list = new List<MemberData>();

			Type type = Instance.GetType();

			while (type != null)
			{
				PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

				for (int i = 0; i < properties.Length; ++i)
				{
					PropertyInfo property = properties[i];

					SerializableAttribute serializableAttr = GetSerializableAttribute(property);

					if (serializableAttr == null || property.GetSetMethod(true) == null)
						continue;

					list.Add(new MemberData(Instance, property, serializableAttr.Identifier));
				}

				type = type.BaseType;
			}

			return list.ToArray();
		}

		private MemberData[] GetFields(object Instance)
		{
			List<MemberData> list = new List<MemberData>();

			Type type = Instance.GetType();

			while (type != null)
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

				for (int i = 0; i < fields.Length; ++i)
				{
					FieldInfo field = fields[i];

					SerializableAttribute serializableAttr = GetSerializableAttribute(field);

					if (serializableAttr == null)
						continue;

					list.Add(new MemberData(Instance, field, serializableAttr.Identifier));
				}

				type = type.BaseType;
			}

			return list.ToArray();
		}

		private static SerializableAttribute GetSerializableAttribute(MemberInfo Member)
		{
			SerializableAttribute serializableAttr = GetAttribute<SerializableAttribute>(Member);

			if (serializableAttr != null)
				return serializableAttr;

			return null;
		}

		private static T GetAttribute<T>(MemberInfo Member) where T : System.Attribute
		{
			object[] attr = Member.GetCustomAttributes(typeof(T), false);

			if (attr.Length == 0)
				return null;

			return (T)attr[0];
		}
	}
}