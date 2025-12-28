// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Reflection;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Control
{
	//TODO: Which properties should be here?
	public class FunctionCallStatement : FlowStatement
	{
		private MethodInfo method = null;
		private Statement[] arguments = null;

		[SerializableElement(2)]
		public override string Name
		{
			get;
			set;
		}

		[SerializableElement(3)]
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

				arguments = new Statement[parametersInfo.Length];
				ParametersDefaultValue = new object[parametersInfo.Length];
				ParametersName = new string[parametersInfo.Length];

				for (int i = 0; i < parametersInfo.Length; ++i)
					ParametersName[i] = parametersInfo[i].Name;

				HasReturnValue = (Method.ReturnType != typeof(void));
			}
		}

		public Statement[] Arguments
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

		public FunctionCallStatement()
		{
		}
	}
}
