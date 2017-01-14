// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Windows.Forms;

namespace VisualScriptTool.Editor
{
	public partial class MainForm : Form
	{
		private DiagramTab CurrentTab
		{
			get
			{
				if (TabControl.SelectedTab == null)
					return null;

				return (DiagramTab)TabControl.SelectedTab;
			}
		}

		public MainForm()
		{
			InitializeComponent();
		}

		private void NewMenuItem_Click(object sender, System.EventArgs e)
		{
			DiagramTab diagramTab = new DiagramTab("New Diagram");
			diagramTab.New();
			TabControl.TabPages.Add(diagramTab);
		}

		private void LoadMenuItem_Click(object sender, System.EventArgs e)
		{
			DiagramTab diagramTab = new DiagramTab("New Diagram");
			diagramTab.Load(Application.StartupPath + "/1.json");
			TabControl.TabPages.Add(diagramTab);
		}

		private void SaveMenuItem_Click(object sender, System.EventArgs e)
		{
			if (CurrentTab == null)
				return;

			SaveDiagramTab(CurrentTab);
		}

		private void SaveAllMenuItem_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < TabControl.TabCount; ++i)
				SaveDiagramTab((DiagramTab)TabControl.TabPages[i]);
		}

		private void SaveDiagramTab(DiagramTab Tab)
		{
			Tab.Save(Application.StartupPath + "/1.json");
		}
	}
}