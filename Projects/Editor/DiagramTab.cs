// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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
		private const string VARIABLE_STATEMENTS_ARRAY_NAME = "VariableStatements";
		private const string STATEMENT_INSTANCES_ARRAY_NAME = "StatementInstnaces";

		private IContainer components;
		private ContextMenuStrip listMenu;
		private ToolStripMenuItem addVariableButton;
		private ListBox list = null;
		private StatementCanvas canvas = null;
		private TableLayoutPanel tableLayoutPanel = null;
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

		public VariableStatement[] VariableStatements
		{
			get { return canvas.VariableStatements; }
		}

		public StatementInstance[] StatementInstances
		{
			get { return canvas.StatementInstances; }
		}

		public DiagramTab()
		{
			InitializeComponent();

			canvas.BindClassFunctions(typeof(Math));

			canvas.OnVariableStatementsChanged += OnVariableStatementsChanged;
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

			ISerializeArray statementInstanceDataArray = obj.Get<ISerializeArray>(STATEMENT_INSTANCES_ARRAY_NAME);
			StatementInstance[] statementInstance = Creator.GetSerializer(StatementInstances.GetType()).Deserialize<StatementInstance[]>(statementInstanceDataArray);

			canvas.AddStatementInstance(statementInstance);

			ISerializeArray variableStatementDataArray = obj.Get<ISerializeArray>(VARIABLE_STATEMENTS_ARRAY_NAME);
			VariableStatement[] variableStatements = Creator.GetSerializer(VariableStatements.GetType()).Deserialize<VariableStatement[]>(variableStatementDataArray);

			canvas.AddVariableStatement(variableStatements);

			for (int i = 0; i < StatementInstances.Length; ++i)
				StatementInstances[i].ResolveSlotConnections(canvas);

			canvas.Refresh();

			canvas.ResetView();

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
			Statement[] statements = StatementInstances.ToStatements<Statement>();

			CSharpCodeGenerator codeGenerator = new CSharpCodeGenerator();

			//TODO: What is the first element of the array?
			File.WriteAllText(Application.StartupPath + "/" + Name + ".cs", codeGenerator.Generate(statements)[0]);
		}

		private void SaveInternal(string FilePath)
		{
			IsNew = false;
			IsDirty = false;

			ISerializeObject obj = Creator.Create<ISerializeObject>();

			ISerializeArray variableStatementDataArray = obj.AddArray(VARIABLE_STATEMENTS_ARRAY_NAME);
			Creator.GetSerializer(VariableStatements.GetType()).Serialize(variableStatementDataArray, VariableStatements);

			ISerializeArray statementInstanceDataArray = obj.AddArray(STATEMENT_INSTANCES_ARRAY_NAME);
			Creator.GetSerializer(StatementInstances.GetType()).Serialize(statementInstanceDataArray, StatementInstances);

			File.WriteAllText(FilePath, obj.Content);
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
			addVariableButton = new ToolStripMenuItem();
			list = new ListBox();
			canvas = new StatementCanvas();
			tableLayoutPanel = new TableLayoutPanel();

			listMenu.SuspendLayout();
			list.SuspendLayout();
			canvas.SuspendLayout();
			SuspendLayout();
			// 
			// listMenu
			// 
			listMenu.Items.AddRange(new ToolStripItem[] {
			addVariableButton});
			listMenu.Name = "listMenu";
			listMenu.Size = new Size(141, 26);
			// 
			// AddVariableButton
			// 
			addVariableButton.Name = "AddVariableButton";
			addVariableButton.Size = new Size(140, 22);
			addVariableButton.Text = "Add Variable";
			addVariableButton.Click += AddVariableButton_Click;
			// 
			// list
			// 
			list.ContextMenuStrip = listMenu;
			list.Dock = DockStyle.Fill;
			list.IntegralHeight = false;
			list.Location = new Point(0, 0);
			list.Name = "list";
			list.Size = new Size(300, 100);
			list.TabIndex = 1;
			list.KeyUp += List_KeyUp;
			list.MouseClick += List_MouseClick;
			list.MouseMove += List_MouseMove;
			// 
			// canvas
			// 
			canvas.AllowDrop = true;
			canvas.BackColor = Color.DimGray;
			canvas.CompositingQuality = CompositingQuality.Default;
			canvas.Dock = DockStyle.Fill;
			canvas.DrawAxis = true;
			canvas.GraphicsUnit = GraphicsUnit.Pixel;
			canvas.InterpolationMode = InterpolationMode.Default;
			canvas.Location = new Point(0, 0);
			canvas.MaximumZoom = 1F;
			canvas.MinimumZoom = 0.5F;
			canvas.Name = "canvas";
			canvas.Origin = new Point(0, 0);
			canvas.Pan = ((PointF)(resources.GetObject("canvas.Pan")));
			canvas.PixelOffsetMode = PixelOffsetMode.Default;
			canvas.Size = new Size(200, 100);
			canvas.SmoothingMode = SmoothingMode.HighQuality;
			canvas.TabIndex = 2;
			canvas.TextContrast = 0;
			canvas.TextRenderingHint = TextRenderingHint.SystemDefault;
			canvas.Zoom = 1F;
			// 
			// tableLayoutPanel
			// 
			tableLayoutPanel.ColumnCount = 2;
			tableLayoutPanel.Dock = DockStyle.Fill;
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
			tableLayoutPanel.Controls.Add(list, 0, 0);
			tableLayoutPanel.Controls.Add(canvas, 1, 0);
			tableLayoutPanel.Name = "tableLayoutPanel";
			tableLayoutPanel.RowCount = 1;
			tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel.TabIndex = 0;
			// 
			// DiagramTab
			// 
			Controls.Add(tableLayoutPanel);

			listMenu.ResumeLayout(false);
			list.ResumeLayout(false);
			canvas.ResumeLayout(false);
			ResumeLayout();
		}

		private void List_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (list.SelectedItem == null)
					return;

				if (!Utilities.ShowConfirmation("Warning", "Are you sure ?"))
					return;

				canvas.RemoveStatement((Statement)list.SelectedItem);
			}
		}

		private void OnVariableStatementsChanged()
		{
			list.Items.Clear();

			foreach (VariableStatement statement in VariableStatements)
				list.Items.Add(statement);
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