// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Serialization;

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
			canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			Button button = new Button();
			button.Click += Button_Click;
			Controls.Add(button);

			Controls.Add(canvas);

			this.Name = Name;
			Text = Name;

			Load();
		}

		private void Button_Click(object sender, System.EventArgs e)
		{
			Save();
		}

		public void New()
		{

			IsDirty = true;
		}

		public void Load()
		{
			Serializer serializer = Serialization.Creator.GetSerializer(Statements.GetType());

			ISerializeArray dataArray = Serialization.Creator.Create<ISerializeArray>(System.IO.File.ReadAllText(Application.StartupPath + "/1.json"));

			Statements.AddRange(serializer.Deserialize<StatementInstance[]>(dataArray));
		}

		public void Save()
		{
			Serializer serializer = Serialization.Creator.GetSerializer(Statements.GetType());

			ISerializeArray dataArray = Serialization.Creator.Create<ISerializeArray>();

			serializer.Serialize(dataArray, Statements);

			System.IO.File.WriteAllText(Application.StartupPath + "/1.json", dataArray.Content);
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
			SuspendLayout();
			ResumeLayout(false);
		}
	}
}