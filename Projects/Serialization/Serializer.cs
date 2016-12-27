// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
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

		public object Deserialize(string Path)
		{
			ISerializeObject data = JSONSerializeObject.Deserialize(System.IO.File.ReadAllText(Path));

			//StoreObject(data, Object);

			return null;
		}

		private void StoreObject(ISerializeObject Parent, object Object)
		{
			List<MemberData> members = new List<MemberData>();

			members.AddRange(GetFields(Object));
			members.AddRange(GetProperties(Object));

			ISerializeArray membersArray = Parent.AddArray("Value");

			for (int i = 0; i < members.Count; ++i)
				StoreMember(membersArray, members[i]);
		}

		private void StoreMember(ISerializeArray MembersArray, MemberData Member)
		{
			ISerializeObject memberObject = MembersArray.AddObject();

			object value = Member.Value;

			System.Type valueType = value.GetType();

			if (valueType.IsArray)
			{
				ISerializeArray membersArray = memberObject.AddArray(Member.Identifier.ToString());

				//System.Array array = (System.Array)Value;

				//for (int i = 0; i < array.Length; ++i)
				//{
				//	object item = array.GetValue(i);
				//	System.Type itemType = item.GetType();

				//	if (IsTypeStorable(itemType))
				//		membersArray.AddItem(array.GetValue(i));
				//	else
				//	{
				//		ISerializeObject memberObject = membersArray.AddObject();
				//		StoreValue(memberObject, itemType, item);
				//	}
				//}
			}
			else if (IsTypeStorable(valueType))
				memberObject.Set(Member.Identifier.ToString(), value);
			//else
			//	StoreObject(Object, Value);
		}

		private void StoreValue(ISerializeObject Object, object Value)
		{
			
		}

		private static bool IsTypeStorable(System.Type Type)
		{
			return (Type.IsPrimitive || Type == typeof(string));
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

					SerializableAttribute serializableAttr = GetAttribute<SerializableAttribute>(property);

					if (serializableAttr == null)
						continue;

					list.Add(new MemberData(Instance, property, serializableAttr));
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

					SerializableAttribute serializableAttr = GetAttribute<SerializableAttribute>(field);

					if (serializableAttr == null)
						continue;

					list.Add(new MemberData(Instance, field, serializableAttr));
				}

				type = type.BaseType;
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
