// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.CodeGeneration
{
	public interface ICodeGeneratorBase
	{
		byte[] Generate(string Name, Statement[] Statements);
	}
}
