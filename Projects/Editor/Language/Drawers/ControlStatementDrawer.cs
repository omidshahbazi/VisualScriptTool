// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class ControlStatementDrawer : Drawer
	{
		public static readonly Color EXECUTION_SLOT_COLOR = Color.White;
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

		public ControlStatementDrawer(IStatementInstanceHolder StatementInstanceHolder) :
			base(StatementInstanceHolder)
		{
			line = new CubicSPLine();
		}

		protected override void DrawBody(StatementInstance StatementInstance)
		{
			base.DrawBody(StatementInstance);

			//DrawExecuteSlot(GetLeftSlotPosition(StatementInstance, 0));
			//DrawExecuteSlot(GetRightSlotPosition(StatementInstance, 0));
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