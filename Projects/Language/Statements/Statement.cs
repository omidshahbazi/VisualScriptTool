// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;

namespace VisualScriptTool.Language.Statements
{
	public abstract class Statement
	{
		public abstract string Name
		{
			get;
			set;
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public class StatementList : List<Statement>
	{ }
}
