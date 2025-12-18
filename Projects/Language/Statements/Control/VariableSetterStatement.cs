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

		[SerializableElement(3)]
		public VariableStatement Variable
		{
			get;
			set;
		}

		[SerializableElement(4)]
		public UserDefinedStatement Statement
		{
			get;
			set;
		}

		[SerializableElement(5)]
		public VariableStatement DefaultValue
		{
			get;
			set;
		}
	}
}
