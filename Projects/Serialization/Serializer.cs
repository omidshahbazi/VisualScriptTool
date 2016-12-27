// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;
using VisualScriptTool.Serialization.JSONSerializer;

namespace VisualScriptTool.Serialization
{
	public class Serializer
	{
		private class InstanceData
		{
			private static uint globalID;

			public string ID
			{
				get;
				private set;
			}

			public object Instance
			{
				get;
				private set;
			}

			public InstanceData(object Instance)
			{
				ID = "O#" + (globalID++).ToString();
				this.Instance = Instance;
			}
		}

		private class InstanceDataList : List<InstanceData>
		{ }

		private InstanceDataList instances = new InstanceDataList();

		public ISerializeObject Serialize(object Instance)
		{
			ISerializeObject data = new JSONSerializeObject(null);

			Serialize(data, Instance);

			return data;
		}

		public void Serialize(ISerializeObject Object, object Instance)
		{
			if (IsTypeStorable(Instance.GetType()))
				return;

			AddInstance(Instance);

			for (int i = 0; i < instances.Count; ++i)
			{
				InstanceData instance = instances[i];

				StoreInstance(Object.AddObject(instance.ID), instance);
			}
		}

		public T Deserialize<T>(ISerializeObject Object)
		{
			//Deserialize(Object new T());

			return default(T);
		}

		public void Deserialize(ISerializeObject Object, object Instance)
		{
			if (Instance == null)
				return;

			//StoreObject(data, Object);
		}

		private void StoreInstance(ISerializeObject Object, InstanceData Instance)
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
						membersArray.AddItem(null);
						continue;
					}

					membersArray.AddItem(IsTypeStorable(item.GetType()) ? item : GetOrAddInstance(item).ID);
				}
			}
			else if (IsTypeStorable(Member.Type))
				Object.Set(identifier, Member.Value);
			else
				Object.Set(identifier, GetOrAddInstance(Member.Value).ID);
		}

		private InstanceData GetInstance(object Instance)
		{
			for (int i = 0; i < instances.Count; ++i)
				if (instances[i].Instance == Instance)
					return instances[i];

			return null;
		}

		private InstanceData AddInstance(object Instance)
		{
			InstanceData instance = new InstanceData(Instance);
			instances.Add(instance);
			return instance;
		}

		private InstanceData GetOrAddInstance(object Instance)
		{
			InstanceData instance = GetInstance(Instance);

			if (instance != null)
				return instance;

			return AddInstance(Instance);
		}

		private static bool IsTypeStorable(System.Type Type)
		{
			return (Type.IsPrimitive || Type == typeof(string));
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

					if (serializableAttr == null)
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
