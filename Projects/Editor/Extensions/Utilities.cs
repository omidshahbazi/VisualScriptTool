// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Windows.Forms;

namespace VisualScriptTool.Editor.Extensions
{
	public static class Utilities
    {
		public static bool ShowConfirmation(string Title, string Text)
        {
            return MessageBox.Show(Text, Title, MessageBoxButtons.YesNo) == DialogResult.Yes;
        }
	}
}