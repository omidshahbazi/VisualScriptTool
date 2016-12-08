﻿// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class Drawer
	{
		private const float HEADER_TEXT_MARGIN = 1.0F;
		private const float TWO_HEADER_TEXT_MARGIN = HEADER_TEXT_MARGIN * 2;

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

		protected abstract float MinimumWidth
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

			graphics.TranslateTransform(StatementInstance.Bounds.Location.X, StatementInstance.Bounds.Location.Y);

			SizeF headerSize = MeasureString(StatementInstance.Statement.Name) + new SizeF(TWO_HEADER_TEXT_MARGIN, TWO_HEADER_TEXT_MARGIN);
			headerSize.Width = Math.Max(headerSize.Width, MinimumWidth);
			StatementInstance.HeaderSize = headerSize;

			DrawHeader(StatementInstance);

			Draw(StatementInstance);

			graphics.TranslateTransform(-StatementInstance.Bounds.Location.X, -StatementInstance.Bounds.Location.Y);
        }

		protected virtual void DrawHeader(StatementInstance StatementInstance)
		{
			Statement statement = StatementInstance.Statement;

			DrawFillRectangle(0, 0, StatementInstance.HeaderSize.Width, StatementInstance.HeaderSize.Height, headeBackBrush);

			DrawString(statement.Name, HEADER_TEXT_MARGIN, HEADER_TEXT_MARGIN, headeTextBrush);
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

		protected void DrawRectangle(float X, float Y, float Width, float Height, Pen Pen)
		{
			graphics.DrawRectangle(Pen, X, Y, Width, Height);
		}

		protected void DrawFillRectangle(float X, float Y, float Width, float Height, Brush Brush)
		{
			graphics.FillRectangle(Brush, X, Y, Width, Height);
		}

		protected SizeF MeasureString(string Value)
		{
			return MeasureString(Value, Font);
		}

		protected SizeF MeasureString(string Value, Font Font)
		{
			return graphics.MeasureString(Value, Font);
		}
	}
}
