// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public class Serializer
	{
		private SerializeReferences serializeRefernces = null;
		private DeserializeReferences deserializeRefernces = null;

		public ISerializeObject Serialize(object Instance)
		{
			ISerializeObject data = Factory.Create();

			Serialize(data, Instance);

			return data;
		}

		public void Serialize(ISerializeObject Object, object Instance)
		{
			Reset();

			if (IsPrimitiveType(Instance.GetType()))
				return;

			serializeRefernces.AddInstance(Instance);

			for (int i = 0; i < serializeRefernces.Instances.Length; ++i)
			{
				SerializeReferences.InstanceData instance = serializeRefernces.Instances[i];

				StoreInstance(Object.AddObject(instance.ID), instance);
			}
		}

		public T Deserialize<T>(ISerializeObject Object) where T : new()
		{
			if (Object == null || Object.Count == 0)
				return default(T);

			if (!IsComplexType(typeof(T)))
				return default(T);

			T obj = new T();

			Deserialize(Object, obj);

			return obj;
		}

		public void Deserialize(ISerializeObject Object, object Instance)
		{
			Reset();

			if (Object == null || Object.Count == 0 || Instance == null)
				return;

			if (!IsComplexType(Instance.GetType()))
				return;

			IEnumerator<KeyValuePair<string, object>> it = Object.GetEnumerator();

			while (it.MoveNext())
				deserializeRefernces.AddInstance(it.Current.Key, (ISerializeObject)it.Current.Value);

			deserializeRefernces.Instances[0].Instance = Instance;
			DeserializeInstance(deserializeRefernces.Instances[0]);

			for (int i = 1; i < deserializeRefernces.Instances.Length; ++i)
				DeserializeInstance(deserializeRefernces.Instances[i]);
		}

		private void StoreInstance(ISerializeObject Object, SerializeReferences.InstanceData Instance)
		{
			MemberData[] members = GetMembers(Instance.Instance);

			for (int i = 0; i < members.Length; ++i)
				StoreMember(Object, members[i]);
		}

		private void StoreMember(ISerializeObject Object, MemberData Member)
		{
			string identifier = Member.Identifier.ToString();

			if (Member.Value == null)
			{
				Object.Set(identifier, null);
				return;
			}

			if (Member.Type.IsArray)
			{
				ISerializeArray membersArray = Object.AddArray(identifier);

				System.Array array = (System.Array)Member.Value;

				for (int i = 0; i < array.Length; ++i)
				{
					object item = array.GetValue(i);

					if (item == null)
					{
						membersArray.Add(null);
						continue;
					}

					membersArray.Add(IsPrimitiveType(item.GetType()) ? item : serializeRefernces.GetOrAddInstance(item).ID);
				}
			}
			else if (IsPrimitiveType(Member.Type))
				Object.Set(identifier, Member.Value);
			else
				Object.Set(identifier, serializeRefernces.GetOrAddInstance(Member.Value).ID);
		}

		private void DeserializeInstance(DeserializeReferences.InstanceData Instance)
		{
			MemberData[] members = GetMembers(Instance.Instance);

			for (int i = 0; i < members.Length; ++i)
				SetValue(Instance.SerializeObject, members[i]);
		}

		private void SetValue(ISerializeObject Object, MemberData Member)
		{
			string identifier = Member.Identifier.ToString();

			if (Member.Type.IsArray)
			{
				ISerializeArray dataArray = Object.Get<ISerializeArray>(identifier);

				if (dataArray == null)
					return;


			}
			else
			{
				object value = null;

				if (Object.Contains(identifier))
					value = Object[identifier];

				if (DeserializeReferences.InstanceData.IsReferenceIDFormat(value.ToString()))
				{
					DeserializeReferences.InstanceData referenceIntance = deserializeRefernces.GetInstance(value.ToString());

					if (!referenceIntance.HasReference)
						referenceIntance.Instance = Activator.CreateInstance(Member.Type);

					value = referenceIntance.Instance;
				}

				Member.Value = Convert.ChangeType(value, Member.Type);
			}
		}

		private void Reset()
		{
			serializeRefernces = new SerializeReferences();
			deserializeRefernces = new DeserializeReferences();
		}

		private static bool IsPrimitiveType(System.Type Type)
		{
			return (Type.IsPrimitive || Type == typeof(string));
		}

		private static bool IsComplexType(System.Type Type)
		{
			return (!Type.IsPrimitive && Type != typeof(string));
		}

		private static SerializableAttribute GetSerializableAttribute(MemberInfo Member)
		{
			SerializableAttribute serializableAttr = GetAttribute<SerializableAttribute>(Member);

			if (serializableAttr != null)
				return serializableAttr;

			if (Member.Module.Name.StartsWith("System."))
				return new SerializableAttribute((Member.DeclaringType.FullName + "::" + Member.Name).GetHashCode());

			return null;
		}

		private static MemberData[] GetMembers(object Instance)
		{
			List<MemberData> members = new List<MemberData>();

			members.AddRange(GetFields(Instance));
			members.AddRange(GetProperties(Instance));

			return members.ToArray();
		}

		private static MemberData[] GetProperties(object Instance)
		{
			List<MemberData> list = new List<MemberData>();

			Type type = Instance.GetType();

			while (type != null)
			{
				PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

				for (int i = 0; i < properties.Length; ++i)
				{
					PropertyInfo property = properties[i];

					SerializableAttribute serializableAttr = GetSerializableAttribute(property);

					if (serializableAttr == null || property.GetSetMethod(true) == null)
						continue;

					list.Add(new MemberData(Instance, property, serializableAttr));
				}

				type = type.BaseType;
			}

			return list.ToArray();
		}

		private static MemberData[] GetFields(object Instance)
		{
			List<MemberData> list = new List<MemberData>();

			Type type = Instance.GetType();

			while (type != null)
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

				for (int i = 0; i < fields.Length; ++i)
				{
					FieldInfo field = fields[i];

					SerializableAttribute serializableAttr = GetSerializableAttribute(field);

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
