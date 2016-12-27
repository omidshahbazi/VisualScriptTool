// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Serialization
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class SerializableAttribute : Attribute
	{
		public int Identifier
		{
			get;
			private set;
		}

		public SerializableAttribute(int Identifier)
		{
			this.Identifier = Identifier;
		}
	}

	//[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	//public class NonSerializableAttribute : Attribute
	//{
	//}
}