// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Windows.Forms;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());


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
		}
	}
}
