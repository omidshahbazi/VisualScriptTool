// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.CodeGeneration.Language.CSharp
{
	class IfCodeGenerator : StatementCodeGenerator
	{
		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(IfStatement) }; }
		}

		public override string Generate(Statement Statement)
		{
			IfStatement statement = (IfStatement)Statement;

			StringBuilder builder = new StringBuilder();

			builder.Append("if (");
			builder.Append(statement.Condition.Name);
			builder.Append(')');
			builder.AppendLine("{");



			builder.AppendLine("}");

			return builder.ToString();
		}
	}
}