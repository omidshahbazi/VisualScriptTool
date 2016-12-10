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

		public override uint SlotsCount
		{
			get { return 1; }
		}

		protected override Color HeaderBackColor
		{
			get { return Color.Purple; }
		}

		protected override Color BodyBackColor
		{
			get { return Color.Black; }
		}

		protected Brush VariableSlotBrush
		{
			get;
			private set;
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(BooleanVariable), typeof(FloatVariable), typeof(IntegerVariable), typeof(StringVariable) }; }
		}

		public VariableDrawer(IStatementInstanceHolder StatementInstanceHolder) :
			base(StatementInstanceHolder)
		{
			VariableSlotBrush = new SolidBrush(HeaderBackColor);
		}

		protected override void DrawBody(StatementInstance StatementInstance)
		{
			base.DrawBody(StatementInstance);

			DrawSlot(GetRightSlotPosition(StatementInstance, 0));
		}

		protected void DrawSlot(PointF Position)
		{
			DrawFillTriangle(Position.X - HALF_SLOT_SIZE, Position.Y - HALF_SLOT_SIZE, Position.X - HALF_SLOT_SIZE, Position.Y + HALF_SLOT_SIZE, Position.X + HALF_SLOT_SIZE, Position.Y, VariableSlotBrush);
		}

		public override bool IsLeftSlotActive(uint Index)
		{
			return false;
		}

		public override bool IsRightSlotActive(uint Index)
		{
			return (Index == 0);
		}
	}
}