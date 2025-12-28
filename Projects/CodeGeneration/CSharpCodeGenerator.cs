// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
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

			for (int i = 0; i < Statements.Length; ++i)
			{
				VariableStatement statement = Statements[i].As<VariableStatement>();
				if (statement == null)
					continue;

				StatementCodeGenerator.Get(statement.GetType()).Generate(builder, statement);
			}

			List<Statement> parentStatements = new List<Statement>(Statements.FindAll<FunctionStatement>());

			EntrypointStatement executerStatement = Statements.Find<EntrypointStatement>();
			if (executerStatement != null)
				parentStatements.Add(executerStatement);

			for (int i = 0; i < parentStatements.Count; ++i)
				StatementCodeGenerator.Get(parentStatements[i].GetType()).Generate(builder, parentStatements[i]);

			builder.AppendLine("}");

			return Encoding.UTF8.GetBytes(builder.ToString());
		}
	}
}
