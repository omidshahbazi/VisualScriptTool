// Copyright 2016-2017 ?????????????. All Rights Reserved.

using System.Drawing;

namespace VisualScriptTool.Editor
{
	public static class PointFExtensions
	{
		public static PointF Add(this PointF a, PointF Value)
		{
			return new PointF(a.X + Value.X, a.Y + Value.Y);
		}

		public static PointF Subtract(this PointF a, PointF Value)
		{
			return new PointF(a.X - Value.X, a.Y - Value.Y);
		}

		public static PointF Multiply(this PointF a, float Value)
		{
			return new PointF(a.X * Value, a.Y * Value);
		}
	}
}