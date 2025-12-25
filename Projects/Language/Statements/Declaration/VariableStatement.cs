// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Declaration
{
	public class VariableStatement : UserDefinedStatement
	{
		[SerializableElement(1)]
		public override string Name
		{
			get;
			set;
		}

		[SerializableElement(3)]
		public AnyDataType Value
		{
			get;
			set;
		}

		public override string ToString()
		{
			return Name + " (" + Value.Type.Name + ")";
		}
	}

	public class VariableStatementList : List<VariableStatement>
	{ }
}
