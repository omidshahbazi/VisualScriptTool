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

			SerializerCompiler compiler = new SerializerCompiler();

			File.WriteAllText(Application.StartupPath + "/../Test/Test_Serializer.cs", compiler.Compile(typeof(Test)));
			File.WriteAllText(Application.StartupPath + "/../Test/Test1_Serializer.cs", compiler.Compile(typeof(Test1)));

			//Serializer ser = new Serializer();
		}
	}
}