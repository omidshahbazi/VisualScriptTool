// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Reflection;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Control
{
	public class FunctionStatement : FlowStatement
	{
		private Statement[] parameters = null;

		[SerializableElement(0)]
		public override string Name
		{
			get;
			set;
		}

		public MethodInfo Method
		{
			get;
			private set;
		}

		public Statement[] Parameters
		{
			get { return parameters; }
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

		public FunctionStatement(MethodInfo Method)
		{
			this.Method = Method;

			Name = Method.Name;

			ParameterInfo[] parametersInfo = Method.GetParameters();

			parameters = new Statement[parametersInfo.Length];
			ParametersDefaultValue = new object[parametersInfo.Length];
			ParametersName = new string[parametersInfo.Length];

			for (int i = 0; i < parametersInfo.Length; ++i)
				ParametersName[i] = parametersInfo[i].Name;

			HasReturnValue = (Method.ReturnType != typeof(void));
		}
	}
}
