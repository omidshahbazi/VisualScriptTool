// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor
{
	public class GridCanvas : Canvas
	{
		protected override void OnDrawCanvas(Graphics Graphics)
		{
			base.OnDrawCanvas(Graphics);

			Pen originPen = new Pen(Color.Black, 1.0F);
			Pen gridPen = new Pen(Color.Gray, 0.5F);

			PointF min = ScreenToCanvas(PointF.Empty);
			PointF max = ScreenToCanvas(new PointF(Width, Height));

			//for (float x = min.X; x < max.X; x += 10)
			//	Graphics.DrawLine(gridPen, x, min.Y, x, max.Y);

			//for (float y = min.Y; y < max.Y; y += 10)
			//	Graphics.DrawLine(gridPen, min.X, y, max.X, y);

			Graphics.DrawLine(originPen, min.X, 0.0F, max.X, 0.0F);
			Graphics.DrawLine(originPen, 0.0F, min.Y, 0.0F, max.Y);
		}
	}
}