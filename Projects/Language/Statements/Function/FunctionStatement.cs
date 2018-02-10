// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Reflection;
using System.Text;
using VisualScriptTool.Language.Extensions;
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

		[SerializableElement(2)]
		public string MethodInfo
		{
			get { return method.GetFullName(); }
			set { Method = MethodInfoExtensions.Get(value); }
		}

		public MethodInfo Method
		{
			get { return method; }
			set
			{
				method = value;

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

		public FunctionStatement()
		{
		}
	}
}
