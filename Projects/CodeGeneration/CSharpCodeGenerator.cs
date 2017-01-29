// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Text;
using VisualScriptTool.CodeGeneration.Language;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.CodeGeneration
{
	public class CSharpCodeGenerator : CodeGeneratorBase
	{
		public override string[] Generate(ExecuterStatement Statement)
		{
			if (Statement.Statement == null)
				return null;

			StringBuilder builder = new StringBuilder();

			builder.AppendLine("class x");
			builder.AppendLine("{");
			builder.AppendLine("void doIt()");
			builder.AppendLine("{");

			StatementCodeGenerator codeGen = StatementCodeGenerator.Get(Statement.Statement.GetType());

			codeGen.Generate(builder, Statement.Statement);

			builder.AppendLine("}");
			builder.AppendLine("}");

			return new string[] { builder.ToString() };
		}
	}
}
