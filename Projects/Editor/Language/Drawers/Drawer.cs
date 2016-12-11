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
		protected const float SLOT_HEIGHT = 30.0F;
		protected const float HALF_SLOT_HEIGHT = SLOT_HEIGHT / 2;
		protected const float SLOT_SIZE = 10.0F;
		protected const float HALF_SLOT_SIZE = SLOT_SIZE / 2;
		protected const float SLOT_MARGIN = 10.0F;

		private Brush headeTextBrush = null;
		private Brush headeBackBrush = null;
		private Brush bodyBackBrush = null;
		private Brush variableSlotBrush = null;

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
			get { return SLOT_HEIGHT * SlotsCount; }
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
			variableSlotBrush = new SolidBrush(HeaderBackColor);
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
		}

		public virtual void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
		}

		protected virtual void DrawSlot(Slot Slot)
		{
			Slot.Bounds = (Slot.Type == Slot.Types.Setter || Slot.Type == Slot.Types.EntryPoint ? GetLeftSlotBounds(Slot) : GetRightSlotBounds(Slot));

			DrawFillTriangle(Slot.Position.X - HALF_SLOT_SIZE, Slot.Position.Y - HALF_SLOT_SIZE, Slot.Position.X - HALF_SLOT_SIZE, Slot.Position.Y + HALF_SLOT_SIZE, Slot.Position.X + HALF_SLOT_SIZE, Slot.Position.Y, variableSlotBrush);
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
			Graphics.DrawPolygon(Pen, new PointF[] { new PointF(X1, Y1), new PointF(X2, Y2), new PointF(X3, Y3) });
		}

		protected void DrawFillTriangle(float X1, float Y1, float X2, float Y2, float X3, float Y3, Brush Brush)
		{
			Graphics.FillPolygon(Brush, new PointF[] { new PointF(X1, Y1), new PointF(X2, Y2), new PointF(X3, Y3) });
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
			return (Slot.Index * SLOT_HEIGHT) + HALF_SLOT_HEIGHT;
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
