// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
{
	static class Program
	{
		class Test1
		{
			[Serialization.Serializable(2)]
			public string[] Name;

			[Serialization.Serializable(4)]
			public Random child;

			public Test1()
			{
				Name = new string[] { "omid", "ali" };
				child = new Random();
			}
		}
		class Test
		{
			[Serialization.Serializable(1)]
			public int Index;

			[Serialization.Serializable(0)]
			public bool Flag;

			[Serialization.Serializable(6)]
			public string[] Name2;

			[Serialization.Serializable(3)]
			public Random[] Points;

			[Serialization.Serializable(4)]
			public Test1 child;

			[Serialization.Serializable(7)]
			public Test1 child1;

			[Serialization.Serializable(5)]
			public System.Drawing.Point Poisition
			{
				get;
				private set;
			}

			//public Test()
			//{
			//	Index = 3;
			//	child = new Test1();
			//	child1 = child;
			//	Points = new Random[] { new Random(), null, new Random() };
			//	Flag = true;
			//}
		}

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());

			string path = Application.StartupPath + "/1.json";

			//
			// Arrays
			//
			//System.IO.File.WriteAllText(path, ser.Serialize(new Test[] { new Test(), new Editor.Program.Test() }).Content);

			//Test test = new Test(1);

			Serializer ser = new Serializer();
			ser.ObjectFactories.Add(new SystemObjectFactory());
			ser.Strategies.Add(new SystemStrategy());


			Test test = new Test();
			Test[] testArray = new Test[] { test, null, test, new Test() };
			List<Test> testList = new List<Test> { test, null, new Test() };

			//System.IO.File.WriteAllText(path, ser.Serialize(testList).Content);

			//ser = new Serializer();
			//ser.Deserialize(Factory.Create(System.IO.File.ReadAllText(path)), test);
			//Test[] test1 = ser.Deserialize<Test[]>(Creator.Create(System.IO.File.ReadAllText(path)));
		}
	}
}