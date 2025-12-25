// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Control
{
	public class WhileStatement : ControlStatement
	{
		public override string Name
		{
			get { return "while"; }
			set { }
		}

		[Constraints(typeof(bool))]
		[SerializableElement(2)]
		public VariableStatement Condition
		{
			get;
			set;
		}

		[SerializableElement(4)]
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

		public WhileStatement()
		{
			ConditionDefaultValue = true;
		}
	}
}