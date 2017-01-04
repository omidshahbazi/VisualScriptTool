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
			else if (DefaultValue is string)
				return "\"" + DefaultValue + "\"";

			return DefaultValue.ToString();
		}
	}

	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class SerializableInstantiatorAttribute : Attribute
	{
		public object[] DefaultParameters
		{
			get;
			private set;
		}

		public SerializableInstantiatorAttribute(params object[] DefaultParameters)
		{
			this.DefaultParameters = DefaultParameters;
		}

		public string GetDefaultParameterAsString(uint Index)
		{
			object value = DefaultParameters[Index];
			
			if (value == null)
				return "null";

			if (value is bool)
				return ((bool)value ? "true" : "false");
			else if (value is string)
				return "\"" + value + "\"";

			return value.ToString();
		}
	}
}
