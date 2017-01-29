// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Text;
using VisualScriptTool.CodeGeneration.Language;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.CodeGeneration
{
	public class CSharpCodeGenerator : CodeGeneratorBase
	{
		public override string[] Generate(Statement[] Statements)
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine("class x");
			builder.AppendLine("{");
			builder.AppendLine("void doIt()");
			builder.AppendLine("{");

			for (int i = 0; i < Statements.Length; ++i)
			{
				Statement statement = Statements[i];

				if (statement is VariableStatement)
					StatementCodeGenerator.Get(statement.GetType()).Generate(builder, statement);
			}

			for (int i = 0; i < Statements.Length; ++i)
			{
				if (Statements[i] is ExecuterStatement)
				{
					ExecuterStatement statement = (ExecuterStatement)Statements[i];

					StatementCodeGenerator.Get(statement.Statement.GetType()).Generate(builder, statement.Statement);
				}
			}

			builder.AppendLine("}");
			builder.AppendLine("}");

			return new string[] { builder.ToString() };
		}


	}
}
