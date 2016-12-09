// Copyright 2016-2017 ?????????????. All Rights Reserved.

using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor
{
	public interface IStatementInstanceHolder
	{
		StatementInstance GetByStatement(Statement Statement);
	}
}
