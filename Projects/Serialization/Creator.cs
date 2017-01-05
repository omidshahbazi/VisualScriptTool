// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization.JSONSerializer;

namespace VisualScriptTool.Serialization
{
	public static class Creator
	{
		private static System.Collections.Generic.Dictionary<System.Type, Serializer> serializers = new System.Collections.Generic.Dictionary<System.Type, Serializer>();

		public static ISerializeObject Create()
		{
			return new JSONSerializeObject(null);
		}

		public static T Create<T>() where T : ISerializeData
		{
			if (typeof(T) == typeof(ISerializeObject))
				return (T)(ISerializeData)new JSONSerializeObject(null);
			else
				return (T)(ISerializeData)new JSONSerializeArray(null);
		}

		public static T Create<T>(string Data) where T : ISerializeData
		{
			return JSONSerializeObject.Deserialize<T>(Data);
		}

		public static Serializer GetSerializer(System.Type Type)
		{
			return serializers[Type];
		}

		public static void AddSerializer(Serializer Serializer)
		{
			serializers[Serializer.Type] = Serializer;
		}
	}
}