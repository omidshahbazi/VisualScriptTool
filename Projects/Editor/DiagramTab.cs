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

			Button loadButton = new Button();
			loadButton.Click += LoadButton_Click;
			Controls.Add(loadButton);

			Button savebutton = new Button();
			savebutton.Click += SaveButton_Click;
			savebutton.Location = new Point(200, 0);
			Controls.Add(savebutton);

			Controls.Add(canvas);

			this.Name = Name;
			Text = Name;
		}

		private void LoadButton_Click(object sender, System.EventArgs e)
		{
			Load();

			canvas.Refresh();
		}

		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			Save();
		}

		public void New()
		{

			IsDirty = true;


		}

		public void Load()
		{
			//BooleanVariable boolVariable = new BooleanVariable();
			//boolVariable.Name = "flag";
			//boolVariable.Value = true;


			//IfStatement ifStatement = new IfStatement();
			//ifStatement.Condition = boolVariable;

			//ForStatement forStatement = new ForStatement();
			//ifStatement.Statement = forStatement;
			//ifStatement.CompleteStatement = forStatement;

			//IntegerVariable intVariable = new IntegerVariable();
			//intVariable.Name = "myVariable";
			//intVariable.Value = 0;

			//IntegerVariable intVariable1 = new IntegerVariable();
			//intVariable1.Name = "secondVar";
			//intVariable1.Value = 0;

			//IfStatement ifStatement1 = new IfStatement();
			//ifStatement1.Condition = boolVariable;

			//forStatement.MinimumValue = intVariable;
			//forStatement.MaximumValue = intVariable1;
			//forStatement.Statement = ifStatement1;

			//Statements.Add(new IfStatementInstance(ifStatement));
			//Statements.Add(new IfStatementInstance(ifStatement1));
			//Statements.Add(new ForStatementInstance(forStatement));

			//Statements.Add(new VariableStatementInstance(intVariable1));
			//Statements.Add(new VariableStatementInstance(intVariable));
			//Statements.Add(new VariableStatementInstance(boolVariable));


			Serializer serializer = Serialization.Creator.GetSerializer(Statements.GetType());

			ISerializeArray dataArray = Serialization.Creator.Create<ISerializeArray>(System.IO.File.ReadAllText(Application.StartupPath + "/1.json"));

			Statements.AddRange(serializer.Deserialize<StatementInstance[]>(dataArray));

			for (int i = 0; i < Statements.Count; ++i)
				Statements[i].ResolveSlotConnections(canvas);
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