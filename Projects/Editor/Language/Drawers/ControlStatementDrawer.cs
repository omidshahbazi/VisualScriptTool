﻿// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class ControlStatementDrawer : Drawer
	{
		public enum Directions
		{
			In = 0,
			Out
		}

		private const float LINE_START_OFFSET_AMOUNT = 100.0F;

		private CubicSPLine line = new CubicSPLine();

		protected Pen ExecuteConnectionPen
		{
			get;
			private set;
		}

		protected Pen VariableConnectionPen
		{
			get;
			private set;
		}

		public ControlStatementDrawer(IStatementInstanceHolder StatementInstanceHolder) :
			base(StatementInstanceHolder)
		{
			line = new CubicSPLine();
			ExecuteConnectionPen = new Pen(Color.White, 2.0F);
			VariableConnectionPen = new Pen(Color.Purple, 1.5F);
		}

		protected override Color HeaderBackColor
		{
			get { return Color.DarkGray; }
		}

		public override void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
			base.DrawConections(Graphics, StatementInstance);

			ControlStatement statement = (ControlStatement)StatementInstance.Statement;

			if (statement.CompleteStatement != null)
				DrawLine(GetRightSlotPosition(StatementInstance, 0), Directions.Out, GetLeftSlotPosition(GetInstanceByStatement(statement.CompleteStatement), 0), Directions.In, ExecuteConnectionPen);
		}

		protected void DrawLine(PointF Start, Directions StartDirection, PointF End, Directions EndDirection, Pen Pen)
		{
			PointF startOffset = PointF.Empty;
			PointF endOffset = PointF.Empty;

			startOffset.X = DirectionToOffset(StartDirection);
			endOffset.X = DirectionToOffset(EndDirection);

			line.Update(Start, startOffset, End, endOffset);
			line.Draw(Graphics, Pen);
		}

		private float DirectionToOffset(Directions Direction)
		{
			if (Direction == Directions.In)
				return -LINE_START_OFFSET_AMOUNT;

			return LINE_START_OFFSET_AMOUNT;
		}
	}
}