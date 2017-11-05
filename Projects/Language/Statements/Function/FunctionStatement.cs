// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Reflection;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Control
{
	public class FunctionStatement : FlowStatement
	{
		private MethodInfo method = null;
		private Statement[] parameters = null;

		[SerializableElement(0)]
		public override string Name
		{
			get;
			set;
		}

		public string[] ParametersName
		{
			get;
			private set;
		}

		public Statement[] Parameters
		{
			get { return parameters; }
		}

		public bool HasReturnValue
		{
			get;
			private set;
		}

		public FunctionStatement(MethodInfo Method)
		{
			method = Method;

			Name = method.Name;

			ParameterInfo[] parametersInfo = method.GetParameters();
			parameters = new Statement[parametersInfo.Length];
			ParametersName = new string[parametersInfo.Length];
			for (int i = 0; i < parametersInfo.Length; ++i)
				ParametersName[i] = parametersInfo[i].Name;

			HasReturnValue = (method.ReturnType != typeof(void));
		}
	}
}
