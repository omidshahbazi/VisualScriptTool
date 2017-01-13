// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VisualScriptTool.Reflection;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor.Serializers
{
	class SystemCompilerStrategy : ICompileStrategy
	{
		MethodBase ICompileStrategy.GetInstantiator(Type Type)
		{
			ConstructorInfo[] ctors = Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < ctors.Length; ++i)
			{
				ConstructorInfo ctor = ctors[i];

				ParameterInfo[] parameters = ctor.GetParameters();

				if (parameters.Length == 0)
					return ctor;

				bool isAppropriate = true;
				for (uint j = 0; j < parameters.Length; ++j)
					if (!((ICompileStrategy)this).IsPrimitive(parameters[j].ParameterType))
					{
						isAppropriate = false;
						break;
					}

				if (isAppropriate)
					return ctor;
			}

			return null;
		}

		MethodInfo ICompileStrategy.GetPreSerialize(Type Type)
		{
			return null;
		}

		MethodInfo ICompileStrategy.GetPostSerialize(Type Type)
		{
			return null;
		}

		MethodInfo ICompileStrategy.GetPreDeserialize(Type Type)
		{
			return null;
		}

		MethodInfo ICompileStrategy.GetPostDeserialize(Type Type)
		{
			return null;
		}

		MemberInfo[] ICompileStrategy.GetMembers(Type Type)
		{
			List<MemberInfo> list = new List<MemberInfo>();

			PropertyInfo[] properties = Type.GetAllProperties(BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < properties.Length; ++i)
			{
				PropertyInfo property = properties[i];

				if (property.GetSetMethod() != null)
					list.Add(property);
			}

			FieldInfo[] fields = Type.GetAllFields(BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; ++i)
			{
				FieldInfo field = fields[i];

				if (AttributeUtils.GetAttribute<CompilerGeneratedAttribute>(field) == null)
					list.Add(fields[i]);
			}

			return list.ToArray();
		}

		int ICompileStrategy.GetMemberID(MemberInfo Member, int DefaultID)
		{
			return DefaultID;
		}

		string ICompileStrategy.GetInstantiatorParameterDefaultValue(MethodBase Method, uint Index)
		{
			ParameterInfo parameter = Method.GetParameters()[Index];

			return parameter.ParameterType.GetDefaultValue().ToString();
		}

		string ICompileStrategy.GetMemberDefaultValue(MemberInfo Member)
		{
			Type type = (Member is FieldInfo ? ((FieldInfo)Member).FieldType : ((PropertyInfo)Member).PropertyType);

			if (type == typeof(string))
				return "\"\"";

			return type.GetDefaultValue().ToString();
		}

		bool ICompileStrategy.IsPrimitive(Type Type)
		{
			return (Type.IsPrimitive || Type == typeof(string));
		}

		bool ICompileStrategy.IsArray(Type Type)
		{
			return Type.IsArray();
		}

		bool ICompileStrategy.IsList(Type Type)
		{
			return Type.IsList();
		}

		bool ICompileStrategy.IsMap(Type Type)
		{
			return (Type.GetInterface(typeof(IDictionary).FullName) != null);
		}
	}
}