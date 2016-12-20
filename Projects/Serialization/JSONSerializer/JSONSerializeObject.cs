// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Serialization.JSONSerializer
{
	class JSONSerializeObject : ISerializeObject
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

		ISerializeArray ISerializeObject.AddArray(string Name)
		{
			return new JSONSerializeArray();
		}

		ISerializeObject ISerializeObject.AddObject(string Name)
		{
			return new JSONSerializeObject();
		}

		void ISerializeObject.Set(string Name, float Value)
		{
		}

		void ISerializeObject.Set(string Name, string Value)
		{
		}

		void ISerializeObject.Set(string Name, double Value)
		{
		}

		void ISerializeObject.Set(string Name, uint Value)
		{
		}

		void ISerializeObject.Set(string Name, int Value)
		{
		}

		void ISerializeObject.Set(string Name, bool Value)
		{
		}

		void ISerializeObject.Set(string Name, object Value)
		{
		}
	}
}
