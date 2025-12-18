// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;

namespace VisualScriptTool.Editor.Extensions
{
	public static class RectangleFExtensions
	{
		public static RectangleF GetRectBetweenPoints(PointF A, PointF B)
		{
			RectangleF rect = new RectangleF();
			rect.X = (A.X < B.X ? A.X : B.X);
			rect.Y = (A.Y < B.Y ? A.Y : B.Y);
			rect.Width = Math.Abs(A.X - B.X);
			rect.Height = Math.Abs(A.Y - B.Y);
			return rect;
		}

		public static RectangleF Extend(this RectangleF A, RectangleF B)
		{
			float x = Math.Min(A.X, B.X);
			float y = Math.Min(A.Y, B.Y);

			float right = Math.Max(A.X + A.Width, B.X + B.Width);
			float bottom = Math.Max(A.Y + A.Height, B.Y + B.Height);

			return new RectangleF(x, y, right - x, bottom - y);
		}
	}
}