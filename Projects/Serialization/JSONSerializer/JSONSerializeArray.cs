// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;

namespace VisualScriptTool.Serialization.JSONSerializer
{
	class JSONSerializeArray : ISerializeArray
	{
		private List<object> items = new List<object>();

		uint ISerializeData.Count
		{
			get { return (uint)items.Count; }
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

		ISerializeData ISerializeData.Parent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public JSONSerializeArray(ISerializeData Parent)
		{
			this.Parent = Parent;
        }

		ISerializeArray ISerializeArray.AddArray()
		{
			ISerializeArray obj = new JSONSerializeArray(this);

			items.Add(obj);

			return obj;
		}

		ISerializeObject ISerializeArray.AddObject()
		{
			ISerializeObject obj = new JSONSerializeObject(this);

			items.Add(obj);

			return obj;
		}

		T ISerializeArray.GetObject<T>(uint Index)
		{
			return (T)items[(int)Index];
		}
	}
}
