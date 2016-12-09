// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Language.Statements.Control
{
	public class IfStatement : ControlStatement
	{
		public override string Name
		{
			get { return "if"; }
			set { }
		}

		public BooleanVariable ConditionValue
		{
			get;
			set;
		}

		public Statement Statement
		{
			get;
			set;
		}

		public Statement ElseStatment
		{
			get;
			set;
		}
	}
}
