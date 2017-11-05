// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration.Variables;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Control
{
	public class IfStatement : ControlStatement
	{
		public override string Name
		{
			get { return "if"; }
			set { }
		}

		[SerializableElement(2)]
		public BooleanVariable Condition
		{
			get;
			set;
		}

		[SerializableElement(5)]
		public bool ConditionDefaultValue
		{
			get;
			set;
		}

		[SerializableElement(3)]
		public Statement Statement
		{
			get;
			set;
		}

		[SerializableElement(4)]
		public Statement ElseStatment
		{
			get;
			set;
		}
	}
}
