// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.CodeGeneration
{
	public abstract class CodeGeneratorBase
    {
		public abstract string[] Generate(ExecuterStatement Statements);
    }
}
