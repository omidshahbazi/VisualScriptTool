// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Declaration.Variables;

namespace VisualScriptTool.Language.Statements.Control
{
	public class ForStatement : ControlStatement
	{
		public Statement InitializeStatement
		{
			get;
			set;
		}

		public BooleanVariable BreakConditionValue
		{
			get;
			set;
		}

		public StatementList StepStatements
		{
			get;
			set;
		}

		public StatementList Statments
		{
			get;
			set;
		}

		public ForStatement()
		{
			StepStatements = new StatementList();
			Statments = new StatementList();
		}
	}
}
