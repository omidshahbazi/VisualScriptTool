// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Windows.Forms;
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor
{
    public partial class AddVariableForm : Form
    {
        private enum Types
        {
            Boolean,
            Integer,
            Float,
            String
        }

        private StatementCanvas canvas = null;

        public AddVariableForm(StatementCanvas Canvas)
        {
            InitializeComponent();

            canvas = Canvas;

            TypeComboBox.Items.Add(Types.Boolean);
            TypeComboBox.Items.Add(Types.Integer);
            TypeComboBox.Items.Add(Types.Float);
            TypeComboBox.Items.Add(Types.String);

            TypeComboBox.SelectedItem = Types.Boolean;
        }

        private void AddButton_Click(object sender, System.EventArgs e)
        {
            VariableStatement variable = null;

            switch ((Types)TypeComboBox.SelectedItem)
            {
                case Types.Boolean:
                    variable = new BooleanVariable();
                    break;
                case Types.Integer:
                    variable = new IntegerVariable();
                    break;
                case Types.Float:
                    variable = new FloatVariable();
                    break;
                case Types.String:
                    variable = new StringVariable();
                    break;
            }

            variable.Name = NameTextBox.Text;

            canvas.AddStatementInstance(new VariableStatementInstance(variable));

            Close();
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
