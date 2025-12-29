// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Declaration
{
	public class FunctionStatement : UserDefinedStatement
	{
		private List<Statement> parameters = new List<Statement>();
		private List<AnyDataType> parameterDefaultValues = new List<AnyDataType>();

		[SerializableElement(2)]
		public override string Name
		{
			get;
			set;
		}

		[SerializableElement(3)]
		public Statement[] Parameters
		{
			get { return parameters.ToArray(); }
			set { parameters = new List<Statement>(value); }
		}

		[SerializableElement(4)]
		public AnyDataType[] ParametersDefaultValue
		{
			get { return parameterDefaultValues.ToArray(); }
			set { parameterDefaultValues = new List<AnyDataType>(value); }
		}

		public bool HasReturnValue
		{
			get;
			private set;
		}

		public void AddParameter(Statement Statement)
		{
			parameters.Add(Statement);
			parameterDefaultValues.Add(new AnyDataType());
		}

		public FunctionStatement()
		{
		}
	}
}
