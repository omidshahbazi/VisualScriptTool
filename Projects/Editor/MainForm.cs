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
		}
	}
}