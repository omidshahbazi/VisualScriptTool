// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor
{
	public class GridCanvas : Canvas
	{
		private Point lastMousePosition;

		protected bool IsPanning
		{
			get;
			set;
		}

		protected override void OnDrawCanvas(IDevice Device)
		{
			base.OnDrawCanvas(Device);

			Pen originPen = new Pen(Color.Black, 1.0F);
			Pen gridPen = new Pen(Color.Gray, 0.5F);

			PointF min = ScreenToCanvas(PointF.Empty);
			PointF max = ScreenToCanvas(new PointF(Width, Height));

			//for (float x = min.X; x < max.X; x += 10)
			//	Graphics.DrawLine(gridPen, x, min.Y, x, max.Y);

			//for (float y = min.Y; y < max.Y; y += 10)
			//	Graphics.DrawLine(gridPen, min.X, y, max.X, y);

			Device.DrawLine(min.X, 0.0F, max.X, 0.0F, originPen);
			Device.DrawLine(0.0F, min.Y, 0.0F, max.Y, originPen);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == MouseButtons.Right)
				IsPanning = true;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (e.Button == MouseButtons.Right)
				IsPanning = false;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (IsPanning)
			{
				Pan = new PointF(Pan.X + (e.X - lastMousePosition.X), Pan.Y + (e.Y - lastMousePosition.Y));
				Refresh();
			}

			lastMousePosition = e.Location;
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);

			Zoom += (e.Delta / 1000.0F);

			Refresh();
		}
	}
}