// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor
{
	public class VariableStatementInstance : StatementInstance
	{
		public VariableStatementInstance(VariableStatement Statement, PointF Position) :
			base(Statement, Position)
		{
			AddSlot(Slot.Types.Getter, 0);
		}
	}
}