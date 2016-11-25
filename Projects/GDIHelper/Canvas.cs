// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VisualScriptTool.GDIHelper
{
	public class Canvas : UserControl
	{
		public delegate void DrawCanvasHandler(Graphics Graphics);
		public delegate void DrawScreenHandler(Graphics Graphics);

		private Matrix identityMatrix = new Matrix();
		private bool drawGrid = true;
		private bool drawShadow = true;
		private Image gridImage;
		private PointF panAmount;
		private float zoomAmount = 1;

		private PointF[] pntArr = new PointF[1] { PointF.Empty };

		private Color shadowColor1 = Color.FromArgb(100, 0, 0, 0);
		private Color shadowColor2 = Color.FromArgb(0, 0, 0, 0);
		private int shadowThickness = 16;

		protected Matrix transform = new Matrix();

		public event DrawCanvasHandler DrawCanvas;
		public event DrawCanvasHandler DrawScreen;

		public bool DrawGrid
		{
			get { return drawGrid; }
			set
			{
				drawGrid = value;
				Invalidate();
			}
		}

		public bool DrawShadow
		{
			get { return drawShadow; }
			set
			{
				drawShadow = value;
				Invalidate();
			}
		}

		public Image GridImage
		{
			get { return gridImage; }
			set
			{
				gridImage = value;
				Invalidate();
			}
		}

		public PointF Pan
		{
			get { return panAmount; }
			set
			{
				panAmount = value;
				Invalidate();
			}
		}

		public float PanX
		{
			get { return panAmount.X; }
			set
			{
				panAmount.X = value;
				Invalidate();
			}
		}

		public float PanY
		{
			get { return panAmount.Y; }
			set
			{
				panAmount.Y = value;
				Invalidate();
			}
		}

		public float Zoom
		{
			get { return zoomAmount; }
			set
			{
				if (value < 0.5F || value > 4.0F)
					return;

				var halfscreen = new PointF(Width / 2.0F, Height / 2.0F);
				var wCenter = ScreenToCanvas(halfscreen);
				zoomAmount = Math.Min(Math.Max(value, 0.1F), 100);
				LookAt(wCenter);
				Invalidate();
			}
		}

		public int ShadowThickness
		{
			get { return shadowThickness; }
			set
			{
				shadowThickness = value;
				Invalidate();
			}
		}

		protected Point LastMousePos
		{
			get;
			private set;
		}

		protected bool Panning
		{
			get;
			private set;
		}

		public SmoothingMode SmoothingMode
		{
			get;
			set;
		}

		public Canvas()
		{
			ResizeRedraw = true;
			DoubleBuffered = true;
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			Focus();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.KeyCode == Keys.Space)
				Panning = true;
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (e.KeyCode == Keys.Space)
				Panning = false;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (!Panning)
				return;

			if (e.Button == MouseButtons.Left)
			{
				PointF pointF = new PointF(e.X - LastMousePos.X, e.Y - LastMousePos.Y);

				PanX += pointF.X;
				PanY += pointF.Y;
			}

			LastMousePos = e.Location;
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);

			Zoom += (e.Delta / 1000.0F);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			transform.Reset();
			transform.Translate(panAmount.X, panAmount.Y);
			transform.Scale(zoomAmount, zoomAmount);

			e.Graphics.Transform = transform;
			if (zoomAmount > 1)
				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			else
				e.Graphics.InterpolationMode = InterpolationMode.Default;
			if (drawGrid && gridImage != null)
			{
				PointF pointF1 = ScreenToCanvas(PointF.Empty);
				PointF pointF2 = ScreenToCanvas(new PointF(ClientSize.Width, ClientSize.Height));
				float num1 = pointF1.X - pointF1.X % gridImage.Width;
				if (pointF1.X < 0.0)
					num1 -= gridImage.Width;
				float num2 = pointF1.Y - pointF1.Y % gridImage.Height;
				if (pointF1.Y < 0.0)
					num2 -= gridImage.Height;
				float x = num1;
				while (x < (double)pointF2.X)
				{
					float y = num2;
					while (y < (double)pointF2.Y)
					{
						e.Graphics.DrawImage(gridImage, x, y, gridImage.Width, gridImage.Height);
						y += gridImage.Height;
					}
					x += gridImage.Width;
				}
			}
			e.Graphics.InterpolationMode = InterpolationMode.Default;
			e.Graphics.SmoothingMode = SmoothingMode;

			OnDrawCanvas(e.Graphics);

			e.Graphics.Transform = identityMatrix;
			if (drawShadow)
			{
				Rectangle rect1 = new Rectangle(0, 0, shadowThickness, Height);
				Rectangle rect2 = new Rectangle(Width - shadowThickness, 0, shadowThickness, Height);
				Rectangle rect3 = new Rectangle(0, 0, Width, shadowThickness);
				Rectangle rect4 = new Rectangle(0, Height - shadowThickness, Width, shadowThickness);
				LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(rect1, shadowColor1, shadowColor2, 0.0f);
				--rect1.Width;
				e.Graphics.FillRectangle(linearGradientBrush1, rect1);
				LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rect2, shadowColor1, shadowColor2, 180f);
				e.Graphics.FillRectangle(linearGradientBrush2, rect2);
				LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(rect3, shadowColor1, shadowColor2, 90f);
				--rect3.Height;
				e.Graphics.FillRectangle(linearGradientBrush3, rect3);
				LinearGradientBrush linearGradientBrush4 = new LinearGradientBrush(rect4, shadowColor1, shadowColor2, 270f);
				e.Graphics.FillRectangle(linearGradientBrush4, rect4);
				linearGradientBrush1.Dispose();
				linearGradientBrush2.Dispose();
				linearGradientBrush3.Dispose();
				linearGradientBrush4.Dispose();
			}

			OnDrawScreen(e.Graphics);
		}

		protected virtual void OnDrawCanvas(Graphics Graphics)
		{
			if (DrawCanvas != null)
				DrawCanvas(Graphics);
        }

		protected virtual void OnDrawScreen(Graphics Graphics)
		{
			if (DrawScreen != null)
				DrawScreen(Graphics);
		}

		public void LookAt(PointF Point)
		{
			var halfW = Width * 0.5F;
			var halfH = Height * 0.5F;
			Pan = new PointF(-Point.X * Zoom + halfW, -Point.Y * Zoom + halfH);
		}

		public PointF CanvasToScreen(PointF Point)
		{
			pntArr[0] = Point;
			transform.TransformPoints(pntArr);
			return pntArr[0];
		}

		public PointF ScreenToCanvas(PointF Point)
		{
			pntArr[0] = Point;
			Matrix matrix = transform.Clone();
			matrix.Invert();
			matrix.TransformPoints(pntArr);
			return pntArr[0];
		}
	}
}