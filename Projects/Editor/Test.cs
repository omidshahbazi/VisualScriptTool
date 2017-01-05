// Copyright 2016-2017 ?????????????. All Rights Reserved.

using System.Collections.Generic;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
{
	struct Test1
	{
		[SerializableElement(1)]
		public string[] Name;

		[SerializableElement(3)]
		public System.Random child;

		[SerializableInstantiator]
		public Test1(int a)
		{
			Name = new string[] { "omid", "ali" };
			child = null;

		}
	}

	class Test
	{
		[SerializableInstantiator(1)]
		public static object Instantiate(System.Type Type, int i)
		{
			return new Test();
		}

		[SerializableElement(0, 10123)]
		public int Index;

		[SerializableElement(1, true)]
		public bool Flag;

		[SerializableElement(2)]
		public string[] Name2;

		[SerializableElement(7)]
		public List<string> Name3;

		[SerializableElement(9)]
		public System.Random[] Points1;

		[SerializableElement(3)]
		public List<System.Random> Points2;

		[SerializableElement(6)]
		public Test1 child;

		//public Test1 child1;

		public System.Drawing.Point Poisition
		{
			get;
			private set;
		}

		private Test(int a, bool b, string item)
		{
			Index = 3;
			//child1 = child;
			Points1 = null;
			Flag = true;
		}

		public Test(int a = 1)
		{
			Index = 3;
			child = new Test1();
			Points1 = null;
			Flag = true;
		}
	}
}