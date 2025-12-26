// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.CodeGeneration.Language.CSharp
{
	class ForCodeGenerator : ControlStatementCodeGenerator
	{
		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(ForStatement) }; }
		}

		public override void Generate(StringBuilder Builder, Statement Statement)
		{
			ForStatement statement = Statement.As<ForStatement>();

			Builder.Append("for (int i = ");

			if (statement.First == null)
				Builder.Append(statement.FirstDefaultValue.ToString());
			else
				Builder.Append(statement.First.Name);

			Builder.Append("; i <= ");

			if (statement.Last == null)
				Builder.Append(statement.LastDefaultValue.ToString());
			else
				Builder.Append(statement.Last.Name);

			Builder.Append("; i += ");

			if (statement.Step == null)
				Builder.Append(statement.StepDefaultValue.ToString());
			else
				Builder.Append(statement.Step.Name);

			Builder.AppendLine(")");

			Builder.AppendLine("{");

			if (statement.Statement != null)
				Get(statement.Statement.GetType()).Generate(Builder, statement.Statement);

			Builder.AppendLine("}");

			base.Generate(Builder, Statement);
		}
	}
}