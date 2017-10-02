// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VisualScriptTool.CodeGeneration;
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Language.Statements.Declaration.Variables;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
{
	public class DiagramTab : TabPage
	{
		private ListBox list = null;
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

		public StatementInstance[] Statements
		{
			get { return canvas.Statements; }
		}

		public DiagramTab()
		{
			list = new ListBox();
			list.Size = new Size(300, 1);
			list.Dock = DockStyle.Left;
			list.IntegralHeight = false;
			list.MouseMove += List_MouseMove;

			canvas = new StatementCanvas();
			canvas.BackColor = Color.DimGray;
			canvas.Dock = DockStyle.Fill;
			canvas.MinimumZoom = 0.5F;
			canvas.MaximumZoom = 1.0F;
			canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			canvas.AllowDrop = true;

			Controls.Add(list);
			Controls.Add(canvas);
		}

		private void List_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left || list.SelectedItem == null)
				return;

			DragAndDropManager.SetData(list.SelectedItem);

			list.DoDragDrop(list.SelectedItem.ToString(), DragDropEffects.Copy);
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

			StatementInstance[] instance = serializer.Deserialize<StatementInstance[]>(dataArray);

			canvas.AddStatementInstance(instance);

			foreach (StatementInstance inst in instance)
				if (inst is VariableStatementInstance && !list.Items.Contains(inst.Statement))
					list.Items.Add(inst.Statement);

			for (int i = 0; i < Statements.Length; ++i)
				Statements[i].ResolveSlotConnections(canvas);

			canvas.Refresh();

			UpdateTabText();
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

		public void GenerateCode()
		{
			VisualScriptTool.Language.Statements.Statement[] statements = new VisualScriptTool.Language.Statements.Statement[Statements.Length];
			for (int i = 0; i < Statements.Length; ++i)
				statements[i] = Statements[i].Statement;

			CSharpCodeGenerator codeGenerator = new CSharpCodeGenerator();

			File.WriteAllText(Application.StartupPath + "/" + Name + ".cs", codeGenerator.Generate(statements)[0]);
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