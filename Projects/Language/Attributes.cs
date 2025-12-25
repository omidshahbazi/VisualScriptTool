// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Language
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
	public class ConstraintsAttribute : Attribute
	{
		public Type[] Constraints
		{
			get;
			private set;
		}

		public ConstraintsAttribute(params Type[] Constraints)
		{
			this.Constraints = Constraints;
		}
	}
}
