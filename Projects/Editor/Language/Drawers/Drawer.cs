// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class Drawer
	{
		private const float HEADER_TEXT_MARGIN = 2.5F;
		private const float TWO_HEADER_TEXT_MARGIN = HEADER_TEXT_MARGIN * 2;
		private const float MINIMUM_MEASURE_STRING_WITHD = 100.0F;
		private const float MINIMUM_MEASURE_STRING_HEIGHT = 20.0F;

		private Graphics graphics = null;
		private Brush headeTextBrush = null;
		private Brush headeBackBrush = null;

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

		protected abstract Color HeaderBackColor
		{
			get;
		}

		public abstract Type StatementType
		{
			get;
		}

		public Drawer()
		{
			Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
			HeaderTextColor = Color.White;
			headeTextBrush = new SolidBrush(HeaderTextColor);
			headeBackBrush = new SolidBrush(HeaderBackColor);
		}

		public void Draw(Graphics Graphics, StatementInstance StatementInstance)
		{
			graphics = Graphics;

			graphics.TranslateTransform(StatementInstance.Position.X, StatementInstance.Position.Y);

			Statement statement = StatementInstance.Statement;

			SizeF size = MeasureString(statement.Name);

			DrawRectangle(0, 0, size.Width + TWO_HEADER_TEXT_MARGIN, size.Height + TWO_HEADER_TEXT_MARGIN, headeBackBrush);

			DrawString(statement.Name, HEADER_TEXT_MARGIN, HEADER_TEXT_MARGIN, headeTextBrush);

			Draw(StatementInstance);
		}

		protected abstract void Draw(StatementInstance StatementInstance);

		protected void DrawString(string Value, float X, float Y, Brush Brush)
		{
			DrawString(Value, X, Y, Brush, Font);
		}

		protected void DrawString(string Value, float X, float Y, Brush Brush, Font Font)
		{
			graphics.DrawString(Value, Font, Brush, X, Y);
		}

		protected void DrawRectangle(float X, float Y, float Width, float Height, Brush Brush)
		{
			graphics.FillRectangle(Brush, X, Y, Width, Height);
		}

		protected SizeF MeasureString(string Value)
		{
			return MeasureString(Value, Font);
		}

		protected SizeF MeasureString(string Value, Font Font)
		{
			SizeF size = graphics.MeasureString(Value, Font);

			size.Width = Math.Max(size.Width, MINIMUM_MEASURE_STRING_WITHD);
			size.Height = Math.Max(size.Height, MINIMUM_MEASURE_STRING_HEIGHT);

			return size;
		}
	}
}
