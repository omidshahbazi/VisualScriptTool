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
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip listMenu;
        private ToolStripMenuItem AddVariableButton;
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
            InitializeComponent();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramTab));
            this.listMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.list = new System.Windows.Forms.ListBox();
            this.canvas = new VisualScriptTool.Editor.StatementCanvas();
            this.AddVariableButton = new System.Windows.Forms.ToolStripMenuItem();
            this.listMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listMenu
            // 
            this.listMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddVariableButton});
            this.listMenu.Name = "listMenu";
            this.listMenu.Size = new System.Drawing.Size(141, 26);
            // 
            // list
            // 
            this.list.Dock = System.Windows.Forms.DockStyle.Left;
            this.list.IntegralHeight = false;
            this.list.Location = new System.Drawing.Point(0, 0);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(300, 100);
            this.list.TabIndex = 1;
            this.list.MouseUp += List_MouseClick;
            // 
            // canvas
            // 
            this.canvas.AllowDrop = true;
            this.canvas.BackColor = System.Drawing.Color.DimGray;
            this.canvas.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.GraphicsUnit = System.Drawing.GraphicsUnit.Pixel;
            this.canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.MaximumZoom = 1F;
            this.canvas.MinimumZoom = 0.5F;
            this.canvas.Name = "canvas";
            this.canvas.Origin = new System.Drawing.Point(0, 0);
            this.canvas.Pan = ((System.Drawing.PointF)(resources.GetObject("canvas.Pan")));
            this.canvas.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
            this.canvas.Size = new System.Drawing.Size(200, 100);
            this.canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.canvas.TabIndex = 2;
            this.canvas.TextContrast = 0;
            this.canvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.canvas.Zoom = 1F;
            // 
            // AddVariableButton
            // 
            this.AddVariableButton.Name = "AddVariableButton";
            this.AddVariableButton.Size = new System.Drawing.Size(140, 22);
            this.AddVariableButton.Text = "Add Variable";
            this.AddVariableButton.Click += AddVariableButton_Click;
            // 
            // DiagramTab
            // 
            this.Controls.Add(this.list);
            this.Controls.Add(this.canvas);
            this.listMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void AddVariableButton_Click(object sender, System.EventArgs e)
        {
        }

        private void List_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            listMenu.Show(list, e.Location);
        }

        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || list.SelectedItem == null)
                return;

            DragAndDropManager.SetData(list.SelectedItem);

            list.DoDragDrop(list.SelectedItem.ToString(), DragDropEffects.Copy);
        }
    }
}