// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Declaration.Variables
{
	public class VariableSetterStatement : VariableStatement
	{
		[SerializableElement(1)]
		public VariableStatement Variable
		{
			get;
			set;
		}

		[SerializableElement(2)]
		public VariableStatement Statement
		{
			get;
			set;
		}

		[SerializableElement(3)]
		public object DefaultValue
		{
			get;
			set;
		}
	}
}
