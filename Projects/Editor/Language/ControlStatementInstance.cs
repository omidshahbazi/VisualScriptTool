// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language
{
	public abstract class ControlStatementInstance : FlowStatementInstance
	{
		public ControlStatementInstance(ControlStatement Statement) :
			base(Statement)
		{
		}
	}
}