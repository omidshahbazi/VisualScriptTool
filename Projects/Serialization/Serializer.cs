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

			if (type.IsArray)
			{

			}
			else
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

				for (int i = 0; i < fields.Length; ++i)
				{
					FieldInfo field = fields[i];

					object[] serializableAttr = field.GetCustomAttributes(typeof(SerializableAttribute), false);
				}

				List<PropertyInfo> properties = new List<PropertyInfo>();

				properties.AddRange(type.GetProperties(BindingFlags.Instance | BindingFlags.Public));
				properties.AddRange(type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic));

				for (int i = 0; i < properties.Count; ++i)
				{
					PropertyInfo property = properties[i];

					object[] serializableAttr = property.GetCustomAttributes(typeof(SerializableAttribute), false);

					if (serializableAttr.Length == 0)
						continue;


				}
			}

			return "";
		}
	}
}
