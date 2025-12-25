// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration;
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

		[Constraints(typeof(bool))]
		[SerializableElement(2)]
		public VariableStatement Condition
		{
			get;
			set;
		}

		[Constraints(typeof(bool))]
		[SerializableElement(5)]
		public AnyDataType ConditionDefaultValue
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

		public IfStatement()
		{
			ConditionDefaultValue = false;
		}
	}
}
