// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.CodeGeneration.Language
{
	abstract class StatementCodeGenerator
	{
		public abstract Type[] StatementTypes
		{
			get;
		}

		public abstract string Generate(Statement Statement);
	}
}
