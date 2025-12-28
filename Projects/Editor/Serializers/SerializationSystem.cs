// Copyright 2016-2017 ?????????????. All Rights Reserved.using System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Language;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Reflection;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor.Serializers
{
	static class SerializationSystem
	{
		public static void Initialize()
		{
			Type[] types = TypeUtils.GetDrievedTypesOf<Serializer>();
			for (int i = 0; i < types.Length; ++i)
			{
				if (types[i].IsAbstract)
					continue;

				Creator.AddSerializer((Serializer)Activator.CreateInstance(types[i]));
			}

#if DEBUG
			Compile();
#endif
		}

		private static void Compile()
		{
			ISerializationCompileStrategy defaultCompilerStrategy = new DefaultCompileStrategy();
			ISerializationCompileStrategy systemCompilerStrategy = new SystemCompilerStrategy();
			ISerializationCompileStrategy internalValueTypeCompilerStrategy = new InternalValueTypeCompilerStrategy();

			Dictionary<Type, ISerializationCompileStrategy> typeStrategyMap = new Dictionary<Type, ISerializationCompileStrategy>();

			List<Type> types = new List<Type>();

			types.Add(typeof(Statement));
			types.AddRange(TypeUtils.GetDrievedTypesOf<Statement>(true));

			types.Add(typeof(StatementInstance));
			types.AddRange(TypeUtils.GetDrievedTypesOf<StatementInstance>());

			for (int i = 0; i < types.Count; ++i)
				typeStrategyMap[types[i]] = defaultCompilerStrategy;

			typeStrategyMap[typeof(PointF)] = systemCompilerStrategy;
			typeStrategyMap[typeof(SizeF)] = systemCompilerStrategy;
			typeStrategyMap[typeof(AnyDataType)] = internalValueTypeCompilerStrategy;


			SerializerCompiler compiler = new SerializerCompiler();

			Dictionary<Type, ISerializationCompileStrategy>.Enumerator it = typeStrategyMap.GetEnumerator();
			while (it.MoveNext())
			{
				compiler.Strategy = it.Current.Value;

				File.WriteAllText(Application.StartupPath + "/../Editor/Serializers/" + it.Current.Key.Name + "_Serializer.cs", compiler.Compile(it.Current.Key, "VisualScriptTool.Editor.Serializers"));
			}
		}
	}
}
