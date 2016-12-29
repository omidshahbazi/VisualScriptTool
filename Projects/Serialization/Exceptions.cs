// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Serialization
{
	public class ObjectFactoryNotFoundException : Exception
	{
		public Type Type
		{
			get;
			private set;
		}

		public ObjectFactoryNotFoundException(Type Type)
		{
			this.Type = Type;
		}
	}

	public class StrategyNotFoundException : Exception
	{
		public Type Type
		{
			get;
			private set;
		}

		public StrategyNotFoundException(Type Type)
		{
			this.Type = Type;
		}
	}
}