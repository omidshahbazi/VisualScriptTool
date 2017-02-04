// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Declaration.Variables
{
	public class VariableSetterStatement : FlowStatement
	{
		[SerializableElement(0)]
		public override string Name
		{
			get { return Variable.Name; }
			set { }
		}

		[SerializableElement(2)]
		public VariableStatement Variable
		{
			get;
			set;
		}

		[SerializableElement(3)]
		public VariableStatement Statement
		{
			get;
			set;
		}

		[SerializableElement(4)]
		public object DefaultValue
		{
			get;
			set;
		}
	}
}
