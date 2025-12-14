// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;

namespace VisualScriptTool.Serialization
{
	public interface ISerializeObject : ISerializeData
	{
		object this[string Name]
		{
			get;
		}

		bool Contains(string Name);

		ISerializeArray AddArray(string Name, string Comment = null);
		ISerializeObject AddObject(string Name, string Comment = null);

		void Set(string Name, object Value, string Comment = null);
		void Set(string Name, bool Value, string Comment = null);
		void Set(string Name, int Value, string Comment = null);
		void Set(string Name, uint Value, string Comment = null);
		void Set(string Name, float Value, string Comment = null);
		void Set(string Name, double Value, string Comment = null);
		void Set(string Name, string Value, string Comment = null);

		T Get<T>(string Name);

		void Remove(string Name);

		IEnumerator<KeyValuePair<string, object>> GetEnumerator();
	}
}
