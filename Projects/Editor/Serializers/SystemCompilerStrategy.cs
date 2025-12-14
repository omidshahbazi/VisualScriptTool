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
	class SystemCompilerStrategy : ISerializationCompileStrategy
	{
		MethodBase ISerializationCompileStrategy.GetInstantiator(Type Type)
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
					if (!((ISerializationCompileStrategy)this).IsPrimitive(parameters[j].ParameterType))
					{
						isAppropriate = false;
						break;
					}

				if (isAppropriate)
					return ctor;
			}

			return null;
		}

		MethodInfo ISerializationCompileStrategy.GetPreSerialize(Type Type)
		{
			return null;
		}

		MethodInfo ISerializationCompileStrategy.GetPostSerialize(Type Type)
		{
			return null;
		}

		MethodInfo ISerializationCompileStrategy.GetPreDeserialize(Type Type)
		{
			return null;
		}

		MethodInfo ISerializationCompileStrategy.GetPostDeserialize(Type Type)
		{
			return null;
		}

		MemberInfo[] ISerializationCompileStrategy.GetMembers(Type Type)
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

		int ISerializationCompileStrategy.GetMemberID(MemberInfo Member, int DefaultID)
		{
			return DefaultID;
		}

		string ISerializationCompileStrategy.GetInstantiatorParameterDefaultValue(MethodBase Method, uint Index)
		{
			ParameterInfo parameter = Method.GetParameters()[Index];

			return parameter.ParameterType.GetDefaultValue().ToString();
		}

		string ISerializationCompileStrategy.GetMemberDefaultValue(MemberInfo Member)
		{
			Type type = (Member is FieldInfo ? ((FieldInfo)Member).FieldType : ((PropertyInfo)Member).PropertyType);

			if (type == typeof(string))
				return "\"\"";

			return type.GetDefaultValue().ToString();
		}

		bool ISerializationCompileStrategy.IsPrimitive(Type Type)
		{
			return (Type.IsPrimitive || Type == typeof(string));
		}

		bool ISerializationCompileStrategy.IsArray(Type Type)
		{
			return Type.IsArray();
		}

		bool ISerializationCompileStrategy.IsList(Type Type)
		{
			return Type.IsList();
		}

		bool ISerializationCompileStrategy.IsMap(Type Type)
		{
			return (Type.GetInterface(typeof(IDictionary).FullName) != null);
		}
	}
}