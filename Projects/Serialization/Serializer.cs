// Copyright 2016-2017 ?????????????. All Rights Reserved.

namespace VisualScriptTool.Serialization
{
	public abstract class Serializer
	{
		public abstract System.Type Type
		{
			get;
		}

		public abstract void Serialize(ISerializeObject Object, object Instance);
		public abstract void Deserialize(ISerializeObject Object, object Instance);

		public static Serializer GetSerializer(System.Type Type)
		{
			return Creator.GetSerializer(Type);
		}

		public static ISerializeArray AddArray(ISerializeObject Object, int ID)
		{
			return Object.AddArray(ID.ToString());
		}

		public static ISerializeObject AddObject(ISerializeObject Object, int ID)
		{
			return Object.AddObject(ID.ToString());
		}

		public static ISerializeArray AddArray(ISerializeArray Array)
		{
			return Array.AddArray();
		}

		public static ISerializeObject AddObject(ISerializeArray Array)
		{
			return Array.AddObject();
		}

		public static void Set(ISerializeObject Object, int ID, object Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		public static void Set(ISerializeObject Object, int ID, bool Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		public static void Set(ISerializeObject Object, int ID, int Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		public static void Set(ISerializeObject Object, int ID, uint Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		public static void Set(ISerializeObject Object, int ID, float Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		public static void Set(ISerializeObject Object, int ID, double Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		public static void Set(ISerializeObject Object, int ID, string Value)
		{
			Object.Set(ID.ToString(), Value);
		}

		public static T Get<T>(ISerializeObject Object, int ID, T DefaultValue = default(T))
		{
			if (Object.Contains(ID.ToString()))
				return Object.Get<T>(ID.ToString());

			return DefaultValue;
		}

		public static void Add(ISerializeArray Array, object Item)
		{
			Array.Add(Item);
		}

		public static T Get<T>(ISerializeArray Array, uint Index)
		{
			return Array.Get<T>(Index);
		}
	}
}