// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration.Variables;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			DiagramTab diagramTab = new DiagramTab("test");


			BooleanVariable boolVariable = new BooleanVariable();
			boolVariable.Name = "flag";
			boolVariable.Value = true;


			IfStatement ifStatement = new IfStatement();
			ifStatement.Condition = boolVariable;

			ForStatement forStatement = new ForStatement();
			ifStatement.Statement = forStatement;
			ifStatement.CompleteStatement = forStatement;

			IntegerVariable intVariable = new IntegerVariable();
			intVariable.Name = "myVariable";
			intVariable.Value = 0;

			IntegerVariable intVariable1 = new IntegerVariable();
			intVariable1.Name = "secondVar";
			intVariable1.Value = 0;

			IfStatement ifStatement1 = new IfStatement();
			ifStatement1.Condition = boolVariable;

			forStatement.MinimumValue = intVariable;
			forStatement.MaximumValue = intVariable1;
			forStatement.Statement = ifStatement1;

			diagramTab.Statements.Add(new IfStatementInstance(ifStatement));
			diagramTab.Statements.Add(new IfStatementInstance(ifStatement1));
			diagramTab.Statements.Add(new ForStatementInstance(forStatement));

			diagramTab.Statements.Add(new VariableStatementInstance(intVariable1));
			diagramTab.Statements.Add(new VariableStatementInstance(intVariable));
			diagramTab.Statements.Add(new VariableStatementInstance(boolVariable));

			Serialization.Creator.AddSerializer(new PointF_Serializer());
			Serialization.Creator.AddSerializer(new SizeF_Serializer());
			Serialization.Creator.AddSerializer(new StatementInstance_Serializer());
			Serialization.Creator.AddSerializer(new IfStatementInstance_Serializer());
			Serialization.Creator.AddSerializer(new ForStatementInstance_Serializer());
			Serialization.Creator.AddSerializer(new VariableStatementInstance_Serializer());


			ISerializeArray dataArray = Serialization.Creator.Create<ISerializeArray>(System.IO.File.ReadAllText(Application.StartupPath + "/1.json"));

			Serialization.Creator.GetSerializer(diagramTab.Statements.GetType()).Deserialize(dataArray, diagramTab.Statements);



			//ISerializeArray dataArray = Serialization.Creator.Create<ISerializeArray>();

			//Serialization.Creator.GetSerializer(diagramTab.Statements.GetType()).Serialize(dataArray, diagramTab.Statements);

			//System.IO.File.WriteAllText(Application.StartupPath + "/1.json", dataArray.Content);

			tabControl1.TabPages.Add(diagramTab);


		}
	}
}