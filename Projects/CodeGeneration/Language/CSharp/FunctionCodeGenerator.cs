// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.CodeGeneration.Language.CSharp
{
	class FunctionCodeGenerator : FlowStatementCodeGenerator
	{
		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(FunctionStatement) }; }
		}

		public override void Generate(StringBuilder Builder, Statement Statement)
		{
			FunctionStatement statement = (FunctionStatement)Statement;

			Builder.Append(statement.Method.DeclaringType.FullName);
			Builder.Append('.');
			Builder.Append(statement.Method.Name);
			Builder.Append('(');

			for (int i = 0; i < statement.Parameters.Length; ++i)
			{
				if (i != 0)
					Builder.Append(',');

				Statement stat = statement.Parameters[i];

				if (stat == null)
					Builder.Append(statement.ParametersDefaultValue[i]);
				else
					Get(stat.GetType()).Generate(Builder, stat);
			}

			Builder.Append(");");

			base.Generate(Builder, Statement);
		}
	}
}