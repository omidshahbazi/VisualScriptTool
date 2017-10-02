// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Windows.Forms;

namespace VisualScriptTool.Editor
{
	public partial class MainForm : Form
	{
		private const string FILE_EXTENSIONS = "Graph Files|*.json";

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

			AddTab().Load(Application.StartupPath + "/New Diagram.json");
		}

		private void NewMenuItem_Click(object sender, System.EventArgs e)
		{
			AddTab().New("New Diagram");
		}

		private void LoadMenuItem_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			openFileDialog.InitialDirectory = Application.StartupPath;
			openFileDialog.Filter = FILE_EXTENSIONS;

			if (openFileDialog.ShowDialog() == DialogResult.Cancel)
				return;

			for (int i = 0; i < openFileDialog.FileNames.Length; ++i)
				AddTab().Load(openFileDialog.FileNames[i]);
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

		private void GenerateCSharpCode_Click(object sender, System.EventArgs e)
		{
			if (CurrentTab == null)
				return;

			CurrentTab.GenerateCode();
		}

		private DiagramTab AddTab()
		{
			DiagramTab diagramTab = new DiagramTab();
			TabControl.TabPages.Add(diagramTab);
			TabControl.SelectedTab = diagramTab;
			return diagramTab;
		}

		private void SaveDiagramTab(DiagramTab Tab)
		{
			if (Tab.IsNew)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.InitialDirectory = Application.StartupPath;
				saveFileDialog.Filter = FILE_EXTENSIONS;

				if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
					return;

				Tab.Save(saveFileDialog.FileName);
			}
			else
				Tab.Save();
		}
	}
}