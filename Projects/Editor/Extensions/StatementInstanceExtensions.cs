// Copyright 2016-2017 ?????????????. All Rights Reserved.

using System.Collections.Generic;
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor.Extensions
{
	public static class StatementInstanceExtensions
	{
		public static T[] ToStatements<T>(this StatementInstance[] a) where T : Statement
		{
			List<T> statements = new List<T>();

			for (int i = 0; i < a.Length; ++i)
			{
				if (a[i].Statement is T statement)
				{
					statements.Add(statement);
				}
			}

			return statements.ToArray();
		}
	}
}