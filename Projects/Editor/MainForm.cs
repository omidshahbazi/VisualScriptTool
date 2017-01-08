// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Windows.Forms;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			DiagramTab diagramTab = new DiagramTab("test");
			tabControl1.TabPages.Add(diagramTab);


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
		}
	}
}