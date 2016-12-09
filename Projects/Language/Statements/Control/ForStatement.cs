// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Language.Statements.Control
{
	public class ForStatement : ControlStatement
	{
		public override string Name
		{
			get { return "for"; }
			set { }
		}

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

		public Statement StepStatement
		{
			get;
			set;
		}

		public StatementList Statment
		{
			get;
			set;
		}
	}
}