// Copyright 2016-2017 ?????????????. All Rights Reserved.
namespace VisualScriptTool.Serialization
{
	public interface ISerializeArray : ISerializeData
	{
		object this[uint Index]
		{
			get;
		}

		ISerializeArray AddArray();
		ISerializeObject AddObject();

		void Add(object Item);
		T Get<T>(uint Index);
	}
}
