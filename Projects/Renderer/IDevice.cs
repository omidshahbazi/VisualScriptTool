// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;

namespace VisualScriptTool.Renderer
{
	public interface IDevice
	{
		void DrawString(string Value, float X, float Y, Brush Brush, Font Font);

		void DrawTriangle(float X1, float Y1, float X2, float Y2, float X3, float Y3, Pen Pen);

		void DrawFillTriangle(float X1, float Y1, float X2, float Y2, float X3, float Y3, Brush Brush);

		void DrawLine(float X1, float Y1, float X2, float Y2, Pen Pen);

		void DrawPolygon(Pen Pen, params PointF[] Points);

		void DrawFillPolygon(Brush Brush, params PointF[] Points);

		void DrawRectangle(float X, float Y, float Width, float Height, Pen Pen);

		void DrawLines(PointF[] Points, Pen Pen);

		void DrawFillRectangle(float X, float Y, float Width, float Height, Brush Brush);

		void DrawCircle(float X, float Y, float Radius, Pen Pen);

		void DrawFillCircle(float X, float Y, float Radius, Brush Brush);

		SizeF MeasureString(string Value, Font Font);
	}
}
