// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class VariableDrawer : Drawer
	{
		private Brush backBrush = null;

		protected override float MinimumWidth
		{
			get { return 75.0F; }
		}

		protected override Color HeaderBackColor
		{
			get { return Color.Purple; }
		}

		public VariableDrawer()
		{
			backBrush = new SolidBrush(Color.Black);
		}

		protected override void Draw(StatementInstance StatementInstance)
		{
			DrawRectangle(0.0F, HeaderSize.Height, HeaderSize.Width, 100.0F, backBrush);
        }
	}
}