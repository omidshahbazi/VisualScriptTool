// Copyright 2016-2017 ?????????????. All Rights Reserved.
namespace VisualScriptTool.Serialization
{
	public interface ISerializeData
	{
		ISerializeData Parent
		{
			get;
		}

		string Name
		{
			get;
		}

		uint Count
		{
			get;
		}

		string Content
		{
			get;
		}
	}
}
