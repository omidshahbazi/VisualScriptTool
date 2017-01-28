// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using System.Text;

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
				StringBuilder str = new StringBuilder();
				str.Append('[');
				for (int i = 0; i < items.Count;++i)
				{
					if (i != 0)
						str.Append(',');

					object item = items[i];

					if (item == null)
						str.Append("null");
					else if (item is ISerializeData)
						str.Append(((ISerializeData)item).Content);
					else if (item is string)
					{
						str.Append('"');
						str.Append(item.ToString());
						str.Append('"');
					}
					else if (item is bool)
						str.Append(item.ToString().ToLower());
					else
						str.Append(item.ToString());
				}
				str.Append(']');
				return str.ToString();
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
