// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;

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

			for (float t = 0; t < 1.05F; t += 0.05F)
			{
				PointF end = End.Multiply((float)Math.Pow(t, 3));
				PointF endOffset = EndOffset.Multiply(3 * (1 - t) * (float)Math.Pow(t, 2));
				PointF startOffset = StartOffset.Multiply(3 * (float)Math.Pow(1 - t, 2) * t);
				PointF start = Start.Multiply((float)Math.Pow(1 - t, 3));

				points.Add(end.Add(endOffset).Add(startOffset).Add(start));
			}
		}

		public void Draw(Graphics Graphics, Pen Pen)
		{
			Graphics.DrawLines(Pen, points.ToArray());
		}

		//public bool CheckCollision(PointF Point, float MaxDist)
		//{
		//	for (float t = 0; t < 1f; t += 0.1f)
		//	{
		//		Vector2 PP3 = Utilities.PointToVector2(Start);
		//		Vector2 PP2 = Utilities.PointToVector2(StartOffset);
		//		Vector2 PP1 = Utilities.PointToVector2(EndOffset);
		//		Vector2 PP0 = Utilities.PointToVector2(End);

		//		Vector2 PP3_0 = Utilities.PointToVector2(Start);
		//		Vector2 PP2_0 = Utilities.PointToVector2(StartOffset);
		//		Vector2 PP1_0 = Utilities.PointToVector2(EndOffset);
		//		Vector2 PP0_0 = Utilities.PointToVector2(End);

		//		Vector2 pt = PP0 * Mathf.Pow(t, 3) + PP1 * 3 * (1 - t) * Mathf.Pow(t, 2) + PP2 * 3 * Mathf.Pow(1 - t, 2) * t + PP3 * Mathf.Pow(1 - t, 3);
		//		var t2 = t + 0.1f;
		//		Vector2 pt2 = PP0_0 * Mathf.Pow(t2, 3) + PP1_0 * 3 * (1 - t2) * Mathf.Pow(t2, 2) + PP2_0 * 3 * Mathf.Pow(1 - t2, 2) * t2 + PP3_0 * Mathf.Pow(1 - t2, 3);
		//		if (LineToPointDistance(pt, pt2, Utilities.PointToVector2(Point)) < MaxDist)
		//			return true;
		//	}
		//	return false;
		//}

		//private static float LineToPointDistance(Vector2 V, Vector2 W, Vector2 P)
		//{
		//	float l = (V.x - W.x) * (V.x - W.x) + (V.y - W.y) * (V.y - W.y);
		//	if (l == 0)
		//		return Vector2.Distance(P, V);

		//	float t = Vector2.Dot(P - V, W - V) / l;
		//	if (t < 0)
		//		return Vector2.Distance(P, V);
		//	else if (t > 1)
		//		return Vector2.Distance(P, W);

		//	Vector2 proj = V + t * (W - V);

		//	return Vector2.Distance(P, proj);
		//}
	}
}