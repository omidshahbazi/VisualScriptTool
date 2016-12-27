// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Windows.Forms;

namespace VisualScriptTool.Editor
{
	static class Program
	{
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

			[Serialization.Serializable(2)]
			public string[] Name = new string[] { "omid", "ali" };

			[Serialization.Serializable(3)]
			public System.Drawing.Point[] Points = new System.Drawing.Point[] { new System.Drawing.Point(10, 100), new System.Drawing.Point(1000, 230) };

			[Serialization.Serializable(4)]
			public Random child = new Random();

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


			Serialization.Serializer ser = new Serialization.Serializer();
			System.IO.File.WriteAllText(path, ser.Serialize(new Test()));

			ser = new Serialization.Serializer();
			Test test = (Test)ser.Deserialize(path);
		}
	}
}
