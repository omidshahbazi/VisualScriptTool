// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor
{
	public class IfStatementInstance : ControlStatementInstance
	{
		public IfStatementInstance(IfStatement Statement, PointF Position) :
			base(Statement, Position)
		{
			AddSlot(Slot.Types.Argument, 1);

			AddSlot(Slot.Types.Executer, 1);
			AddSlot(Slot.Types.Executer, 2);
		}
	}
}