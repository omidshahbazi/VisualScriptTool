// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Declaration
{
	public class IntegerVariable : VariableStatement
	{
		[SerializableElement(2)]
		public int Value
		{
			get;
			set;
		}
	}
}
