// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class ControlStatementDrawer : Drawer
	{
		public static readonly Color EXECUTION_SLOT_COLOR = Color.White;
		public const float LINE_START_OFFSET_AMOUNT = 100.0F;

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

		public override void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
			base.DrawConections(Graphics, StatementInstance);

			Slot[] slots = StatementInstance.Slots;

			for (int i = 0; i < slots.Length; ++i)
			{
				Slot slot = slots[i];

				if (slot.ConnectedSlot != null)
					DrawLine(slot, slot.ConnectedSlot, Pens.White);
			}
		}

		protected void DrawLine(Slot From, Slot To, Pen Pen)
		{
			PointF startOffset = PointF.Empty;
			PointF endOffset = PointF.Empty;

			startOffset.X = DirectionToOffset(From);
			endOffset.X = DirectionToOffset(To);

			line.Update(From.Center, startOffset, To.Center, endOffset);
			line.Draw(Graphics, Pen);
		}

		private float DirectionToOffset(Slot Slot)
		{
			if (Slot.IsLeftAligned)
				return -LINE_START_OFFSET_AMOUNT;

			return LINE_START_OFFSET_AMOUNT;
		}
	}
}