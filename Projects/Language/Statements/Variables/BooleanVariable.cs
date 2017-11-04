// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Declaration.Variables
{
	public class BooleanVariable : VariableStatement
	{
		[SerializableElement(2)]
		public bool Value
		{
			get;
			set;
		}
	}
}
