// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;

namespace VisualScriptTool.Renderer
{
	class Device : IDevice
	{
		public Graphics Graphics
		{
			get;
			set;
		}

		void IDevice.DrawString(string Value, float X, float Y, Brush Brush, Font Font)
		{
			Graphics.DrawString(Value, Font, Brush, X, Y);
		}

		void IDevice.DrawTriangle(float X1, float Y1, float X2, float Y2, float X3, float Y3, Pen Pen)
		{
			((IDevice)this).DrawPolygon(Pen, new PointF[] { new PointF(X1, Y1), new PointF(X2, Y2), new PointF(X3, Y3) });
		}

		void IDevice.DrawFillTriangle(float X1, float Y1, float X2, float Y2, float X3, float Y3, Brush Brush)
		{
			((IDevice)this).DrawFillPolygon(Brush, new PointF[] { new PointF(X1, Y1), new PointF(X2, Y2), new PointF(X3, Y3) });
		}

		void IDevice.DrawLine(float X1, float Y1, float X2, float Y2, Pen Pen)
		{
			Graphics.DrawLine(Pen, X1, Y1, X2, Y2);
		}

		void IDevice.DrawPolygon(Pen Pen, params PointF[] Points)
		{
			Graphics.DrawPolygon(Pen, Points);
		}

		void IDevice.DrawFillPolygon(Brush Brush, params PointF[] Points)
		{
			Graphics.FillPolygon(Brush, Points);
		}

		void IDevice.DrawRectangle(float X, float Y, float Width, float Height, Pen Pen)
		{
			Graphics.DrawRectangle(Pen, X, Y, Width, Height);
		}

		void IDevice.DrawLines(PointF[] Points, Pen Pen)
		{
			Graphics.DrawLines(Pen, Points);
		}

		void IDevice.DrawFillRectangle(float X, float Y, float Width, float Height, Brush Brush)
		{
			Graphics.FillRectangle(Brush, X, Y, Width, Height);
		}

		void IDevice.DrawCircle(float X, float Y, float Radius, Pen Pen)
		{
			Graphics.DrawEllipse(Pen, new RectangleF(X, Y, Radius, Radius));
		}

		void IDevice.DrawFillCircle(float X, float Y, float Radius, Brush Brush)
		{
			Graphics.FillEllipse(Brush, new RectangleF(X, Y, Radius, Radius));
		}

		SizeF IDevice.MeasureString(string Value, Font Font)
		{
			return Graphics.MeasureString(Value, Font);
		}
	}
}
