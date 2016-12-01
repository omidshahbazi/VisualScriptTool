// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Editor.Properties;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor
{
	public class DiagramTab : TabPage
	{
		private StatementCanvas canvas = null;
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
			get { return canvas.Statements; }
		}

		public DiagramTab(string Name)
		{
			canvas = new StatementCanvas();
			canvas.BackColor = Color.DimGray;
			canvas.Dock = DockStyle.Fill;
			canvas.MinimumZoom = 0.5F;
			canvas.MaximumZoom = 1.0F;
			canvas.GraphicsUnit = GraphicsUnit.Millimeter;

			Controls.Add(canvas);

			this.Name = Name;
			Text = Name;
		}

		public void New()
		{

			IsDirty = true;
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