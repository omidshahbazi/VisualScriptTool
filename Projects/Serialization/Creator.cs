// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization.JSONSerializer;

namespace VisualScriptTool.Serialization
{
	public static class Creator
	{
		public static ISerializeObject Create()
		{
			return new JSONSerializeObject(null);
		}

		public static ISerializeObject Create(string Data)
		{
			return JSONSerializeObject.Deserialize(Data);
		}
	}
}