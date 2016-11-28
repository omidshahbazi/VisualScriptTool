// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class VariableDrawer : Drawer
	{
		protected override Color HeaderBackColor
		{
			get { return Color.Pink; }
		}

		protected override void Draw(StatementInstance StatementInstance)
		{

		}
	}
}