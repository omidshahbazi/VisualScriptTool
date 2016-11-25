// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Language.Statements.Control
{
	public class IfStatement : ControlStatement
	{
		public BooleanVariable ConditionValue
		{
			get;
			set;
		}

		public StatementList Statements
		{
			get;
			set;
		}

		public StatementList ElseStatments
		{
			get;
			set;
		}

		public IfStatement()
		{
			Statements = new StatementList();
			ElseStatments = new StatementList();
		}
	}
}
