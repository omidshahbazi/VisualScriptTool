// Copyright 2016-2017 ?????????????. All Rights Reserved.
using SimpleJson;
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

		string ISerializeData.Content
		{
			get
			{
				JsonArray array = new JsonArray();

				JSONSerializeObject.GetContent(array, this);

				return array.ToString();
			}
		}

		object ISerializeArray.this[uint Index]
		{
			get { return items[(int)Index]; }
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

		void ISerializeArray.Add(object Item)
		{
			items.Add(Item);
		}

		T ISerializeArray.Get<T>(uint Index)
		{
			return (T)items[(int)Index];
		}
	}
}
