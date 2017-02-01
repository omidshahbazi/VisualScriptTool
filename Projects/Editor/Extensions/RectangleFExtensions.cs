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
	}
}