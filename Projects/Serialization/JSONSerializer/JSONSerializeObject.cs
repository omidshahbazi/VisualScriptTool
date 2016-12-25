// Copyright 2016-2017 ?????????????. All Rights Reserved.
using SimpleJson;
using System.Collections.Generic;

namespace VisualScriptTool.Serialization.JSONSerializer
{
	class JSONSerializeObject : ISerializeObject
	{
		private class Map : Dictionary<string, object>
		{ }

		private Map map = new Map();

		uint ISerializeData.Count
		{
			get { return (uint)map.Count; }
		}

		string ISerializeData.Name
		{
			get { return ""; }
		}

		public ISerializeData Parent
		{
			get;
			private set;
		}

		string ISerializeObject.Content
		{
			get
			{
				JsonObject obj = new JsonObject();

				GetContent(obj, map);

				return obj.ToString();
			}
		}

		public JSONSerializeObject(ISerializeData Parent)
		{
			this.Parent = Parent;
		}

		ISerializeArray ISerializeObject.AddArray(string Name)
		{
			ISerializeArray obj = new JSONSerializeArray(this);

			map[Name] = obj;

			return obj;
		}

		ISerializeObject ISerializeObject.AddObject(string Name)
		{
			ISerializeObject obj = new JSONSerializeObject(this);

			map[Name] = obj;

			return obj;
		}

		void ISerializeObject.Set(string Name, float Value)
		{
			map[Name] = Value;
		}

		void ISerializeObject.Set(string Name, string Value)
		{
			map[Name] = Value;
		}

		void ISerializeObject.Set(string Name, double Value)
		{
			map[Name] = Value;
		}

		void ISerializeObject.Set(string Name, uint Value)
		{
			map[Name] = Value;
		}

		void ISerializeObject.Set(string Name, int Value)
		{
			map[Name] = Value;
		}

		void ISerializeObject.Set(string Name, bool Value)
		{
			map[Name] = Value;
		}

		void ISerializeObject.Set(string Name, object Value)
		{
			map[Name] = Value;
		}

		private static void GetContent(JsonObject Object, Map Map)
		{
			Map.Enumerator it = Map.GetEnumerator();
			while (it.MoveNext())
			{
				string key = it.Current.Key;
				object value = it.Current.Value;

				if (value is ISerializeObject)
				{
					JsonObject jsonObj = new JsonObject();
					Object[key] = jsonObj;
					GetContent(jsonObj, ((JSONSerializeObject)value).map);
				}
				else if (value is ISerializeArray)
				{
					JsonArray jsonArray = new JsonArray();
					Object[key] = jsonArray;
					GetContent(jsonArray, (ISerializeArray)value);
				}
				else
					Object[key] = value;
			}
		}

		private static void GetContent(JsonArray Array, ISerializeArray SerializeArray)
		{
			for (uint i = 0; i < SerializeArray.Count; ++i)
			{
				object value = SerializeArray.GetItem<object>(i);

				if (value is ISerializeObject)
				{
					JsonObject jsonObj = new JsonObject();
					Array.Add(jsonObj);
					GetContent(jsonObj, ((JSONSerializeObject)value).map);
				}
				else if (value is ISerializeArray)
				{
					JsonArray jsonArray = new JsonArray();
					Array.Add(jsonArray);
					GetContent(jsonArray, (ISerializeArray)value);
				}
				else
					Array.Add(value);
			}
		}
	}
}
