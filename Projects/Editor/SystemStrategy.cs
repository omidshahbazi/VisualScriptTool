// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
{
	public class SystemStrategy : IStrategy
	{
		bool IStrategy.CanHandle(Type Type)
		{
			return (Type.Module.Name.StartsWith("System."));
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
				PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

				for (int i = 0; i < properties.Length; ++i)
				{
					PropertyInfo property = properties[i];

					if (property.GetSetMethod(true) == null)
						continue;

					list.Add(new MemberData(Instance, property, GetIdentifier(property)));
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
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);

				for (int i = 0; i < fields.Length; ++i)
				{
					FieldInfo field = fields[i];

					list.Add(new MemberData(Instance, field, GetIdentifier(field)));
				}

				type = type.BaseType;
			}

			return list.ToArray();
		}

		private static int GetIdentifier(MemberInfo Member)
		{
			return (Member.DeclaringType.FullName + "::" + Member.Name).GetHashCode();
		}
	}
}