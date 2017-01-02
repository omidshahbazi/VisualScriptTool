// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.IO;
using System.Windows.Forms;
using VisualScriptTool.Serialization;


namespace VisualScriptTool.Editor
{
	static class Program
	{
		class Test1
		{
			public string[] Name;

			public Random child;

			public Test1()
			{
				Name = new string[] { "omid", "ali" };
				child = new Random();
			}
		}

		class Test
		{
			public int Index;

			public bool Flag;

			public string[] Name2;

			public Random[] Points;

			//public Test1 child;

			//public Test1 child1;

			public System.Drawing.Point Poisition
			{
				get;
				private set;
			}

			public Test()
			{
				Index = 3;
				//child = new Test1();
				//child1 = child;
				Points = new Random[] { new Random(), null, new Random() };
				Flag = true;
			}
		}

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new MainForm());

			string path = Application.StartupPath + "/1.cs";

			SchemaCompiler compiler = new SchemaCompiler();

			File.WriteAllText(path, compiler.Compile(typeof(Test)));

			//Serializer ser = new Serializer();
		}
	}
}