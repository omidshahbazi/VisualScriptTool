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
		MethodBase ICompileStrategy.GetInstantiator(Type Type)
		{
			ConstructorInfo[] ctors = Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < ctors.Length; ++i)
			{
				ConstructorInfo ctor = ctors[i];

				if (IsMethodACorrectInstantiator(ctor))
					return ctor;
			}

			MethodInfo[] methods = Type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < methods.Length; ++i)
			{
				MethodInfo method = methods[i];

				if (!method.ReturnType.IsAssignableFrom(Type))
					continue;

				if (IsMethodACorrectInstantiator(method))
					return method;
			}

			return null;
		}

		MemberInfo[] ICompileStrategy.GetMembers(Type Type)
		{
			List<MemberInfo> list = new List<MemberInfo>();

			PropertyInfo[] properties = TypeUtils.GetProperties(Type);
			for (int i = 0; i < properties.Length; ++i)
			{
				PropertyInfo property = properties[i];

				if (!IsSerializableMember(property))
					continue;

				if (property.GetSetMethod() != null)
					list.Add(property);
			}

			FieldInfo[] fields = TypeUtils.GetFields(Type);
			for (int i = 0; i < fields.Length; ++i)
			{
				FieldInfo field = fields[i];

				if (!IsSerializableMember(field))
					continue;

				if (AttributeUtils.GetAttribute<CompilerGeneratedAttribute>(field) == null)
					list.Add(fields[i]);
			}

			return list.ToArray();
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

		private bool IsSerializableMember(MemberInfo Member)
		{
			return (AttributeUtils.GetAttribute<SerializableElementAttribute>(Member) != null);
		}

		private static bool IsMethodACorrectInstantiator(MethodBase Method)
		{
			SerializableInstantiatorAttribute serializableInstantiator = AttributeUtils.GetAttribute<SerializableInstantiatorAttribute>(Method);

			if (serializableInstantiator == null)
				return false;

			ParameterInfo[] parameters = Method.GetParameters();

			if (parameters.Length == 0)
				return true;

			if (serializableInstantiator.DefaultParameters == null || serializableInstantiator.DefaultParameters.Length != parameters.Length)
				return false;

			for (int j = 0; j < parameters.Length; ++j)
				if (parameters[j].ParameterType != serializableInstantiator.DefaultParameters[j].GetType())
					return false;

			return true;
		}
	}
}
