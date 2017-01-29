// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Text;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Reflection;

namespace VisualScriptTool.CodeGeneration.Language
{
	abstract class StatementCodeGenerator
	{
		private static StatementCodeGenerator[] codeGenerators = null;

		public abstract Type[] StatementTypes
		{
			get;
		}

		public abstract void Generate(StringBuilder Builder, Statement Statement);

		public static StatementCodeGenerator Get(Type StatementType)
		{
			if (codeGenerators == null)
			{
				Type[] types = TypeUtils.GetDrievedTypesOf<StatementCodeGenerator>();
				StatementCodeGeneratorList generators = new StatementCodeGeneratorList();

				for (int i = 0; i < types.Length; ++i)
				{
					if (types[i].IsAbstract)
						continue;

					generators.Add((StatementCodeGenerator)Activator.CreateInstance(types[i]));
				}

				codeGenerators = generators.ToArray();
			}

			for (int i = 0; i < codeGenerators.Length; ++i)
				if (Array.IndexOf(codeGenerators[i].StatementTypes, StatementType) != -1)
					return codeGenerators[i];

			return null;
		}
	}

	class StatementCodeGeneratorList : List<StatementCodeGenerator>
	{ }
}
