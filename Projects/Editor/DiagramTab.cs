// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Editor.Properties;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor
{
	public class DiagramTab : TabPage
	{
		private bool drawShadow = true;
		private bool drawGrid = true;
		private Image gridImage;
		private Canvas canvas = null;
		private bool isDirty = false;

		public bool IsDirty
		{
			get { return isDirty; }
			set
			{
				isDirty = value;

				UpdateTabText();
			}
		}

		public StatementInstanceList Statements
		{
			get;
			set;
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

		public bool DrawGrid
		{
			get { return drawGrid; }
			set
			{
				drawGrid = value;
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

		public int ShadowThickness
		{
			get { return shadowThickness; }
			set
			{
				shadowThickness = value;
				Invalidate();
			}
		}

		private Color shadowColor1 = Color.FromArgb(100, 0, 0, 0);
		private Color shadowColor2 = Color.FromArgb(0, 0, 0, 0);
		private int shadowThickness = 16;

		public DiagramTab(string Name)
		{
			Statements = new StatementInstanceList();

			canvas = new Canvas();
			canvas.BackColor = Color.DimGray;
			canvas.Dock = DockStyle.Fill;
			canvas.DrawCanvas += DrawCanvas;
			canvas.MinimumZoom = 0.5F;
			canvas.MaximumZoom = 1.0F;

			Controls.Add(canvas);

			this.Name = Name;
			Text = Name;
		}

		public void New()
		{

			IsDirty = true;
		}

		private void DrawCanvas(Graphics Graphics)
		{
			if (drawGrid && gridImage != null)
			{
				PointF pointF1 = canvas.ScreenToCanvas(PointF.Empty);
				PointF pointF2 = canvas.ScreenToCanvas(new PointF(ClientSize.Width, ClientSize.Height));
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
						Graphics.DrawImage(gridImage, x, y, gridImage.Width, gridImage.Height);
						y += gridImage.Height;
					}
					x += gridImage.Width;
				}
			}

			Pen originPen = new Pen(Color.Black);

			Graphics.DrawLine(originPen, 0.0F, canvas.Height / 2.0F, canvas.Width / 2.0F, 0 )

			for (int i = 0; i < Statements.Count; ++i)
			{
				StatementInstance statementInstance = Statements[i];

				StatementDrawer.Draw(Graphics, statementInstance);
			}


			//e.Graphics.Transform = identityMatrix;
			//if (drawShadow)
			//{
			//	Rectangle rect1 = new Rectangle(0, 0, shadowThickness, Height);
			//	Rectangle rect2 = new Rectangle(Width - shadowThickness, 0, shadowThickness, Height);
			//	Rectangle rect3 = new Rectangle(0, 0, Width, shadowThickness);
			//	Rectangle rect4 = new Rectangle(0, Height - shadowThickness, Width, shadowThickness);
			//	LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(rect1, shadowColor1, shadowColor2, 0.0f);
			//	--rect1.Width;
			//	e.Graphics.FillRectangle(linearGradientBrush1, rect1);
			//	LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rect2, shadowColor1, shadowColor2, 180f);
			//	e.Graphics.FillRectangle(linearGradientBrush2, rect2);
			//	LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(rect3, shadowColor1, shadowColor2, 90f);
			//	--rect3.Height;
			//	e.Graphics.FillRectangle(linearGradientBrush3, rect3);
			//	LinearGradientBrush linearGradientBrush4 = new LinearGradientBrush(rect4, shadowColor1, shadowColor2, 270f);
			//	e.Graphics.FillRectangle(linearGradientBrush4, rect4);
			//	linearGradientBrush1.Dispose();
			//	linearGradientBrush2.Dispose();
			//	linearGradientBrush3.Dispose();
			//	linearGradientBrush4.Dispose();
			//}
		}

		private void SomethingChanged(object sender, System.EventArgs e)
		{
			IsDirty = true;
		}

		private void UpdateTabText()
		{
			if (isDirty)
				Text = Name + "*";
			else
				Text = Name;
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			this.ResumeLayout(false);
		}

		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
		}
	}
}