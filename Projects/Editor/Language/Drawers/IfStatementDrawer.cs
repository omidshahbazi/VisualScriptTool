// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Editor.Language.Drawers.Controls;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class IfStatementDrawer : ControlStatementDrawer
	{
		protected override float MinimumWidth
		{
			get { return 200.0F; }
		}

		public override uint RowCount
		{
			get { return 3; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(IfStatement) }; }
		}

		//public override void Draw(IDevice Device, StatementInstance StatementInstance)
		//{
		//	base.Draw(Device, StatementInstance);

		//	IfStatement statement = (IfStatement)StatementInstance.Statement;

		//	if (statement.Condition == null)
		//	{
		//		Slot slot = StatementInstance.Slots[2];

		//		checkBox.Value = statement.ConditionDefaultValue;
		//		checkBox.Location = new PointF(slot.Bounds.Right + 80, slot.Bounds.Y);
		//		checkBox.Draw(Device);
		//	}
		//}
	}
}