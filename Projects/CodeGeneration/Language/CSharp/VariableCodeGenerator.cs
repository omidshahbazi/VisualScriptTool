// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Declaration;

namespace VisualScriptTool.CodeGeneration.Language.CSharp
{
	class VariableCodeGenerator : StatementCodeGenerator
	{
		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(VariableStatement) }; }
		}

		public override void Generate(StringBuilder Builder, Statement Statement)
		{
			VariableStatement statement = Statement.As<VariableStatement>();

			if (statement.Value.Type == null)
				return;

			Builder.Append(statement.Value.Type.FullName);
			Builder.Append(' ');
			Builder.Append(Statement.Name);

			if (statement.Value != null)
			{
				Builder.Append(" = ");
				Builder.Append(statement.Value.ToString());
			}

			Builder.AppendLine(";");
		}
	}
}
