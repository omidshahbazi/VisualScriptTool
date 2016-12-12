// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor
{
	public class ForStatementInstance : ControlStatementInstance
	{
		public ForStatementInstance(ForStatement Statement, PointF Position) :
			base(Statement, Position)
		{
			AddSlot("Body", Slot.Types.Executer, 1);

			AddSlot("Minimum", Slot.Types.Argument, 1);
			AddSlot("Maximum", Slot.Types.Argument, 2);
			AddSlot("Step", Slot.Types.Argument, 3);
		}
	}
}