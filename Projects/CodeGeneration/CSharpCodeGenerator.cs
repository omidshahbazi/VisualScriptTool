// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
using VisualScriptTool.CodeGeneration.Language;
using VisualScriptTool.CodeGeneration.Language.CSharp;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.CodeGeneration
{
	public class CSharpCodeGenerator : CodeGeneratorBase
	{
		StatementCodeGenerator[] codeGenerator = new StatementCodeGenerator[] { new VariableCodeGenerator(), new IfCodeGenerator() };

		public override string[] Generate(Statement[] Statements)
		{
			StringBuilder builder = new StringBuilder();

			for (int i = 0; i < Statements.Length; ++i)
			{
				Statement statement = Statements[i];

				StatementCodeGenerator codeGen = GetCodeGenerator(statement.GetType());

				if (codeGen == null)
					continue;

				builder.AppendLine(codeGen.Generate(statement));
			}

			return new string[] { builder.ToString() };
		}

		private StatementCodeGenerator GetCodeGenerator(Type StatementType)
		{
			for (int i = 0; i < codeGenerator.Length; ++i)
				if (Array.IndexOf(codeGenerator[i].StatementTypes, StatementType) != -1)
					return codeGenerator[i];

			return null;
		}
	}
}
