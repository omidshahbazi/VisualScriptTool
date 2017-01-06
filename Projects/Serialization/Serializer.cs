// Copyright 2016-2017 ?????????????. All Rights Reserved.

using System;

namespace VisualScriptTool.Serialization
{
	class Test : Serializer
	{
		public override Type Type
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override object CreateInstance()
		{
			throw new NotImplementedException();
		}

		public override T Deserialize<T>(ISerializeData Data)
		{
			throw new NotImplementedException();
		}

		public override void Serialize(ISerializeData Data, object Instance)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class Serializer
	{
		public abstract System.Type Type
		{
			get;
		}

		public abstract object CreateInstance();
		public abstract void Serialize(ISerializeData Data, object Instance);
		public abstract T Deserialize<T>(ISerializeData Data);

		protected static Serializer GetSerializer(System.Type Type)
		{
			return Creator.GetSerializer(Type);
		}

		protected static ISerializeArray AddArray(ISerializeObject Object, int ID)
		{
			return Object.AddArray(ID.ToString());
		}

		protected static ISerializeObject AddObject(ISerializeObject Object, int ID)
		{
			return Object.AddObject(ID.ToString());
		}

		protected static ISerializeArray AddArray(ISerializeArray Array)
		{
			return Array.AddArray();
		}

		protected static ISerializeObject AddObject(ISerializeArray Array)
		{
			return Array.AddObject();
		}

		protected static void Set(ISerializeObject Object, int ID, object Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		protected static void Set(ISerializeObject Object, int ID, bool Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		protected static void Set(ISerializeObject Object, int ID, int Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		protected static void Set(ISerializeObject Object, int ID, uint Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		protected static void Set(ISerializeObject Object, int ID, float Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		protected static void Set(ISerializeObject Object, int ID, double Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		protected static void Set(ISerializeObject Object, int ID, string Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		protected static T Get<T>(ISerializeObject Object, int ID, T DefaultValue = default(T))
		{
			if (Object.Contains(ID.ToString()))
				return Object.Get<T>(ID.ToString());

			return DefaultValue;
		}

		protected static void Add(ISerializeArray Array, object Item)
		{
			Array.Add(Item);
		}

		protected static T Get<T>(ISerializeArray Array, uint Index)
		{
			return Array.Get<T>(Index);
		}
	}
}