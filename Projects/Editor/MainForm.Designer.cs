namespace VisualScriptTool.Editor
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.canvas1 = new VisualScriptTool.GDIHelper.Canvas();
			this.SuspendLayout();
			// 
			// canvas1
			// 
			this.canvas1.DrawGrid = true;
			this.canvas1.DrawShadow = true;
			this.canvas1.GridImage = null;
			this.canvas1.Location = new System.Drawing.Point(12, 12);
			this.canvas1.Name = "canvas1";
			this.canvas1.Pan = ((System.Drawing.PointF)(resources.GetObject("canvas1.Pan")));
			this.canvas1.PanX = 0F;
			this.canvas1.PanY = 0F;
			this.canvas1.ShadowThickness = 16;
			this.canvas1.Size = new System.Drawing.Size(703, 361);
			this.canvas1.TabIndex = 0;
			this.canvas1.Zoom = 1F;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(757, 428);
			this.Controls.Add(this.canvas1);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.ResumeLayout(false);

		}

		#endregion

		private GDIHelper.Canvas canvas1;
	}
}