// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Declaration
{
	public class FloatVariable : VariableStatement
	{
		[SerializableElement(2)]
		public float Value
		{
			get;
			set;
		}
	}
}
