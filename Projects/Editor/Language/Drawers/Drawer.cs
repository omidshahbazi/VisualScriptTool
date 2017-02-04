// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class Drawer
	{
		protected const float HEADER_TEXT_MARGIN = 1.0F;
		protected const float TWO_HEADER_TEXT_MARGIN = HEADER_TEXT_MARGIN * 2;
		protected const float ROW_HEIGHT = 30.0F;
		protected const float HALF_ROW_HEIGHT = ROW_HEIGHT / 2;
		protected const float SLOT_SIZE = 10.0F;
		protected const float HALF_SLOT_SIZE = SLOT_SIZE / 2;
		protected const float SLOT_MARGIN = 10.0F;
		public static readonly Color EXECUTION_SLOT_COLOR = Color.White;
		public const float LINE_START_OFFSET_AMOUNT = 100.0F;

		private Brush headeTextBrush = null;
		private Brush headeBackBrush = null;
		private Brush bodyBackBrush = null;
		private Brush executeSlotBrush = null;
		private Brush variableSlotBrush = null;
		private Brush argumentSlotBrush = null;
		private static Pen executionPen = null;
		private static Pen argumentPen = null;

		private CubicSPLine line = new CubicSPLine();

		protected IDevice Device
		{
			get;
			private set;
		}

		protected virtual Font Font
		{
			get;
			private set;
		}

		protected virtual Color HeaderTextColor
		{
			get;
		}

		protected abstract float MinimumWidth
		{
			get;
		}

		public abstract uint RowCount
		{
			get;
		}

		protected virtual float BodyHeight
		{
			get { return ROW_HEIGHT * RowCount; }
		}

		protected abstract Color HeaderBackColor
		{
			get;
		}

		protected abstract Color BodyBackColor
		{
			get;
		}

		public abstract Type[] StatementTypes
		{
			get;
		}

		public Drawer()
		{
			Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
			HeaderTextColor = Color.White;
			headeTextBrush = new SolidBrush(HeaderTextColor);
			headeBackBrush = new SolidBrush(HeaderBackColor);
			bodyBackBrush = new SolidBrush(BodyBackColor);
			executeSlotBrush = new SolidBrush(ControlStatementDrawer.EXECUTION_SLOT_COLOR);
			variableSlotBrush = new SolidBrush(VariableDrawer.HEADER_COLOR);
			argumentSlotBrush = new SolidBrush(VariableDrawer.HEADER_COLOR);
			executionPen = new Pen(EXECUTION_SLOT_COLOR, 2.0F);
			argumentPen = new Pen(VariableDrawer.HEADER_COLOR, 1.0F);

			line = new CubicSPLine();
		}

		public void Draw(IDevice Device, StatementInstance StatementInstance)
		{
			this.Device = Device;

			SizeF headerSize = Device.MeasureString(StatementInstance.Statement.Name, Font) + new SizeF(TWO_HEADER_TEXT_MARGIN, TWO_HEADER_TEXT_MARGIN);
			headerSize.Width = Math.Max(headerSize.Width, MinimumWidth);
			StatementInstance.HeaderSize = headerSize;

			DrawHeader(StatementInstance);

			StatementInstance.BodySize = new SizeF(StatementInstance.HeaderSize.Width, BodyHeight);

			StatementInstance.UpdateBounds();

			DrawBody(StatementInstance);
		}

		protected virtual void DrawHeader(StatementInstance StatementInstance)
		{
			Statement statement = StatementInstance.Statement;

			Device.DrawFillRectangle(StatementInstance.Position.X, StatementInstance.Position.Y, StatementInstance.HeaderSize.Width, StatementInstance.HeaderSize.Height, headeBackBrush);

			Device.DrawString(statement.Name, StatementInstance.Position.X + HEADER_TEXT_MARGIN, StatementInstance.Position.Y + HEADER_TEXT_MARGIN, headeTextBrush, Font);
		}

		protected virtual void DrawBody(StatementInstance StatementInstance)
		{
			Device.DrawFillRectangle(StatementInstance.Position.X, StatementInstance.Position.Y + StatementInstance.HeaderSize.Height, StatementInstance.BodySize.Width, StatementInstance.BodySize.Height, bodyBackBrush);

			DrawSlots(StatementInstance);
		}

		protected virtual void DrawSlots(StatementInstance StatementInstance)
		{
			Slot[] slots = StatementInstance.Slots;

			for (int i = 0; i < slots.Length; ++i)
				DrawSlot(slots[i]);
		}

		public virtual void DrawConections(StatementInstance StatementInstance)
		{
			Slot[] slots = StatementInstance.Slots;

			for (int i = 0; i < slots.Length; ++i)
			{
				Slot slot = slots[i];

				if (slot.ConnectedSlot != null)
					DrawLine(slot, slot.ConnectedSlot);
			}
		}

		protected virtual void DrawSlot(Slot Slot)
		{
			Slot.Bounds = (Slot.IsLeftAligned ? GetLeftSlotBounds(Slot) : GetRightSlotBounds(Slot));

			PointF position = Slot.Position;

			if (!string.IsNullOrEmpty(Slot.Name))
			{
				SizeF nameSize = Device.MeasureString(Slot.Name, Font);

				PointF namePosition = new PointF(Slot.IsLeftAligned ? Slot.Bounds.Right + SLOT_MARGIN : Slot.Bounds.Left - nameSize.Width - SLOT_MARGIN, position.Y - ((nameSize.Height - Slot.Bounds.Height) / 2.0F));
				Device.DrawString(Slot.Name, namePosition.X, namePosition.Y, headeTextBrush, Font);
			}

			switch (Slot.Type)
			{
				case Slot.Types.Getter:
					//case Slot.Types.Setter:
					Device.DrawFillTriangle(position.X, position.Y, position.X, position.Y + SLOT_SIZE, position.X + SLOT_SIZE, position.Y + HALF_SLOT_SIZE, variableSlotBrush);
					break;

				case Slot.Types.EntryPoint:
				case Slot.Types.Executer:
					Device.DrawFillCircle(position.X, position.Y, SLOT_SIZE, executeSlotBrush);
					break;

				case Slot.Types.Argument:
					Device.DrawFillPolygon(argumentSlotBrush, new PointF[] { new PointF(position.X, position.Y + HALF_SLOT_SIZE), new PointF(position.X + HALF_SLOT_SIZE, position.Y), new PointF(position.X + SLOT_SIZE, position.Y + HALF_SLOT_SIZE), new PointF(position.X + HALF_SLOT_SIZE, position.Y + SLOT_SIZE) });
					break;
			}
		}

		protected void DrawLine(Slot From, Slot To)
		{
			PointF startOffset = PointF.Empty;
			PointF endOffset = PointF.Empty;

			startOffset.X = DirectionToOffset(From);
			endOffset.X = DirectionToOffset(To);

			line.Update(From.Center, startOffset, To.Center, endOffset);
			line.Draw(Device, GetPen(From.Type));
		}

		protected virtual float GetSlotYOffset(Slot Slot)
		{
			return (Slot.Index * ROW_HEIGHT) + HALF_ROW_HEIGHT;
		}

		protected virtual PointF GetLeftSlotPosition(Slot Slot)
		{
			return new PointF(Slot.StatementInstance.Bounds.Left + SLOT_MARGIN, Slot.StatementInstance.Bounds.Top + Slot.StatementInstance.HeaderSize.Height + GetSlotYOffset(Slot));
		}

		protected virtual PointF GetRightSlotPosition(Slot Slot)
		{
			return new PointF(Slot.StatementInstance.Bounds.Right - SLOT_MARGIN, Slot.StatementInstance.Bounds.Top + Slot.StatementInstance.HeaderSize.Height + GetSlotYOffset(Slot));
		}

		public virtual RectangleF GetLeftSlotBounds(Slot Slot, float EnlargeAmount = 0.0F)
		{
			PointF position = GetLeftSlotPosition(Slot);

			float halfEnlargeAmount = EnlargeAmount / 2.0F;

			return new RectangleF(position.X - HALF_SLOT_SIZE - halfEnlargeAmount, position.Y - HALF_SLOT_SIZE - halfEnlargeAmount, SLOT_SIZE + EnlargeAmount, SLOT_SIZE + EnlargeAmount);
		}

		public virtual RectangleF GetRightSlotBounds(Slot Slot, float EnlargeAmount = 0.0F)
		{
			PointF position = GetRightSlotPosition(Slot);

			float halfEnlargeAmount = EnlargeAmount / 2.0F;

			return new RectangleF(position.X - HALF_SLOT_SIZE - halfEnlargeAmount, position.Y - HALF_SLOT_SIZE - halfEnlargeAmount, SLOT_SIZE + EnlargeAmount, SLOT_SIZE + EnlargeAmount);
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

				//case Slot.Types.Setter:
				case Slot.Types.Getter:
				case Slot.Types.Argument:
					return argumentPen;
			}

			return null;
		}
	}
}
