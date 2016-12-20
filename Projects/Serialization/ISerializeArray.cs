// Copyright 2016-2017 ?????????????. All Rights Reserved.
namespace VisualScriptTool.Serialization
{
	public interface ISerializeArray : ISerializeData
	{
		ISerializeArray AddArray();
		ISerializeObject AddObject();
	}
}
