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
			Application.Run(new MainForm());

			//SerializerCompiler compiler = new SerializerCompiler();
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

			SerializerCompiler compiler = new SerializerCompiler();
			File.WriteAllText(Application.StartupPath + "/../Editor/StatementInstance_Serializer.cs", compiler.Compile(typeof(StatementInstance)));
			File.WriteAllText(Application.StartupPath + "/../Editor/IfStatementInstance_Serializer.cs", compiler.Compile(typeof(IfStatementInstance)));
			File.WriteAllText(Application.StartupPath + "/../Editor/ForStatementInstance_Serializer.cs", compiler.Compile(typeof(ForStatementInstance)));
			File.WriteAllText(Application.StartupPath + "/../Editor/VariableStatementInstance_Serializer.cs", compiler.Compile(typeof(VariableStatementInstance)));
		}
	}
}