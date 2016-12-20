// Copyright 2016-2017 ?????????????. All Rights Reserved.
namespace VisualScriptTool.Serialization
{
	public interface ISerializeObject : ISerializeData
	{
		ISerializeArray AddArray(string Name);
		ISerializeObject AddObject(string Name);

		void Set(string Name, object Value);
		void Set(string Name, bool Value);
		void Set(string Name, int Value);
		void Set(string Name, uint Value);
		void Set(string Name, float Value);
		void Set(string Name, double Value);
		void Set(string Name, string Value);
	}
}
