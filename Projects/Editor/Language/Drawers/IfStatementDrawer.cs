// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

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

		public IfStatementDrawer()
		{
		}

		protected override void DrawBody(StatementInstance StatementInstance)
		{
			base.DrawBody(StatementInstance);
			
			IfStatement statement = (IfStatement)StatementInstance.Statement;

			if (statement.Condition == null)
			{
				Slot slot = StatementInstance.Slots[2];
				SizeF size = MeasureString(slot.Name);

				DrawFillRectangle(slot.Bounds.Left + size.Width + 30, slot.Position.Y, 10, 10, System.Drawing.Brushes.DimGray);

				DrawFillCircle(slot.Bounds.Left + size.Width + 32, slot.Position.Y + 2, 6, System.Drawing.Brushes.Green);
			}
		}
	}
}