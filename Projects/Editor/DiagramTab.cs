// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VisualScriptTool.CodeGeneration;
using VisualScriptTool.Editor;
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

		public bool IsNew
		{
			get;
			private set;
		}

		public string FilePath
		{
			get;
			private set;
		}

		public StatementInstanceList Statements
		{
			get { return canvas.Statements; }
		}

		public DiagramTab()
		{
			canvas = new StatementCanvas();
			canvas.BackColor = Color.DimGray;
			canvas.Dock = DockStyle.Fill;
			canvas.MinimumZoom = 0.5F;
			canvas.MaximumZoom = 1.0F;
			canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			Controls.Add(canvas);
		}

		public void New(string Name)
		{
			this.Name = Name;
			IsNew = true;
			IsDirty = true;
		}

		public void Load(string FilePath)
		{
			IsNew = false;
			this.FilePath = FilePath;
			Name = Path.GetFileNameWithoutExtension(FilePath);

			Serializer serializer = Creator.GetSerializer(Statements.GetType());

			ISerializeArray dataArray = Creator.Create<ISerializeArray>(File.ReadAllText(FilePath));

			Statements.AddRange(serializer.Deserialize<StatementInstance[]>(dataArray));

			for (int i = 0; i < Statements.Count; ++i)
				Statements[i].ResolveSlotConnections(canvas);

			canvas.Refresh();

			UpdateTabText();

			GenerateCode();
		}

		private void GenerateCode()
		{
			CSharpCodeGenerator codeGenerator = new CSharpCodeGenerator();

			for (int i = 0; i < Statements.Count; ++i)
				if (Statements[i] is ExecuterStatementInstance)
					File.WriteAllText(Application.StartupPath + "/1.cs", codeGenerator.Generate((VisualScriptTool.Language.Statements.ExecuterStatement)Statements[i].Statement)[0]);
		}

		public bool Save()
		{
			if (IsNew)
				return false;

			IsNew = false;
			IsDirty = false;

			Serializer serializer = Creator.GetSerializer(Statements.GetType());

			ISerializeArray dataArray = Creator.Create<ISerializeArray>();

			serializer.Serialize(dataArray, Statements);

			File.WriteAllText(FilePath, dataArray.Content);

			return true;
		}

		public void Save(string FilePath)
		{
			this.FilePath = FilePath;
			Name = Path.GetFileNameWithoutExtension(FilePath);
			IsNew = false;
			IsDirty = false;

			Serializer serializer = Creator.GetSerializer(Statements.GetType());

			ISerializeArray dataArray = Creator.Create<ISerializeArray>();

			serializer.Serialize(dataArray, Statements);

			File.WriteAllText(FilePath, dataArray.Content);
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