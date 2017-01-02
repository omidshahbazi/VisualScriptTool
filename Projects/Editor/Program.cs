// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.IO;
using System.Windows.Forms;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new MainForm());

			string path = Application.StartupPath + "/../Test/Test_Serializer.cs";

			SchemaCompiler compiler = new SchemaCompiler();

			File.WriteAllText(path, compiler.Compile(typeof(Test)));

			//Serializer ser = new Serializer();
		}
	}
}