// Copyright 2016-2017 ?????????????. All Rights Reserved.

using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor.Extensions
{
	public static class StatementInstanceExtensions
	{
		public static T Find<T>(this StatementInstance[] A) where T : StatementInstance
		{
			for (int i = 0; i < A.Length; i++)
			{
				if (!(A[i] is T))
					continue;

				return A[i].As<T>();
			}

			return null;
		}

		public static T[] FindAll<T>(this StatementInstance[] A) where T : StatementInstance
		{
			List<T> list = new List<T>();

			for (int i = 0; i < A.Length; i++)
			{
				if (!(A[i] is T))
					continue;

				list.Add(A[i].As<T>());
			}

			return list.ToArray();
		}

		public static T As<T>(this StatementInstance A) where T : StatementInstance
		{
			return A as T;
		}

		public static T[] ToStatements<T>(this StatementInstance[] A) where T : Statement
		{
			List<T> statements = new List<T>();

			for (int i = 0; i < A.Length; ++i)
			{
				if (A[i].Statement is T statement)
				{
					statements.Add(statement);
				}
			}

			return statements.ToArray();
		}

		public static RectangleF GetBounds(this StatementInstance[] A)
		{
			RectangleF bounds = RectangleF.Empty;

			for (int i = 0; i < A.Length; ++i)
				bounds = bounds.Extend(A[i].Bounds);

			return bounds;
		}
	}
}