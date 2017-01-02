// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Serialization
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class SerializableElementAttribute : Attribute
	{
		public int ID
		{
			get;
			private set;
		}

		public object DefaultValue
		{
			get;
			private set;
		}

		public SerializableElementAttribute(int ID)
		{
			this.ID = ID;
		}

		public SerializableElementAttribute(int ID, object DefaultValue)
		{
			this.ID = ID;
			this.DefaultValue = DefaultValue;
		}

		public string GetDefaultValueAsString()
		{
			if (DefaultValue == null)
				return "null";

			if (DefaultValue is bool)
				return ((bool)DefaultValue ? "true" : "false");

			return DefaultValue.ToString();
		}
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public class SerializableTypeAttribute : Attribute
	{
		public SerializableTypeAttribute()
		{
		}
	}
}
