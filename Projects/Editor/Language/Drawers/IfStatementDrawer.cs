// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class IfStatementDrawer : ControlStatementDrawer
	{
		private Brush backBrush = null;
		private CubicSPLine spline = new CubicSPLine();

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
			StatementInstance.BodySize = new SizeF(StatementInstance.HeaderSize.Width, SLOT_HEIGHT * 2);

			DrawFillRectangle(0.0F, StatementInstance.HeaderSize.Height, StatementInstance.BodySize.Width, StatementInstance.BodySize.Height, backBrush);
		}

		public override void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
			base.DrawConections(Graphics, StatementInstance);

			IfStatement statement = (IfStatement)StatementInstance.Statement;

			if (statement.Statement != null)
                DrawLine(GetRightSlotPosition(StatementInstance, 1), Directions.Out, GetLeftSlotPosition(GetInstanceByStatement(statement.Statement), 0), Directions.In, ExecuteConnectionPen);

			if (statement.ElseStatment != null)
				DrawLine(GetRightSlotPosition(StatementInstance, 1), Directions.Out, GetLeftSlotPosition(GetInstanceByStatement(statement.ElseStatment), 0), Directions.In, ExecuteConnectionPen);

			if (statement.Condition != null)
				DrawLine(GetLeftSlotPosition(StatementInstance, 1), Directions.In, GetRightSlotPosition(GetInstanceByStatement(statement.Condition), 0), Directions.Out, VariableConnectionPen);
		}
	}
}