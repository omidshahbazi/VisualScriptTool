// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class VariableDrawer : Drawer
	{
		protected override float MinimumWidth
		{
			get { return 100.0F; }
		}

		protected override float BodyHeight
		{
			get { return SLOT_HEIGHT; }
		}

		protected override Color HeaderBackColor
		{
			get { return Color.Purple; }
		}

		protected override Color BodyBackColor
		{
			get { return Color.Black; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(BooleanVariable), typeof(FloatVariable), typeof(IntegerVariable), typeof(StringVariable) }; }
		}

		public VariableDrawer(IStatementInstanceHolder StatementInstanceHolder) :
			base(StatementInstanceHolder)
		{
		}
	}
}