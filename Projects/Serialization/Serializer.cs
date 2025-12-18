// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public abstract class Serializer
	{
		public class ReferenceList : List<object>
		{ }

		public class StatementIDTable : Dictionary<string, object>
		{ }

		public class ReferenceResolver
		{
			public string id;
			object instance = null;
			private MemberInfo member = null;

			public ReferenceResolver(string ID, object Instance, MemberInfo Member)
			{
				id = ID;
				instance = Instance;
				member = Member;
			}

			public void Reslve(StatementIDTable References)
			{
				if (!References.ContainsKey(id))
					throw new System.ArgumentException("[" + id + "] not found");

				if (member is FieldInfo)
				{
					((FieldInfo)member).SetValue(instance, References[id]);
					return;
				}

				((PropertyInfo)member).SetValue(instance, References[id], null);
			}
		}

		public class ResolverList : List<ReferenceResolver>
		{ }

		public abstract System.Type Type
		{
			get;
		}

		public abstract object CreateInstance();
		public abstract void Serialize(ISerializeData Object, object Instance);
		public abstract T Deserialize<T>(ISerializeData Object);

		public abstract void SerializeInternal(ISerializeData Data, object Instance, System.Type InstanceType, ReferenceList References);
		public abstract T DeserializeInternal<T>(ISerializeData Data, StatementIDTable StatementIDs, ResolverList Resolvers);

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


		protected static void Set<T>(ISerializeObject Object, int ID, T Value, string Comment = null)
		{
			Object.Set(ID.ToString(), Value, Comment);
		}

		protected static T Get<T>(ISerializeObject Object, int ID, T DefaultValue = default(T))
		{
			if (Object.Contains(ID.ToString()))
				return Object.Get<T>(ID.ToString());

			return DefaultValue;
		}

		protected static bool Contains(ISerializeObject Object, int ID)
		{
			return Object.Contains(ID.ToString());
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