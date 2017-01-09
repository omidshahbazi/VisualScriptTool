// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Windows.Forms;
using VisualScriptTool.Editor.Serializers;

namespace VisualScriptTool.Editor
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Compiler.Compile();

			SerializationSystem.Initialize();

			Application.Run(new MainForm());
		}
	}
}