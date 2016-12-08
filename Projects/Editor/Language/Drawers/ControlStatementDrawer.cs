// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class ControlStatementDrawer : Drawer
	{
		protected override Color HeaderBackColor
		{
			get { return Color.DarkGray; }
		}

		public ControlStatementDrawer()
		{
		}
	}
}