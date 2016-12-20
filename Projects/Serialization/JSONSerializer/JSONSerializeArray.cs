// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Serialization.JSONSerializer
{
	class JSONSerializeArray : ISerializeArray
	{
		uint ISerializeData.Count
		{
			get
			{
				return 0;
			}
		}

		string ISerializeData.Name
		{
			get
			{
				return "";
			}
		}

		ISerializeData ISerializeData.Parent
		{
			get
			{
				return null;
			}
		}

		ISerializeArray ISerializeArray.AddArray()
		{
			return new JSONSerializeArray();
		}

		ISerializeObject ISerializeArray.AddObject()
		{
			return new JSONSerializeObject();
		}
	}
}
