// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Text;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.CodeGeneration.Language.CSharp
{
	abstract class FlowStatementCodeGenerator : StatementCodeGenerator
	{
		public override void Generate(StringBuilder Builder, Statement Statement)
		{
			FlowStatement statement = (FlowStatement)Statement;

			if (statement.CompleteStatement != null)
				Get(statement.CompleteStatement.GetType()).Generate(Builder, statement.CompleteStatement);
		}
	}
}