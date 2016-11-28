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

			StatementDrawer.Initialize();

			DiagramTab diagramTab = new DiagramTab("test");


			BooleanVariable boolVariable = new BooleanVariable();
			boolVariable.Name = "flag";
			boolVariable.Value = true;


			IfStatement ifStatement = new IfStatement();
			ifStatement.ConditionValue = boolVariable;

			ForStatement forStatement = new ForStatement();
			ifStatement.Statements.Add(forStatement);

			IntegerVariable intVariable = new IntegerVariable();
			intVariable.Name = "i";
			intVariable.Value = 0;

			forStatement.InitializeStatement = intVariable;

			//diagramTab.Statements.Add(new StatementInstance(boolVariable, new PointF()));
			//diagramTab.Statements.Add(new StatementInstance(ifStatement, new PointF()));
			//diagramTab.Statements.Add(new StatementInstance(forStatement, new PointF()));
			diagramTab.Statements.Add(new StatementInstance(intVariable, new PointF()));

			tabControl1.TabPages.Add(diagramTab);
		}
	}
}