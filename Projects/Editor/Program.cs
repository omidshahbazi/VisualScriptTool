// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Windows.Forms;

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
		}
	}
}
