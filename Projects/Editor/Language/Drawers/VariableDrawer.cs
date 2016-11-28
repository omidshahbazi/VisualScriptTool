// Copyright 2016-2017 ?????????????. All Rights Reserved.

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class VariableDrawer : Drawer
	{
		protected override void Draw(StatementInstance StatementInstance)
		{
			Graphics.FillRectangle(System.Drawing.Brushes.Black, StatementInstance.Position.X, StatementInstance.Position.Y, 100.0F, 100.0F);
		}
	}
}