// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
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

			PropertyInfo[] properties = TypeUtils.GetProperties(Type);
			for (int i = 0; i < properties.Length; ++i)
			{
				PropertyInfo property = properties[i];

				if (property.GetSetMethod() != null)
					list.Add(property);
			}

			FieldInfo[] fields = TypeUtils.GetFields(Type);
			for (int i = 0; i < fields.Length; ++i)
			{
				FieldInfo field = fields[i];

				if (AttributeUtils.GetAttribute<CompilerGeneratedAttribute>(field) == null)
					list.Add(fields[i]);
			}

			return list.ToArray();
		}
	}
}
