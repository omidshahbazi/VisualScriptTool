// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VisualScriptTool.Reflection;

namespace VisualScriptTool.Serialization
{
	public class DefaultCompileStrategy : ISerializationCompileStrategy
	{
		MethodBase ISerializationCompileStrategy.GetInstantiator(Type Type)
		{
			ConstructorInfo[] ctors = Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			if (!Type.IsAbstract)
				for (int i = 0; i < ctors.Length; ++i)
				{
					ConstructorInfo ctor = ctors[i];

					if (IsMethodACorrectInstantiator(ctor, 0))
						return ctor;
				}

			MethodInfo[] methods = Type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < methods.Length; ++i)
			{
				MethodInfo method = methods[i];

				if (!method.ReturnType.IsAssignableFrom(Type))
					continue;

				ParameterInfo[] parameters = method.GetParameters();
				if (parameters.Length == 0 || parameters[0].ParameterType != typeof(Type))
					continue;

				if (IsMethodACorrectInstantiator(method, 1))
					return method;
			}

			if (!Type.IsAbstract)
			{
				for (int i = 0; i < ctors.Length; ++i)
				{
					ConstructorInfo ctor = ctors[i];

					if (ctor.GetParameters().Length == 0)
						return ctor;
				}
			}

			return null;
		}

		MethodInfo ISerializationCompileStrategy.GetPreSerialize(Type Type)
		{
			return GetCallbackMethod<SerializablePreSerializeAttribute>(Type, BindingFlags.Instance | BindingFlags.Public);
		}

		MethodInfo ISerializationCompileStrategy.GetPostSerialize(Type Type)
		{
			return GetCallbackMethod<SerializablePostSerializeAttribute>(Type, BindingFlags.Instance | BindingFlags.Public);
		}

		MethodInfo ISerializationCompileStrategy.GetPreDeserialize(Type Type)
		{
			return GetCallbackMethod<SerializablePreDeserializeAttribute>(Type, BindingFlags.Instance | BindingFlags.Public);
		}

		MethodInfo ISerializationCompileStrategy.GetPostDeserialize(Type Type)
		{
			return GetCallbackMethod<SerializablePostDeserializeAttribute>(Type, BindingFlags.Instance | BindingFlags.Public);
		}

		MemberInfo[] ISerializationCompileStrategy.GetMembers(Type Type)
		{
			List<MemberInfo> list = new List<MemberInfo>();

            PropertyInfo[] properties = TypeUtils.GetProperties(Type, BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < properties.Length; ++i)
			{
				PropertyInfo property = properties[i];

				if (!IsSerializableMember(property))
					continue;

				if (property.GetSetMethod() != null)
					list.Add(property);
			}

            FieldInfo[] fields = TypeUtils.GetFields(Type, BindingFlags.Instance | BindingFlags.Public);
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

		int ISerializationCompileStrategy.GetMemberID(MemberInfo Member, int DefaultID)
		{
			SerializableElementAttribute serializable = AttributeUtils.GetAttribute<SerializableElementAttribute>(Member);

			if (serializable == null)
				return DefaultID;

			return serializable.ID;
		}

		string ISerializationCompileStrategy.GetInstantiatorParameterDefaultValue(MethodBase Method, uint Index)
		{
			SerializableInstantiatorAttribute attributes = AttributeUtils.GetAttribute<SerializableInstantiatorAttribute>(Method);

			return attributes.GetDefaultParameterAsString(Index);
		}

		string ISerializationCompileStrategy.GetMemberDefaultValue(MemberInfo Member)
		{
			SerializableElementAttribute serializable = AttributeUtils.GetAttribute<SerializableElementAttribute>(Member);

			return serializable.GetDefaultValueAsString();
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

		private bool IsSerializableMember(MemberInfo Member)
		{
			return (AttributeUtils.GetAttribute<SerializableElementAttribute>(Member) != null);
		}

		private static bool IsMethodACorrectInstantiator(MethodBase Method, uint ConstantParameterCount)
		{
			SerializableInstantiatorAttribute serializableInstantiator = AttributeUtils.GetAttribute<SerializableInstantiatorAttribute>(Method);

			if (serializableInstantiator == null)
				return false;

			ParameterInfo[] parameters = Method.GetParameters();

			if (parameters.Length == ConstantParameterCount)
				return true;

			if (serializableInstantiator.DefaultParameters == null || serializableInstantiator.DefaultParameters.Length != parameters.Length - ConstantParameterCount)
				return false;

			//for (uint j = 0; j < parameters.Length - ConstantParameterCount; ++j)
			//	if (parameters[j + ConstantParameterCount].ParameterType != serializableInstantiator.DefaultParameters[j].GetType())
			//		return false;

			return true;
		}

		private static MethodInfo GetCallbackMethod<T>(Type Type, BindingFlags BindingFlag) where T : Attribute
		{
			MethodInfo[] methods = Type.GetMethods(BindingFlag);
			for (int i = 0; i < methods.Length; ++i)
			{
				MethodInfo method = methods[i];

				if (method.ReturnType == null)
					continue;

				ParameterInfo[] parameters = method.GetParameters();
				if (parameters.Length != 0)
					continue;

				if (AttributeUtils.GetAttribute<T>(method) != null)
					return method;
			}

			return null;
		}
	}
}
