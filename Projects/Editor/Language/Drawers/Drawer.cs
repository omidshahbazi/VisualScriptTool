// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements;

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

		private Brush headeTextBrush = null;
		private Brush headeBackBrush = null;
		private Brush bodyBackBrush = null;
		private Brush executeSlotBrush = null;
		private Brush variableSlotBrush = null;
		private Brush argumentSlotBrush = null;

		protected IStatementInstanceHolder StatementInstanceHolder
		{
			get;
			private set;
		}

		protected Graphics Graphics
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

		public abstract uint SlotsCount
		{
			get;
		}

		protected virtual float BodyHeight
		{
			get { return ROW_HEIGHT * SlotsCount; }
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

		public Drawer(IStatementInstanceHolder StatementInstanceHolder)
		{
			this.StatementInstanceHolder = StatementInstanceHolder;

			Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
			HeaderTextColor = Color.White;
			headeTextBrush = new SolidBrush(HeaderTextColor);
			headeBackBrush = new SolidBrush(HeaderBackColor);
			bodyBackBrush = new SolidBrush(BodyBackColor);
			executeSlotBrush = new SolidBrush(ControlStatementDrawer.EXECUTION_SLOT_COLOR);
			variableSlotBrush = new SolidBrush(VariableDrawer.HEADER_COLOR);
			argumentSlotBrush = new SolidBrush(VariableDrawer.HEADER_COLOR);
		}

		public void Draw(Graphics Graphics, StatementInstance StatementInstance)
		{
			this.Graphics = Graphics;

			SizeF headerSize = MeasureString(StatementInstance.Statement.Name) + new SizeF(TWO_HEADER_TEXT_MARGIN, TWO_HEADER_TEXT_MARGIN);
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

			DrawFillRectangle(StatementInstance.Position.X, StatementInstance.Position.Y, StatementInstance.HeaderSize.Width, StatementInstance.HeaderSize.Height, headeBackBrush);

			DrawString(statement.Name, StatementInstance.Position.X + HEADER_TEXT_MARGIN, StatementInstance.Position.Y + HEADER_TEXT_MARGIN, headeTextBrush);
		}

		protected virtual void DrawBody(StatementInstance StatementInstance)
		{
			DrawFillRectangle(StatementInstance.Position.X, StatementInstance.Position.Y + StatementInstance.HeaderSize.Height, StatementInstance.BodySize.Width, StatementInstance.BodySize.Height, bodyBackBrush);

			DrawSlots(StatementInstance);
		}

		protected virtual void DrawSlots(StatementInstance StatementInstance)
		{
			Slot[] slots = StatementInstance.Slots;

			for (int i = 0; i < slots.Length; ++i)
				DrawSlot(slots[i]);
		}

		public virtual void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
		}

		protected virtual void DrawSlot(Slot Slot)
		{
			Slot.Bounds = (Slot.IsLeftAligned ? GetLeftSlotBounds(Slot) : GetRightSlotBounds(Slot));

			PointF position = Slot.Position;

			if (!string.IsNullOrEmpty(Slot.Name))
			{
				SizeF nameSize = MeasureString(Slot.Name);

				PointF namePosition = new PointF(Slot.IsLeftAligned ? Slot.Bounds.Right + SLOT_MARGIN : Slot.Bounds.Left - nameSize.Width - SLOT_MARGIN, position.Y - ((nameSize.Height - Slot.Bounds.Height) / 2.0F));
				DrawString(Slot.Name, namePosition.X, namePosition.Y, headeTextBrush);
            }

			switch (Slot.Type)
			{
				case Slot.Types.Getter:
				case Slot.Types.Setter:
					DrawFillTriangle(position.X, position.Y, position.X, position.Y + SLOT_SIZE, position.X + SLOT_SIZE, position.Y + HALF_SLOT_SIZE, variableSlotBrush);
					break;

				case Slot.Types.EntryPoint:
				case Slot.Types.Executer:
					DrawFillCircle(position.X, position.Y, SLOT_SIZE, executeSlotBrush);
					break;

				case Slot.Types.Argument:
					DrawFillPolygon(argumentSlotBrush, new PointF[] { new PointF(position.X, position.Y + HALF_SLOT_SIZE), new PointF(position.X + HALF_SLOT_SIZE, position.Y), new PointF(position.X + SLOT_SIZE, position.Y + HALF_SLOT_SIZE), new PointF(position.X + HALF_SLOT_SIZE, position.Y + SLOT_SIZE) });
					break;
			}
		}

		protected void DrawString(string Value, float X, float Y, Brush Brush)
		{
			DrawString(Value, X, Y, Brush, Font);
		}

		protected void DrawString(string Value, float X, float Y, Brush Brush, Font Font)
		{
			Graphics.DrawString(Value, Font, Brush, X, Y);
		}

		protected void DrawTriangle(float X1, float Y1, float X2, float Y2, float X3, float Y3, Pen Pen)
		{
			DrawPolygon(Pen, new PointF[] { new PointF(X1, Y1), new PointF(X2, Y2), new PointF(X3, Y3) });
		}

		protected void DrawFillTriangle(float X1, float Y1, float X2, float Y2, float X3, float Y3, Brush Brush)
		{
			DrawFillPolygon(Brush, new PointF[] { new PointF(X1, Y1), new PointF(X2, Y2), new PointF(X3, Y3) });
		}

		protected void DrawPolygon(Pen Pen, params PointF[] Points)
		{
			Graphics.DrawPolygon(Pen, Points);
		}

		protected void DrawFillPolygon(Brush Brush, params PointF[] Points)
		{
			Graphics.FillPolygon(Brush, Points);
		}

		protected void DrawRectangle(float X, float Y, float Width, float Height, Pen Pen)
		{
			Graphics.DrawRectangle(Pen, X, Y, Width, Height);
		}

		protected void DrawFillRectangle(float X, float Y, float Width, float Height, Brush Brush)
		{
			Graphics.FillRectangle(Brush, X, Y, Width, Height);
		}

		protected void DrawCircle(float X, float Y, float Radius, Pen Pen)
		{
			Graphics.DrawEllipse(Pen, new RectangleF(X, Y, Radius, Radius));
		}

		protected void DrawFillCircle(float X, float Y, float Radius, Brush Brush)
		{
			Graphics.FillEllipse(Brush, new RectangleF(X, Y, Radius, Radius));
		}

		protected SizeF MeasureString(string Value)
		{
			return MeasureString(Value, Font);
		}

		protected SizeF MeasureString(string Value, Font Font)
		{
			return Graphics.MeasureString(Value, Font);
		}

		protected StatementInstance GetInstanceByStatement(Statement Statement)
		{
			return StatementInstanceHolder.GetByStatement(Statement);
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

		protected virtual PointF GetLeftSlotConnectionPosition(Slot Slot)
		{
			return new PointF(Slot.StatementInstance.Bounds.Left, Slot.StatementInstance.Bounds.Top + Slot.StatementInstance.HeaderSize.Height + GetSlotYOffset(Slot));
		}

		protected virtual PointF GetRightSlotConnectionPosition(Slot Slot)
		{
			return new PointF(Slot.StatementInstance.Bounds.Right, Slot.StatementInstance.Bounds.Top + Slot.StatementInstance.HeaderSize.Height + GetSlotYOffset(Slot));
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

		public abstract bool IsLeftSlotActive(uint Index);
		public abstract bool IsRightSlotActive(uint Index);
	}
}
