﻿// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using System.Reflection;
using VisualScriptTool.Serialization.JSONSerializer;

namespace VisualScriptTool.Serialization
{
	public class Serializer
	{
		public string Serialize(object Object)
		{
			ISerializeObject data = new JSONSerializeObject(null);

			StoreObject(data, Object);

			return data.Content;
		}

		private void StoreObject(ISerializeObject Parent, object Object)
		{
			System.Type type = Object.GetType();
			ISerializeArray membersArray = Parent.AddArray("Value");

			if (type.IsArray)
			{

			}
			else
			{
				FieldInfo[] fields = GetFields(type);

				for (int i = 0; i < fields.Length; ++i)
				{
					FieldInfo field = fields[i];

					StoreMember(membersArray, field.FieldType, field.Name, field.GetValue(Object));
				}

				PropertyInfo[] properties = GetProperties(type);

				for (int i = 0; i < properties.Length; ++i)
				{
					PropertyInfo property = properties[i];

					StoreMember(membersArray, property.PropertyType, property.Name, property.GetValue(Object, null));
				}
			}
		}

		private void StoreMember(ISerializeArray MembersArray, System.Type Type, string Name, object Value)
		{
			ISerializeObject memberObject = MembersArray.AddObject();

			memberObject.Set("Type", Type.FullName);
			memberObject.Set("Name", Name);

			if (Value == null)
				return;

			if (IsTypeStorable(Type))
				memberObject.Set("Value", Value);
			else
				StoreObject(memberObject, Value);

		}

		private static bool IsTypeStorable(System.Type Type)
		{
			return (Type.IsPrimitive || Type == typeof(string));
		}

		private PropertyInfo[] GetProperties(System.Type Type)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();

			while (Type != null)
			{
				PropertyInfo[] properties = Type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

				for (int i = 0; i < properties.Length; ++i)
				{
					PropertyInfo property = properties[i];

					NotSerializableAttribute serializableAttr = GetAttribute<NotSerializableAttribute>(property);

					if (serializableAttr != null)
						continue;

					list.Add(property);
				}

				Type = Type.BaseType;
			}

			return list.ToArray();
		}

		private FieldInfo[] GetFields(System.Type Type)
		{
			List<FieldInfo> list = new List<FieldInfo>();

			while (Type != null)
			{
				FieldInfo[] fields = Type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

				for (int i = 0; i < fields.Length; ++i)
				{
					FieldInfo field = fields[i];

					object[] serializableAttr = field.GetCustomAttributes(typeof(SerializableAttribute), false);

					if (serializableAttr.Length == 0)
						continue;

					list.Add(field);
				}

				Type = Type.BaseType;
			}

			return list.ToArray();
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
