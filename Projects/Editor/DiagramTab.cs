// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor
{
	public class DiagramTab : TabPage
	{
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

		public DiagramTab(string Name)
		{
			Statements = new StatementInstanceList();

			canvas = new Canvas();
			canvas.BackColor = Color.FromArgb(73, 73, 73);
			canvas.BorderStyle = BorderStyle.FixedSingle;
			canvas.Dock = DockStyle.Fill;
			canvas.DrawGrid = true;
			canvas.DrawShadow = true;
			canvas.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			//canvas.GridImage = Resources.GridPattern;
			canvas.Location = new Point(3, 20);
			canvas.PanX = 0F;
			canvas.PanY = 0F;
			canvas.ShadowThickness = 31;
			canvas.TabIndex = 0;
			canvas.Zoom = 1.0F;
			canvas.DrawCanvas += DrawCanvas;

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
			for (int i = 0; i < Statements.Count; ++i)
			{
				StatementInstance statementInstance = Statements[i];

				StatementDrawer.Draw(Graphics, statementInstance);
			}
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
	}
}