// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.IO;
using System.Windows.Forms;
using VisualScriptTool.Editor.Serializers;
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

			Type[] types = new Type[] { typeof(StatementInstance), typeof(IfStatementInstance), typeof(ForStatementInstance), typeof(VariableStatementInstance), typeof(System.Drawing.PointF), typeof(System.Drawing.SizeF) };

			SerializerCompiler compiler = new SerializerCompiler();
			compiler.Strategy = new SystemCompilerStrategy();

			for (int i = 0; i < types.Length; ++i)
				File.WriteAllText(Application.StartupPath + "/../Editor/Serializers/" + types[i].Name + "_Serializer.cs", compiler.Compile(types[i]));
		}
	}
}