// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements
{
	public abstract class Statement
	{
		[SerializableElement(0)]
		public string ID
		{
			get;
			set;
		}

		public abstract string Name
		{
			get;
			set;
		}
		public Statement()
		{
			ID = Guid.NewGuid().ToString();
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public class StatementList : List<Statement>
	{ }
}
