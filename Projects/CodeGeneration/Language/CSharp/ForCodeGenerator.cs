// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
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
			ForStatement statement = (ForStatement)Statement;

			Builder.Append("for (int i = ");

			if (statement.MinimumValue == null)
				Builder.Append(statement.MinimumDefaultValue.ToString());
			else
				Builder.Append(statement.MinimumValue.Name);

			Builder.Append("; i <= ");

			if (statement.MaximumValue == null)
				Builder.Append(statement.MaximumDefaultValue.ToString());
			else
				Builder.Append(statement.MaximumValue.Name);

			Builder.Append("; i += ");

			if (statement.StepValue == null)
				Builder.Append(statement.StepDefaultValue.ToString());
			else
				Builder.Append(statement.StepValue.Name);

			Builder.AppendLine(")");

			Builder.AppendLine("{");

			if (statement.Statement != null)
				Get(statement.Statement.GetType()).Generate(Builder, statement.Statement);

			Builder.AppendLine("}");

			base.Generate(Builder, Statement);
		}
	}
}