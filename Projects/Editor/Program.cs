// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
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

			ICompileStrategy defaultCompilerStrategy = new DefaultCompileStrategy();
			ICompileStrategy systemCompilerStrategy = new SystemCompilerStrategy();

			Dictionary<Type, ICompileStrategy> types = new Dictionary<Type, ICompileStrategy>();
			types[typeof(StatementInstance)] = defaultCompilerStrategy;
			//types[typeof(IfStatementInstance)] = defaultCompilerStrategy;
			//types[typeof(ForStatementInstance)] = defaultCompilerStrategy;
			//types[typeof(VariableStatementInstance)] = defaultCompilerStrategy;
			//types[typeof(System.Drawing.PointF)] = systemCompilerStrategy;
			//types[typeof(System.Drawing.SizeF)] = systemCompilerStrategy;


			SerializerCompiler compiler = new SerializerCompiler();

			Dictionary<Type, ICompileStrategy>.Enumerator it = types.GetEnumerator();
			while (it.MoveNext())
			{
				compiler.Strategy = it.Current.Value;

				File.WriteAllText(Application.StartupPath + "/../Editor/Serializers/" + it.Current.Key.Name + "_Serializer.cs", compiler.Compile(it.Current.Key));
			}
		}
	}
}