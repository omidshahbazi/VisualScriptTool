// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class IfStatementDrawer : ControlStatementDrawer
	{
		private Brush backBrush = null;

		protected override float MinimumWidth
		{
			get { return 140.0F; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(IfStatement) }; }
		}

		public IfStatementDrawer(IStatementInstanceHolder StatementInstanceHolder) :
			base(StatementInstanceHolder)
		{
			backBrush = new SolidBrush(Color.Black);
		}

		protected override void Draw(StatementInstance StatementInstance)
		{
			StatementInstance.BodySize = new SizeF(StatementInstance.HeaderSize.Width, 60.0F);

			DrawFillRectangle(0.0F, StatementInstance.HeaderSize.Height, StatementInstance.BodySize.Width, StatementInstance.BodySize.Height, backBrush);
		}

		public override void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
			base.DrawConections(Graphics, StatementInstance);

			IfStatement statement = (IfStatement)StatementInstance.Statement;

			CubicSPLine spline = new CubicSPLine();
			spline.Update(StatementInstance.Position, new PointF(100, 50), StatementInstanceHolder.GetByStatement(statement.Statement).Position, new PointF(280, 500));
			spline.Draw(Graphics, Pens.Red);
		}
	}
}