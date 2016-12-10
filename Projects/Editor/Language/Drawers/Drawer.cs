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

		protected abstract float BodyHeight
		{
			get;
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

		protected virtual float GetSlotYOffset(uint Index)
		{
			return (Index * SLOT_HEIGHT) + HALF_SLOT_HEIGHT;
		}

		protected virtual PointF GetLeftSlotPosition(StatementInstance StatementInstance, uint Index)
		{
			return new PointF(StatementInstance.Bounds.Left + SLOT_MARGIN, StatementInstance.Bounds.Top + StatementInstance.HeaderSize.Height + GetSlotYOffset(Index));
		}

		protected virtual PointF GetRightSlotPosition(StatementInstance StatementInstance, uint Index)
		{
			return new PointF(StatementInstance.Bounds.Right - SLOT_MARGIN, StatementInstance.Bounds.Top + StatementInstance.HeaderSize.Height + GetSlotYOffset(Index));
		}

		protected virtual PointF GetLeftSlotConnectionPosition(StatementInstance StatementInstance, uint Index)
		{
			return new PointF(StatementInstance.Bounds.Left, StatementInstance.Bounds.Top + StatementInstance.HeaderSize.Height + GetSlotYOffset(Index));
		}

		protected virtual PointF GetRightSlotConnectionPosition(StatementInstance StatementInstance, uint Index)
		{
			return new PointF(StatementInstance.Bounds.Right, StatementInstance.Bounds.Top + StatementInstance.HeaderSize.Height + GetSlotYOffset(Index));
		}
	}
}
