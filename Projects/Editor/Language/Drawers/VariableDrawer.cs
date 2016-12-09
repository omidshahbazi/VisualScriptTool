// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class VariableDrawer : Drawer
	{
		private Brush backBrush = null;

		protected override float MinimumWidth
		{
			get { return 100.0F; }
		}

		protected override Color HeaderBackColor
		{
			get { return Color.Purple; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(BooleanVariable), typeof(FloatVariable), typeof(IntegerVariable), typeof(StringVariable) }; }
		}

		public VariableDrawer(IStatementInstanceHolder StatementInstanceHolder) :
			base(StatementInstanceHolder)
		{
			backBrush = new SolidBrush(Color.Black);
		}

		protected override void Draw(StatementInstance StatementInstance)
		{
			StatementInstance.BodySize = new SizeF(StatementInstance.HeaderSize.Width, SLOT_HEIGHT);

			DrawFillRectangle(0.0F, StatementInstance.HeaderSize.Height, StatementInstance.BodySize.Width, StatementInstance.BodySize.Height, backBrush);
		}
	}
}