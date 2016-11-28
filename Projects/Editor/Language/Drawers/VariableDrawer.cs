// Copyright 2016-2017 ?????????????. All Rights Reserved.

using System.Drawing;
using System.Drawing.Drawing2D;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class VariableDrawer : Drawer
	{
		PathGradientBrush brush = null;

		public VariableDrawer()
		{
			GraphicsPath path = new GraphicsPath();
			path.AddRectangle(new RectangleF(0, 0, 100, 100));

			brush = new PathGradientBrush(path);

			// Set the color at the center of the path to blue.
			brush.CenterColor = Color.FromArgb(255, 0, 0, 255);

			// Set the color along the entire boundary 
			// of the path to aqua.
			Color[] colors = { Color.FromArgb(255, 0, 255, 255) };
			brush.SurroundColors = colors;
		}

		protected override void Draw(StatementInstance StatementInstance)
		{
			Graphics.PageUnit = GraphicsUnit.Millimeter;
			Graphics.PageScale = 0.5F;
			Graphics.FillRectangle(brush, StatementInstance.Position.X, StatementInstance.Position.Y, 100.0F, 100.0F);
		}
	}
}