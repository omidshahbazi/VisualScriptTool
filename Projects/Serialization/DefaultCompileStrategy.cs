// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VisualScriptTool.Reflection;

namespace VisualScriptTool.Serialization
{
	public class DefaultCompileStrategy : ICompileStrategy
	{
		MemberInfo[] ICompileStrategy.GetMembers(Type Type)
		{
			List<MemberInfo> list = new List<MemberInfo>();

			PropertyInfo[] properties = TypeUtils.GetProperties(Type, BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < properties.Length; ++i)
			{
				PropertyInfo property = properties[i];

				if (property.GetSetMethod() != null)
					list.Add(property);
			}

			FieldInfo[] fields = TypeUtils.GetFields(Type, BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; ++i)
			{
				FieldInfo field = fields[i];

				if (AttributeUtils.GetAttribute<CompilerGeneratedAttribute>(field) == null)
					list.Add(fields[i]);
			}

			return list.ToArray();
		}

		bool ICompileStrategy.IsSerializableMember(MemberInfo Member)
		{
			return (AttributeUtils.GetAttribute<SerializableElementAttribute>(Member) != null);
		}

		int ICompileStrategy.GetMemberID(MemberInfo Member, int DefaultID)
		{
			SerializableElementAttribute serializable = AttributeUtils.GetAttribute<SerializableElementAttribute>(Member);

			if (serializable == null)
				return DefaultID;

			return serializable.ID;
		}

		string ICompileStrategy.GetMemberDefaultValue(MemberInfo Member)
		{
			SerializableElementAttribute serializable = AttributeUtils.GetAttribute<SerializableElementAttribute>(Member);

			if (serializable == null)
				return string.Empty;

			return serializable.GetDefaultValueAsString();
		}

		bool ICompileStrategy.IsPrimitive(Type Type)
		{
			return (Type.IsPrimitive || Type == typeof(string));
		}

		bool ICompileStrategy.IsArray(Type Type)
		{
			return Type.IsArray;
		}

		bool ICompileStrategy.IsList(Type Type)
		{
			return (Type.GetInterface(typeof(IList).FullName) != null);
		}

		bool ICompileStrategy.IsMap(Type Type)
		{
			return (Type.GetInterface(typeof(IDictionary).FullName) != null);
		}
	}
}
