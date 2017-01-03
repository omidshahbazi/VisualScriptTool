// Copyright 2016-2017 ?????????????. All Rights Reserved.
using SimpleJson;
using System;
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

		object ISerializeObject.this[string Name]
		{
			get { return map[Name]; }
		}

		public JSONSerializeObject(ISerializeData Parent)
		{
			this.Parent = Parent;
		}

		bool ISerializeObject.Contains(string Name)
		{
			return map.ContainsKey(Name);
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

		T ISerializeObject.Get<T>(string Name)
		{
			object obj = map[Name];

			if (obj is T)
				return (T)obj;

			obj = Convert.ChangeType(obj, typeof(T));

			return (T)obj;

			//throw new System.InvalidCastException("Value of key [" + Name + "] is " + obj.GetType().Name + ", but desire type is " + typeof(T).Name);
		}

		IEnumerator<KeyValuePair<string, object>> ISerializeObject.GetEnumerator()
		{
			return map.GetEnumerator();
		}

		public static JSONSerializeObject Deserialize(string JSON)
		{
			JsonObject jsonObject = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(JSON);

			JSONSerializeObject obj = new JSONSerializeObject(null);

			SetContent(obj, jsonObject);

			return obj;
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
				object value = SerializeArray[i];

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

		private static void SetContent(JSONSerializeObject Object, JsonObject JsonObject)
		{
			Map map = Object.map;

			IEnumerator<KeyValuePair<string, object>> it = JsonObject.GetEnumerator();
			while (it.MoveNext())
			{
				string key = it.Current.Key;
				object value = it.Current.Value;

				if (value is JsonObject)
				{
					JSONSerializeObject jsonObj = new JSONSerializeObject(Object);
					map[key] = jsonObj;
					SetContent(jsonObj, (JsonObject)value);
				}
				else if (value is JsonArray)
				{
					JSONSerializeArray jsonArray = new JSONSerializeArray(Object);
					map[key] = jsonArray;
					SetContent(jsonArray, (JsonArray)value);
				}
				else
					map[key] = value;
			}
		}

		private static void SetContent(ISerializeArray Array, JsonArray JsonArray)
		{
			for (int i = 0; i < JsonArray.Count; ++i)
			{
				object value = JsonArray[i];

				if (value is JsonObject)
				{
					JSONSerializeObject jsonObj = new JSONSerializeObject(Array);
					Array.Add(jsonObj);
					SetContent(jsonObj, (JsonObject)value);
				}
				else if (value is JsonArray)
				{
					JSONSerializeArray jsonArray = new JSONSerializeArray(Array);
					Array.Add(jsonArray);
					SetContent(jsonArray, (JsonArray)value);
				}
				else
					Array.Add(value);
			}
		}
	}
}
