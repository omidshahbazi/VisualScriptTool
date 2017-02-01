// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Editor.Extensions;

namespace VisualScriptTool.Editor
{
	public class CubicSPLine
	{
		List<PointF> points = new List<PointF>();

		public void Update(PointF Start, PointF End)
		{
			Update(Start, PointF.Empty, End, PointF.Empty);
		}

		public void Update(PointF Start, PointF StartOffset, PointF End, PointF EndOffset)
		{
			points.Clear();

			StartOffset = Start.Add(StartOffset);
			EndOffset = End.Add(EndOffset);

			for (float t = 0; t < 1.05F; t += 0.05F)
			{
				PointF end = End.Multiply((float)Math.Pow(t, 3));
				PointF endOffset = EndOffset.Multiply(3 * (1 - t) * (float)Math.Pow(t, 2));
				PointF startOffset = StartOffset.Multiply(3 * (float)Math.Pow(1 - t, 2) * t);
				PointF start = Start.Multiply((float)Math.Pow(1 - t, 3));

				points.Add(end.Add(endOffset).Add(startOffset).Add(start));
			}
		}

		public void Clear()
		{
			points.Clear();
		}

		public void Draw(Graphics Graphics, Pen Pen)
		{
			if (points.Count == 0)
				return;

			Graphics.DrawLines(Pen, points.ToArray());
		}
	}
}