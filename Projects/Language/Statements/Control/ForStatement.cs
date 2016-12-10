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

		public IntegerVariable MinimumValue
		{
			get;
			set;
		}

		public IntegerVariable MaximumValue
		{
			get;
			set;
		}

		public IntegerVariable StepValue
		{
			get;
			set;
		}

		public Statement Statement
		{
			get;
			set;
		}
	}
}