// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.CodeGeneration.Language.CSharp
{
	class WhileCodeGenerator : ControlStatementCodeGenerator
	{
		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(WhileStatement) }; }
		}

		public override void Generate(StringBuilder Builder, Statement Statement)
		{
			WhileStatement statement = (WhileStatement)Statement;

			Builder.Append("while (");

			if (statement.Condition == null)
				Builder.Append(statement.ConditionDefaultValue.ToString().ToLower());
			else
				Builder.Append(statement.Condition.Name);

			Builder.AppendLine(")");

			Builder.AppendLine("{");

			if (statement.Statement != null)
				Get(statement.Statement.GetType()).Generate(Builder, statement.Statement);

			Builder.AppendLine("}");

			base.Generate(Builder, Statement);
		}
	}
}