// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using VisualScriptTool.CodeGeneration;
using VisualScriptTool.Editor.Extensions;
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Declaration;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
{
	public class DiagramTab : TabPage
	{
		private const string USER_DEFINED_STATEMENTS_ARRAY_NAME = "UserDefinedStatements";
		private const string STATEMENT_INSTANCES_ARRAY_NAME = "StatementInstnaces";

		private ListBox list = null;
		private StatementCanvas canvas = null;
		private IContainer components;
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

			canvas.BindClassFunctions(typeof(Math));

			canvas.OnStatementChanged += OnStatementChanged;
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

			ISerializeObject obj = Creator.Create<ISerializeObject>(File.ReadAllText(FilePath));

			ISerializeArray userDefinedDataArray = obj.Get<ISerializeArray>(USER_DEFINED_STATEMENTS_ARRAY_NAME);
			UserDefinedStatement[] userDefinedStatements = Creator.GetSerializer(typeof(UserDefinedStatement[])).Deserialize<UserDefinedStatement[]>(userDefinedDataArray);

			ISerializeArray statementInstanceDataArray = obj.Get<ISerializeArray>(STATEMENT_INSTANCES_ARRAY_NAME);
			StatementInstance[] statementInstance = Creator.GetSerializer(Statements.GetType()).Deserialize<StatementInstance[]>(statementInstanceDataArray);

			canvas.AddStatementInstance(statementInstance);

			for (int i = 0; i < Statements.Length; ++i)
				Statements[i].ResolveSlotConnections(canvas);

			canvas.Refresh();

			UpdateTabText();
		}

		public bool Save()
		{
			if (IsNew)
				return false;

			SaveInternal(FilePath);

			return true;
		}

		public void Save(string FilePath)
		{
			this.FilePath = FilePath;
			Name = Path.GetFileNameWithoutExtension(FilePath);

			SaveInternal(FilePath);
		}

		public void GenerateCode()
		{
			Statement[] statements = Statements.ToStatements<Statement>();

			CSharpCodeGenerator codeGenerator = new CSharpCodeGenerator();

			//TODO: What is the first element of the array?
			File.WriteAllText(Application.StartupPath + "/" + Name + ".cs", codeGenerator.Generate(statements)[0]);
		}

		private void SaveInternal(string FilePath)
		{
			IsNew = false;
			IsDirty = false;

			ISerializeObject obj = Creator.Create<ISerializeObject>();

			UserDefinedStatement[] userDefinedStatements = Statements.ToStatements<UserDefinedStatement>();
			ISerializeArray userDefinedDataArray = obj.AddArray(USER_DEFINED_STATEMENTS_ARRAY_NAME);
			Creator.GetSerializer(userDefinedStatements.GetType()).Serialize(userDefinedDataArray, userDefinedStatements);

			ISerializeArray statementInstanceDataArray = obj.AddArray(STATEMENT_INSTANCES_ARRAY_NAME);
			Creator.GetSerializer(Statements.GetType()).Serialize(statementInstanceDataArray, Statements);

			File.WriteAllText(FilePath + ".json", obj.Content);
		}

		private void SomethingChanged(object sender, EventArgs e)
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
			components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(DiagramTab));
			listMenu = new ContextMenuStrip(components);
			AddVariableButton = new ToolStripMenuItem();
			list = new ListBox();
			canvas = new StatementCanvas();
			listMenu.SuspendLayout();
			SuspendLayout();
			// 
			// listMenu
			// 
			listMenu.Items.AddRange(new ToolStripItem[] {
			AddVariableButton});
			listMenu.Name = "listMenu";
			listMenu.Size = new System.Drawing.Size(141, 26);
			// 
			// AddVariableButton
			// 
			AddVariableButton.Name = "AddVariableButton";
			AddVariableButton.Size = new System.Drawing.Size(140, 22);
			AddVariableButton.Text = "Add Variable";
			AddVariableButton.Click += AddVariableButton_Click;
			// 
			// list
			// 
			list.ContextMenuStrip = listMenu;
			list.Dock = DockStyle.Left;
			list.IntegralHeight = false;
			list.Location = new System.Drawing.Point(0, 0);
			list.Name = "list";
			list.Size = new System.Drawing.Size(300, 100);
			list.TabIndex = 1;
			list.KeyUp += List_KeyUp;
			list.MouseClick += List_MouseClick;
			list.MouseMove += List_MouseMove;
			// 
			// canvas
			// 
			canvas.AllowDrop = true;
			canvas.BackColor = System.Drawing.Color.DimGray;
			canvas.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
			canvas.Dock = DockStyle.Fill;
			canvas.DrawAxis = false;
			canvas.GraphicsUnit = System.Drawing.GraphicsUnit.Pixel;
			canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
			canvas.Location = new System.Drawing.Point(0, 0);
			canvas.MaximumZoom = 1F;
			canvas.MinimumZoom = 0.5F;
			canvas.Name = "canvas";
			canvas.Origin = new System.Drawing.Point(0, 0);
			canvas.Pan = ((System.Drawing.PointF)(resources.GetObject("canvas.Pan")));
			canvas.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
			canvas.Size = new System.Drawing.Size(200, 100);
			canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			canvas.TabIndex = 2;
			canvas.TextContrast = 0;
			canvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
			canvas.Zoom = 1F;
			// 
			// DiagramTab
			// 
			Controls.Add(list);
			Controls.Add(canvas);
			listMenu.ResumeLayout(false);
			ResumeLayout(false);

		}

		private void List_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (list.SelectedItem == null)
					return;

				if (!Utilities.ShowConfirmation("Warning", "Are you sure ?"))
					return;

				canvas.RemoveStatementInstance(((IStatementInspector)canvas).GetInstance((Statement)list.SelectedItem));
			}
		}

		private void OnStatementChanged()
		{
			list.Items.Clear();

			foreach (StatementInstance inst in Statements)
				if (inst is VariableStatementInstance && !list.Items.Contains(inst.Statement))
					list.Items.Add(inst.Statement);
		}

		private void AddVariableButton_Click(object sender, EventArgs e)
		{
			AddVariableForm form = new AddVariableForm(canvas);
			form.ShowDialog();
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