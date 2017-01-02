// Copyright 2016-2017 ?????????????. All Rights Reserved.

using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
{
	class Test1
	{
		public string[] Name;

		public System.Random child;

		public Test1()
		{
			Name = new string[] { "omid", "ali" };
		}
	}

	class Test
	{
		[SerializableElement(0, 10123)]
		public int Index;

		[SerializableElement(1, true)]
		public bool Flag;

		[SerializableElement(2)]
		public string[] Name2;

		[SerializableElement(3)]
		public System.Random[] Points;

		[SerializableElement(6)]
		public Test child;

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
			Points = null;
			Flag = true;
		}
	}
}