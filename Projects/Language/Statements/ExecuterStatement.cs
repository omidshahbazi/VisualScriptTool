// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements
{
	public class ExecuterStatement : Statement
	{
		public override string Name
		{
			get { return "Executer"; }
			set { }
		}

		[SerializableElement(0)]
		public Statement Statement
		{
			get;
			set;
		}
	}
}