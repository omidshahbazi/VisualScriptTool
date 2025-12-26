// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.CodeGeneration.Language.CSharp
{
	class IfCodeGenerator : ControlStatementCodeGenerator
	{
		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(IfStatement) }; }
		}

		public override void Generate(StringBuilder Builder, Statement Statement)
		{
			IfStatement statement = Statement.As<IfStatement>();

			Builder.Append("if (");

			if (statement.Condition == null)
				Builder.Append(statement.ConditionDefaultValue.ToString().ToLower());
			else
				Builder.Append(statement.Condition.Name);

			Builder.AppendLine(")");
			Builder.AppendLine("{");

			if (statement.Statement != null)
				Get(statement.Statement.GetType()).Generate(Builder, statement.Statement);

			Builder.AppendLine("}");

			if (statement.ElseStatment != null)
			{
				Builder.Append("else");
				Builder.AppendLine("{");

				if (statement.ElseStatment != null)
					Get(statement.ElseStatment.GetType()).Generate(Builder, statement.ElseStatment);

				Builder.AppendLine("}");
			}

			base.Generate(Builder, Statement);
		}
	}
}