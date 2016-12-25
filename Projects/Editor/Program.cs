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

			[Serialization.Serializable]
			public int Index = 3;

			[Serialization.Serializable]
			public string[] Name = new string[] { "omid", "ali" };

			[Serialization.Serializable]
			public Random child = new Random();

			[Serialization.Serializable]
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

			Serialization.Serializer ser = new Serialization.Serializer();
			System.IO.File.WriteAllText("D:/1.json", ser.Serialize(new Test()));
		}
	}
}
