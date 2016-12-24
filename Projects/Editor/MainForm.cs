﻿// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor
{
	public partial class MainForm : Form
	{
		class Test
		{
			//[Serialization.Serializable]
			//private int i;

			//[Serialization.Serializable]
			//public int j;

			//[Serialization.Serializable]
			//private float Rate
			//{
			//	get;
			//	set;
			//}

			[Serialization.Serializable]
			public string Name = "omid";

			[Serialization.Serializable]
			public Point Poisition
			{
				get;
				set;
			}
		}

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

			diagramTab.Statements.Add(new IfStatementInstance(ifStatement, new PointF(350, 220)));
			diagramTab.Statements.Add(new IfStatementInstance(ifStatement1, new PointF(750, 520)));
			diagramTab.Statements.Add(new ForStatementInstance(forStatement, new PointF(650, 220)));

			diagramTab.Statements.Add(new VariableStatementInstance(intVariable1, new PointF(150, 220)));
			diagramTab.Statements.Add(new VariableStatementInstance(intVariable, new PointF(50, 20)));
			diagramTab.Statements.Add(new VariableStatementInstance(boolVariable, new PointF(150, 120)));

			Serialization.Serializer ser = new Serialization.Serializer();
			System.IO.File.WriteAllText("E:/1.txt", ser.Serialize(new Test()));

			tabControl1.TabPages.Add(diagramTab);
		}
	}
}