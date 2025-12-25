// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Windows.Forms;
using VisualScriptTool.Language;
using VisualScriptTool.Language.Statements.Declaration;
using VisualScriptTool.Reflection;

namespace VisualScriptTool.Editor
{
	public partial class AddVariableForm : Form
	{
		private StatementCanvas canvas = null;

		public AddVariableForm(StatementCanvas Canvas)
		{
			InitializeComponent();

			canvas = Canvas;

			for (int i = 0; i < AnyDataType.AVAILABLE_TYPES.Length; ++i)
				TypeComboBox.Items.Add(AnyDataType.AVAILABLE_TYPES[i].Name);
			TypeComboBox.SelectedIndex = 0;
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			VariableStatement variable = new VariableStatement();

			variable.Value = new AnyDataType(Type.GetType("System." + TypeComboBox.SelectedItem.ToString()).GetDefaultValue());

			variable.Name = NameTextBox.Text;

			canvas.AddVariableStatement(variable);

			Close();
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
