// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor
{
	public abstract class ControlStatementInstance : StatementInstance
	{
		public ControlStatementInstance(ControlStatement Statement, PointF Position) :
			base(Statement, Position)
		{
			AddSlot(Slot.Types.EntryPoint, 0);
			AddSlot(Slot.Types.Executer, 0);
		}
	}
}