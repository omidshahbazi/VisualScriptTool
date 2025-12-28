// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Declaration;

namespace VisualScriptTool.Language.Extensions
{
	public static class StatementExtensions
	{
		public static T Find<T>(this Statement[] A) where T : Statement
		{
			for (int i = 0; i < A.Length; i++)
			{
				if (!(A[i] is T))
					continue;

				return A[i].As<T>();
			}

			return null;
		}

		public static T[] FindAll<T>(this Statement[] A) where T : Statement
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

		public static T As<T>(this Statement A) where T : Statement
		{
			return A as T;
		}

		public static bool IsA<T>(this VariableStatement A)
		{
			return A.IsOneOf(typeof(T));
		}

		public static bool IsOneOf(this VariableStatement A, params Type[] Types)
		{
			for (int i = 0; i < Types.Length; i++)
				if ((A.Value.Value == null || A.Value.Type == Types[i]))
					return true;

			return false;
		}

		public static Type[] GetConstraintsOf(this Statement A, string PropertyName)
		{
			PropertyInfo property = A.GetType().GetProperty(PropertyName);

			if (property == null)
				return null;

			List<Type> constraints = new List<Type>();

			IEnumerator<ConstraintsAttribute> it = property.GetCustomAttributes<ConstraintsAttribute>(true).GetEnumerator();
			while (it.MoveNext())
				constraints.AddRange(it.Current.Constraints);

			return constraints.ToArray();
		}

		public static bool IsVariableOf<T>(this Statement A) where T : Statement
		{
			VariableStatement variableStatement = A.As<VariableStatement>();
			if (variableStatement == null)
			{
				return false;
			}

			return variableStatement.IsOneOf(typeof(T));
		}

		public static bool IsVariableOfOneOf(this Statement A, params Type[] Types)
		{
			VariableStatement variableStatement = A.As<VariableStatement>();
			if (variableStatement == null)
			{
				return false;
			}

			return variableStatement.IsOneOf(Types);
		}
	}
}