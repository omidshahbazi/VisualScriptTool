// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public class Serializer
	{
		public class ObjectFactoryList : List<IObjectFactory>
		{ }

		private SerializeReferences serializeRefernces = null;
		private DeserializeReferences deserializeRefernces = null;

		public ObjectFactoryList ObjectFactories
		{
			get;
			private set;
		}

		public Serializer()
		{
			ObjectFactories = new ObjectFactoryList();
			ObjectFactories.Add(new DefaultObjectFactory());
		}

		public ISerializeObject Serialize(object Instance)
		{
			ISerializeObject data = Creator.Create();

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

		public T Deserialize<T>(ISerializeObject Object)
		{
			if (Object == null || Object.Count == 0)
				return default(T);

			if (!IsComplexType(typeof(T)))
				return default(T);

			object obj = Instantiate(typeof(T));

			Deserialize(Object, obj);

			return (T)obj;
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
				{
					Member.Value = null;
					return;
				}

				Type itemType = Type.GetType(Member.Type.FullName.Replace("[]", ""));

				Array array = Array.CreateInstance(itemType, dataArray.Count);

				for (uint i = 0; i < dataArray.Count; ++i)
				{
					object value = dataArray[i];

					if (value == null)
						continue;

					if (DeserializeReferences.InstanceData.IsReferenceIDFormat(value.ToString()))
						value = CreateReferencedValue(value.ToString(), itemType);

					array.SetValue(value, i);
				}

				Member.Value = array;
			}
			else
			{
				object value = null;

				if (Object.Contains(identifier))
					value = Object[identifier];

				if (DeserializeReferences.InstanceData.IsReferenceIDFormat(value.ToString()))
					value = CreateReferencedValue(value.ToString(), Member.Type);

				Member.Value = Convert.ChangeType(value, Member.Type);
			}
		}

		private object CreateReferencedValue(string ID, Type Type)
		{
			DeserializeReferences.InstanceData referenceIntance = deserializeRefernces.GetInstance(ID);

			if (!referenceIntance.HasReference)
				referenceIntance.Instance = Instantiate(Type);

			return referenceIntance.Instance;
		}

		private void Reset()
		{
			serializeRefernces = new SerializeReferences();
			deserializeRefernces = new DeserializeReferences();
		}

		private object Instantiate(Type Type)
		{
			for (int i = 0; i < ObjectFactories.Count; ++i)
				if (ObjectFactories[i].CanInstantiate(Type))
					return ObjectFactories[i].Instantiate(Type);

			throw new FactoryNotFoundException(Type);
		}

		private static bool IsPrimitiveType(Type Type)
		{
			return (Type.IsPrimitive || Type == typeof(string));
		}

		private static bool IsComplexType(Type Type)
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
