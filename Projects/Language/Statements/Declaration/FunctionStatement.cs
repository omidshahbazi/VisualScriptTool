// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Declaration
{
	public abstract class FunctionStatement : UserDefinedStatement
	{
		private Statement[] arguments = null;

		[SerializableElement(2)]
		public override string Name
		{
			get;
			set;
		}

		public Statement[] Parameters
		{
			get { return arguments; }
		}

		public string[] ParametersName
		{
			get;
			private set;
		}

		public object[] ParametersDefaultValue
		{
			get;
			private set;
		}

		public bool HasReturnValue
		{
			get;
			private set;
		}

		public FunctionStatement()
		{
		}
	}
}
