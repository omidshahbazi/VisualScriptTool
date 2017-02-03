// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Text;

namespace VisualScriptTool.Serialization.JSONSerializer
{
	class JSONSerializeObject : ISerializeObject
	{
		private static readonly JSONParser parser = new JSONParser();

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

		string ISerializeData.Content
		{
			get
			{
				StringBuilder str = new StringBuilder();
				str.Append('{');

				Map.Enumerator it = map.GetEnumerator();
				bool isFirstOne = true;
				while (it.MoveNext())
				{
					if (isFirstOne)
						isFirstOne = false;
					else
						str.Append(',');

					object value = it.Current.Value;

					str.Append('"');
					str.Append(it.Current.Key);
					str.Append("\":");

					if (value == null)
						str.Append("null");
					else if (value is ISerializeData)
						str.Append(((ISerializeData)value).Content);
					else if (value is string)
					{
						str.Append('"');
						str.Append(value.ToString());
						str.Append('"');
					}
					else if (value is bool)
						str.Append(value.ToString().ToLower());
					else
						str.Append(value.ToString());
				}
				str.Append('}');
				return str.ToString();
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

			Type type = typeof(T);

			if (type.IsEnum)
				obj = Enum.ToObject(type, obj);
			else
				obj = Convert.ChangeType(obj, typeof(T));

			return (T)obj;

			//throw new System.InvalidCastException("Value of key [" + Name + "] is " + obj.GetType().Name + ", but desire type is " + typeof(T).Name);
		}

		IEnumerator<KeyValuePair<string, object>> ISerializeObject.GetEnumerator()
		{
			return map.GetEnumerator();
		}

		public static T Deserialize<T>(string JSON) where T : ISerializeData
		{
			return (T)parser.Parse(ref JSON);
		}
	}
}
