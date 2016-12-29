// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;

namespace VisualScriptTool.Serialization
{
	public interface ISerializeObject : ISerializeData
	{
		string Content
		{
			get;
		}

		object this[string Name]
		{
			get;
		}

		bool Contains(string Name);

		ISerializeArray AddArray(string Name);
		ISerializeObject AddObject(string Name);

		void Set(string Name, object Value);
		void Set(string Name, bool Value);
		void Set(string Name, int Value);
		void Set(string Name, uint Value);
		void Set(string Name, float Value);
		void Set(string Name, double Value);
		void Set(string Name, string Value);

		T Get<T>(string Name);

		IEnumerator<KeyValuePair<string, object>> GetEnumerator();
	}
}
