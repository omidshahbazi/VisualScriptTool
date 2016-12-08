// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace VisualScriptTool.Renderer
{
	public class Canvas : UserControl
	{
		public delegate void DrawCanvasHandler(Graphics Graphics);

		protected Matrix matrix = new Matrix();

		private float zoom = 1.0F;

		private PointF[] pointArray = new PointF[1] { PointF.Empty };

		public event DrawCanvasHandler DrawCanvas;

		public PointF Pan
		{
			get;
			set;
		}

		public float Zoom
		{
			get { return zoom; }
			set
			{
				if (value < MinimumZoom)
					zoom = MinimumZoom;
				else if (value > MaximumZoom)
					zoom = MaximumZoom;
				else
					zoom = value;
			}
		}

		public float MinimumZoom
		{
			get;
			set;
		}

		public float MaximumZoom
		{
			get;
			set;
		}

		public Point Origin
		{
			get;
			set;
		}

		public int TextContrast
		{
			get;
			set;
		}

		public InterpolationMode InterpolationMode
		{
			get;
			set;
		}

		public SmoothingMode SmoothingMode
		{
			get;
			set;
		}

		public CompositingQuality CompositingQuality
		{
			get;
			set;
		}

		public GraphicsUnit GraphicsUnit
		{
			get;
			set;
		}

		public PixelOffsetMode PixelOffsetMode
		{
			get;
			set;
		}

		public TextRenderingHint TextRenderingHint
		{
			get;
			set;
		}

		public Canvas()
		{
			ResizeRedraw = true;
			DoubleBuffered = true;
			GraphicsUnit = GraphicsUnit.Pixel;
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			Focus();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			matrix.Reset();
			matrix.Translate(Pan.X, Pan.Y);
			matrix.Scale(Zoom, Zoom);
			e.Graphics.Transform = matrix;

			e.Graphics.CompositingQuality = CompositingQuality;
			e.Graphics.InterpolationMode = InterpolationMode;
			e.Graphics.SmoothingMode = SmoothingMode;
			e.Graphics.PageUnit = GraphicsUnit;
			e.Graphics.PixelOffsetMode = PixelOffsetMode;
			e.Graphics.RenderingOrigin = Origin;
			e.Graphics.TextContrast = TextContrast;
			e.Graphics.TextRenderingHint = TextRenderingHint;

			OnDrawCanvas(e.Graphics);
		}

		protected virtual void OnDrawCanvas(Graphics Graphics)
		{
			if (DrawCanvas != null)
				DrawCanvas(Graphics);
		}

		public void LookAt(PointF Point)
		{
			var halfW = Width * 0.5F;
			var halfH = Height * 0.5F;
			Pan = new PointF(-Point.X * Zoom + halfW, -Point.Y * Zoom + halfH);
		}

		public PointF CanvasToScreen(PointF Point)
		{
			pointArray[0] = Point;
			matrix.TransformPoints(pointArray);
			return pointArray[0];
		}

		public PointF ScreenToCanvas(PointF Point)
		{
			pointArray[0] = Point;
			Matrix inverseMatrix = matrix.Clone();
			inverseMatrix.Invert();
			inverseMatrix.TransformPoints(pointArray);
			return pointArray[0];
		}

		public override void Refresh()
		{
			base.Refresh();

            Invalidate();
		}
	}
}