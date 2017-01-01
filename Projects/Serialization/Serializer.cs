// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections;
using System.Collections.Generic;

namespace VisualScriptTool.Serialization
{
	public class Serializer
	{
		public class ObjectFactoryList : List<IObjectFactory>
		{ }

		public class StrategyList : List<IStrategy>
		{ }

		private SerializeReferences serializeRefernces = null;
		private DeserializeReferences deserializeRefernces = null;

		public ObjectFactoryList ObjectFactories
		{
			get;
			private set;
		}

		public StrategyList Strategies
		{
			get;
			private set;
		}

		public Serializer()
		{
			ObjectFactories = new ObjectFactoryList();
			ObjectFactories.Add(new DefaultObjectFactory());

			Strategies = new StrategyList();
			Strategies.Add(new DefaultStrategy());
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

			serializeRefernces.AddInstance(Instance);

			for (int i = 0; i < serializeRefernces.Instances.Length; ++i)
			{
				SerializeReferences.InstanceData instance = serializeRefernces.Instances[i];

				Type type = instance.Instance.GetType();

				// what about List<> or Dictionary<,> or any costum types
				// should we expose set value functionality into strategy interface ?

				if (IsArrayOrList(type))
					StoreInstance(Object.AddArray(instance.ID), instance);
				else if (type.IsClass)
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

		private void StoreInstance(ISerializeArray Array, SerializeReferences.InstanceData Instance)
		{
			Type type = Instance.Instance.GetType();

			if (IsArray(type))
			{
				Array array = (Array)Instance.Instance;

				bool isItemPrimitive = MemberData.IsTypePrimitive(GetArrayItemType(type));

				for (int i = 0; i < array.Length; ++i)
				{
					object item = array.GetValue(i);

					if (item == null)
					{
						Array.Add(null);

						continue;
					}

					if (isItemPrimitive)
						Array.Add(item);
					else
						Array.Add(serializeRefernces.GetOrAddInstance(item).ID);
				}
			}
			else if (IsList(type))
			{
				IList list = (IList)Instance.Instance;

				bool isItemPrimitive = MemberData.IsTypePrimitive(GetListItemType(type));

				for (int i = 0; i < list.Count; ++i)
				{
					object item = list[i];

					if (item == null)
					{
						Array.Add(null);

						continue;
					}

					if (isItemPrimitive)
						Array.Add(item);
					else
						Array.Add(serializeRefernces.GetOrAddInstance(item).ID);
				}
			}
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

			if (Member.IsPrimitive)
				Object.Set(identifier, Member.Value);
			else if (Member.Type.IsArray)
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

					membersArray.Add(Member.IsPrimitive ? item : serializeRefernces.GetOrAddInstance(item).ID);
				}
			}
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

				Type itemType = GetArrayItemType(Member.Type);

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
				{
					value = Object[identifier];

					if (DeserializeReferences.InstanceData.IsReferenceIDFormat(value.ToString()))
						value = CreateReferencedValue(value.ToString(), Member.Type);
				}

				Member.Value = (value == null ? null : Convert.ChangeType(value, Member.Type));
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

			throw new ObjectFactoryNotFoundException(Type);
		}

		private MemberData[] GetMembers(object Instance)
		{
			Type type = Instance.GetType();

			for (int i = 0; i < Strategies.Count; ++i)
				if (Strategies[i].CanHandle(type))
					return Strategies[i].GetMembers(Instance);

			throw new ObjectFactoryNotFoundException(type);
		}

		private static bool IsComplexType(Type Type)
		{
			return (!Type.IsPrimitive && Type != typeof(string));
		}

		private static Type GetArrayItemType(Type Type)
		{
			//return Type.GetType(Type.FullName.Replace("[]", "") + "," + Type.Assembly.FullName.Split(',')[0]);

			if (!Type.HasElementType)
				return null;

			return Type.GetElementType();
		}

		private static Type GetListItemType(Type Type)
		{
			if (Type.GetGenericArguments().Length == 0)
				return null;

			return Type.GetGenericArguments()[0];
		}

		private static bool IsArrayOrList(Type Type)
		{
			return (IsArray(Type) || IsList(Type));
		}

		private static bool IsArray(Type Type)
		{
			return Type.IsArray;
		}

		private static bool IsList(Type Type)
		{
			return (Type.GetInterface("IList") != null);
		}
	}
}
