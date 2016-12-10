// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
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

			diagramTab.Statements.Add(new StatementInstance(ifStatement, new PointF(350, 220)));
			diagramTab.Statements.Add(new StatementInstance(ifStatement1, new PointF(750, 520)));
			diagramTab.Statements.Add(new StatementInstance(forStatement, new PointF(650, 220)));

			diagramTab.Statements.Add(new StatementInstance(intVariable1, new PointF(150, 220)));
			diagramTab.Statements.Add(new StatementInstance(intVariable, new PointF(50, 20)));
			diagramTab.Statements.Add(new StatementInstance(boolVariable, new PointF(150, 120)));

			tabControl1.TabPages.Add(diagramTab);
		}
	}
}