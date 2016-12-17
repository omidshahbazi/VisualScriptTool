// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class ControlStatementDrawer : Drawer
	{
		public static readonly Color EXECUTION_SLOT_COLOR = Color.White;
		public const float LINE_START_OFFSET_AMOUNT = 100.0F;

		private CubicSPLine line = new CubicSPLine();

		private static Pen executionPen = null;
		private static Pen argumentPen = null;

		protected override Color HeaderBackColor
		{
			get { return Color.DarkGray; }
		}

		protected override Color BodyBackColor
		{
			get { return Color.Black; }
		}

		public ControlStatementDrawer()
		{
			line = new CubicSPLine();
			executionPen = new Pen(EXECUTION_SLOT_COLOR, 2.0F);
			argumentPen = new Pen(VariableDrawer.HEADER_COLOR, 1.0F);
		}

		public override void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
			base.DrawConections(Graphics, StatementInstance);

			Slot[] slots = StatementInstance.Slots;

			for (int i = 0; i < slots.Length; ++i)
			{
				Slot slot = slots[i];

				if (slot.ConnectedSlot != null)
					DrawLine(slot, slot.ConnectedSlot);
			}
		}

		protected void DrawLine(Slot From, Slot To)
		{
			PointF startOffset = PointF.Empty;
			PointF endOffset = PointF.Empty;

			startOffset.X = DirectionToOffset(From);
			endOffset.X = DirectionToOffset(To);

			line.Update(From.Center, startOffset, To.Center, endOffset);
			line.Draw(Graphics, GetPen(From.Type));
		}

		private float DirectionToOffset(Slot Slot)
		{
			if (Slot.IsLeftAligned)
				return -LINE_START_OFFSET_AMOUNT;

			return LINE_START_OFFSET_AMOUNT;
		}

		public static Pen GetPen(Slot.Types Type)
		{
			switch (Type)
			{
				case Slot.Types.EntryPoint:
				case Slot.Types.Executer:
					return executionPen;

				case Slot.Types.Setter:
				case Slot.Types.Getter:
				case Slot.Types.Argument:
					return argumentPen;
			}

			return null;
		}
	}
}