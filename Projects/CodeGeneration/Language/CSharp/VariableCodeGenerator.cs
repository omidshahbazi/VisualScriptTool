// Copyright 2016-2017 ?????????????. All Rights Reserved.
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
			get { return new Type[] { typeof(BooleanVariable), typeof(IntegerVariable), typeof(FloatVariable), typeof(StringVariable)}; }
		}

		public override void Generate(StringBuilder Builder, Statement Statement)
		{
			Type variableType = null;
			string value = null;
			if (Statement is BooleanVariable)
			{
				variableType = typeof(bool);
				value = ((BooleanVariable)Statement).Value.ToString().ToLower();
			}
			else if (Statement is IntegerVariable)
			{
				variableType = typeof(int);
				value = ((IntegerVariable)Statement).Value.ToString();
			}
			else if (Statement is FloatVariable)
			{
				variableType = typeof(float);
				value = ((FloatVariable)Statement).Value.ToString() + "F";
			}
			else if (Statement is StringVariable)
			{
				variableType = typeof(string);
				value = ((StringVariable)Statement).Value;
			}

			Builder.Append(variableType.FullName);
			Builder.Append(' ');
			Builder.Append(Statement.Name);
			Builder.Append(" = ");
			Builder.Append(value);
			Builder.AppendLine(";");
		}
	}
}
