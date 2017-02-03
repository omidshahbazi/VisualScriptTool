// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.CodeGeneration.Language.CSharp
{
	class VariableSetterCodeGenerator : StatementCodeGenerator
	{
		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(VariableSetterStatement) }; }
		}

		public override void Generate(StringBuilder Builder, Statement Statement)
		{
			VariableSetterStatement statement = (VariableSetterStatement)Statement;

			if (statement.Statement == null)
				return;

			Builder.Append(statement.Variable.Name);
			Builder.Append(" = ");

			if (statement.Statement == null)
				Builder.Append(statement.DefaultValue.ToString().ToLower());
			else
				Builder.Append(statement.Statement.Name);

			Builder.AppendLine(";");
		}
	}
}
