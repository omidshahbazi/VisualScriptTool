// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Serialization
{
	public class FactoryNotFoundException : Exception
	{
		public Type Type
		{
			get;
			private set;
		}

		public FactoryNotFoundException(Type Type)
		{
			this.Type = Type;
		}
	}
}