// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Text;
using VisualScriptTool.CodeGeneration.Language;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Declaration;

namespace VisualScriptTool.CodeGeneration
{
	public class CSharpCodeGenerator : ICodeGeneratorBase
	{
		public byte[] Generate(string Name, Statement[] Statements)
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine($"class {Name}");
			builder.AppendLine("{");
			builder.AppendLine("void doIt()");
			builder.AppendLine("{");

			for (int i = 0; i < Statements.Length; ++i)
			{
				VariableStatement statement = Statements[i].As<VariableStatement>();
				if (statement == null)
					continue;

				StatementCodeGenerator.Get(statement.GetType()).Generate(builder, statement);
			}

			for (int i = 0; i < Statements.Length; ++i)
			{
				ExecuterStatement statement = Statements[i].As<ExecuterStatement>();
				if (statement == null)
					continue;

				StatementCodeGenerator.Get(statement.Statement.GetType()).Generate(builder, statement.Statement);
			}

			builder.AppendLine("}");
			builder.AppendLine("}");

			return Encoding.UTF8.GetBytes(builder.ToString());
		}
	}
}
