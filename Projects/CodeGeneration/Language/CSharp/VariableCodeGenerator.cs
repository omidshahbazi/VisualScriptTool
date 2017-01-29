﻿// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.CodeGeneration.Language.CSharp
{
	class VariableCodeGenerator : StatementCodeGenerator
	{
		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(BooleanVariable), typeof(IntegerVariable), typeof(FloatVariable), typeof(StringVariable) }; }
		}

		public override string Generate(Statement Statement)
		{
			Type variableType = null;
			if (Statement is BooleanVariable)
				variableType = typeof(bool);
			else if (Statement is IntegerVariable)
				variableType = typeof(int);
			else if (Statement is FloatVariable)
				variableType = typeof(float);
			else if (Statement is StringVariable)
				variableType = typeof(string);

			StringBuilder builder = new StringBuilder();

			builder.Append(variableType.FullName);
			builder.Append(' ');
			builder.Append(Statement.Name);
			builder.Append(';');

			return builder.ToString();
		}
	}
}
