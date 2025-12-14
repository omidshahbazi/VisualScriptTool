// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Control
{
	public class VariableSetterStatement : FlowStatement
	{
		[SerializableElement(2)]
		public override string Name
		{
			get { return Variable.Name; }
			set { }
		}

		//TODO: VariableStatement or UserDefinedStatement? Shouldn't we have only one of them?
		[SerializableElement(3)]
		public VariableStatement Variable
		{
			get;
			set;
		}

		//TODO: VariableStatement or UserDefinedStatement? Shouldn't we have only one of them?
		[SerializableElement(4)]
		public VariableStatement Statement
		{
			get;
			set;
		}

		//TODO: VariableStatement or UserDefinedStatement? Shouldn't we have only one of them?
		[SerializableElement(5)]
		public UserDefinedStatement DefaultValue
		{
			get;
			set;
		}
	}
}
