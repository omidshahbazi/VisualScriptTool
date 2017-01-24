// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class ForStatementDrawer : ControlStatementDrawer
	{
		private Brush backBrush = null;

		protected override float MinimumWidth
		{
			get { return 160.0F; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(ForStatement) }; }
		}

		public override uint RowCount
		{
			get { return 4; }
		}

		public ForStatementDrawer()
		{
			backBrush = new SolidBrush(Color.Black);
		}
	}
}