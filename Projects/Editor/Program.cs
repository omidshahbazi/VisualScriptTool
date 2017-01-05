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
			//compiler.Strategy = new SystemCompilerStrategy();

			//System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			//for (int i = 0; i < assemblies.Length; ++i)
			//	if (assemblies[i].FullName.Contains("System"))
			//	{
			//		Type[] types = assemblies[i].GetExportedTypes();

			//		for (int j = 0; j < types.Length; ++j)
			//		{
			//			if (types[j].IsSealed)
			//				continue;

			//			File.WriteAllText(Application.StartupPath + "/../Test/System.Drawing/" + types[j].Name + "_Serializer.cs", compiler.Compile(types[j]));
			//		}
			//	}

			compiler = new SerializerCompiler();
			File.WriteAllText(Application.StartupPath + "/../Test/Test_Serializer.cs", compiler.Compile(typeof(Test)));
			File.WriteAllText(Application.StartupPath + "/../Test/Test1_Serializer.cs", compiler.Compile(typeof(Test1)));

			//Serializer ser = new Serializer();
		}
	}
}