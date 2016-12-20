// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public class Serializer
	{
		public string Serialize(object Object)
		{
			System.Type type = Object.GetType();

			ISerializeArray membersArray = new JSONSerializer.JSONSerializeArray();

			if (type.IsArray)
			{

			}
			else
			{
				FieldInfo[] fields = GetFields(type);

				for (int i = 0; i < fields.Length; ++i)
				{
					FieldInfo field = fields[i];

					if (field.FieldType.IsPrimitive)
						StoreMember(membersArray, field.FieldType, field.Name, field.GetValue(Object));
				}

				PropertyInfo[] properties = GetProperties(type);

				for (int i = 0; i < properties.Length; ++i)
				{
					PropertyInfo property = properties[i];

					if (property.PropertyType.IsPrimitive)
						StoreMember(membersArray, property.PropertyType, property.Name, property.GetValue(Object, null));
				}
			}

			return "";
		}

		private void StoreMember(ISerializeArray MembersArray, System.Type Type, string Name, object Value)
		{
			ISerializeObject memberObject = MembersArray.AddObject();

			memberObject.Set("Type", Type.FullName);
			memberObject.Set("Name", Name);
			memberObject.Set("Value", Value);
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

					object[] serializableAttr = property.GetCustomAttributes(typeof(SerializableAttribute), false);

					if (serializableAttr.Length == 0)
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
	}
}
