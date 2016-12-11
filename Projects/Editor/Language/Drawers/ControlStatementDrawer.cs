// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class ControlStatementDrawer : Drawer
	{
		private const float LINE_START_OFFSET_AMOUNT = 100.0F;

		private CubicSPLine line = new CubicSPLine();

		protected override Color HeaderBackColor
		{
			get { return Color.DarkGray; }
		}

		protected override Color BodyBackColor
		{
			get { return Color.Black; }
		}

		protected Pen ExecuteConnectionPen
		{
			get;
			private set;
		}

		protected Brush ExecuteSlotBrush
		{
			get;
			private set;
		}

		protected Pen VariableConnectionPen
		{
			get;
			private set;
		}

		protected Brush VariableSlotBrush
		{
			get;
			private set;
		}

		public ControlStatementDrawer(IStatementInstanceHolder StatementInstanceHolder) :
			base(StatementInstanceHolder)
		{
			line = new CubicSPLine();
			ExecuteConnectionPen = new Pen(Color.White, 2.0F);
			ExecuteSlotBrush = new SolidBrush(ExecuteConnectionPen.Color);
			VariableConnectionPen = new Pen(Color.Purple, 1.5F);
			VariableSlotBrush = new SolidBrush(VariableConnectionPen.Color);
		}

		protected override void DrawBody(StatementInstance StatementInstance)
		{
			base.DrawBody(StatementInstance);

			//DrawExecuteSlot(GetLeftSlotPosition(StatementInstance, 0));
			//DrawExecuteSlot(GetRightSlotPosition(StatementInstance, 0));
		}

		protected void DrawExecuteSlot(PointF Position)
		{
			DrawFillCircle(Position.X - HALF_SLOT_SIZE, Position.Y - HALF_SLOT_SIZE, SLOT_SIZE, ExecuteSlotBrush);
		}

		protected void DrawVariableSlot(PointF Position)
		{
			Graphics.FillPolygon(VariableSlotBrush, new PointF[] { new PointF(Position.X - HALF_SLOT_SIZE, Position.Y), new PointF(Position.X, Position.Y + HALF_SLOT_SIZE), new PointF(Position.X + HALF_SLOT_SIZE, Position.Y), new PointF(Position.X, Position.Y - HALF_SLOT_SIZE) });
		}

		public override void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
			base.DrawConections(Graphics, StatementInstance);

			ControlStatement statement = (ControlStatement)StatementInstance.Statement;

			//if (statement.CompleteStatement != null)
				//DrawLine(GetRightSlotConnectionPosition(StatementInstance, 0), Directions.Out, GetLeftSlotConnectionPosition(GetInstanceByStatement(statement.CompleteStatement), 0), Directions.In, ExecuteConnectionPen);
		}

		//protected void DrawLine(PointF Start, Directions StartDirection, PointF End, Directions EndDirection, Pen Pen)
		//{
		//	PointF startOffset = PointF.Empty;
		//	PointF endOffset = PointF.Empty;

		//	startOffset.X = DirectionToOffset(StartDirection);
		//	endOffset.X = DirectionToOffset(EndDirection);

		//	line.Update(Start, startOffset, End, endOffset);
		//	line.Draw(Graphics, Pen);
		//}

		public override bool IsLeftSlotActive(uint Index)
		{
			return (Index == 0);
		}

		public override bool IsRightSlotActive(uint Index)
		{
			return (Index == 0);
		}

		//private float DirectionToOffset(Directions Direction)
		//{
		//	if (Direction == Directions.In)
		//		return -LINE_START_OFFSET_AMOUNT;

		//	return LINE_START_OFFSET_AMOUNT;
		//}
	}
}