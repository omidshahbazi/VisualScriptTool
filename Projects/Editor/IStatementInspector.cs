// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor
{
	public interface IStatementInspector
	{
		StatementInstance GetInstance(Statement Statement);
	}
}