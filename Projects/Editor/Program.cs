// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Windows.Forms;

namespace VisualScriptTool.Editor
{
	static class Program
	{
		class Test1
		{
			//[Serialization.Serializable]
			//private int i;

			//[Serialization.Serializable]
			//public int j;

			//[Serialization.Serializable]
			//private float Rate
			//{
			//	get;
			//	set;
			//}

			[Serialization.Serializable(2)]
			public string[] Name = new string[] { "omid", "ali" };

			[Serialization.Serializable(4)]
			public Random child = new Random();
		}
		class Test
		{
			//[Serialization.Serializable]
			//private int i;

			//[Serialization.Serializable]
			//public int j;

			//[Serialization.Serializable]
			//private float Rate
			//{
			//	get;
			//	set;
			//}

			[Serialization.Serializable(1)]
			public int Index = 3;

			[Serialization.Serializable(0)]
			public bool Flag = true;


			[Serialization.Serializable(6)]
			public string[] Name2 = null;

			[Serialization.Serializable(3)]
			public Random[] Points = new Random[] { new Random(), null, new Random() };

			[Serialization.Serializable(4)]
			public Test1 child = new Test1();

			[Serialization.Serializable(7)]
			public Test1 child1 = null;

			public Test()
			{
				child1 = child;
			}

			[Serialization.Serializable(5)]
			public System.Drawing.Point Poisition
			{
				get;
				set;
			}
		}

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new MainForm());

			string path = Application.StartupPath + "/1.json";

			//
			// Arrays
			//
			//System.IO.File.WriteAllText(path, ser.Serialize(new Test[] { new Test(), new Editor.Program.Test() }).Content);


			Serialization.Serializer ser = new Serialization.Serializer();
			System.IO.File.WriteAllText(path, ser.Serialize(new Test()).Content);

			ser = new Serialization.Serializer();
			Test test = new Test();
			ser.Deserialize(JSONSerializeObject.Deserialize(System.IO.File.ReadAllText(Path)), test);
		}
	}
}