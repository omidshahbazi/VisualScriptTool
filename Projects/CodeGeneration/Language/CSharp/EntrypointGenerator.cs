// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.CodeGeneration.Language.CSharp
{
	class EntrypointGenerator : ControlStatementCodeGenerator
	{
		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(EntrypointStatement) }; }
		}

		public override void Generate(StringBuilder Builder, Statement Statement)
		{
			EntrypointStatement statement = Statement.As<EntrypointStatement>();

			Builder.AppendLine("public static void Main()");
			Builder.AppendLine("{");

			if (statement.Statement != null)
				Get(statement.Statement.GetType()).Generate(Builder, statement.Statement);

			Builder.AppendLine("}");

			base.Generate(Builder, Statement);
		}
	}
}