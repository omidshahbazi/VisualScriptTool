// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Editor
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
}