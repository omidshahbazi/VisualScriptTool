// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class ExecuterStatementDrawer : ControlStatementDrawer
	{
		protected override Color HeaderBackColor
		{
			get { return Color.LightSkyBlue; }
		}

		protected override float MinimumWidth
		{
			get { return 170.0F; }
		}

		public override uint RowCount
		{
			get { return 1; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(ExecuterStatement) }; }
		}

		public ExecuterStatementDrawer()
		{
		}
	}
}