// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Diagnostics;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor.Language
{
	public class FunctionCallStatementInstance : FlowStatementInstance
	{
		[SerializableElement(3)]
		public override Statement Statement
		{
			get { return base.Statement; }
			set
			{
				base.Statement = value;

				if (base.Statement == null)
					return;

				Debug.Assert(value is FunctionCallStatement);

				FunctionCallStatement statement = Statement.As<FunctionCallStatement>();

				for (uint i = 0; i < statement.ParametersName.Length; ++i)
					AddArgumentSlot(statement.ParametersName[i], i + 1, CheckParameterAssignment, OnParameterAssigned, OnRemoveParameterConnection);

				if (statement.HasReturnValue)
					AddGetterSlot(1);
			}
		}

		public FunctionCallStatementInstance() :
			base(null)
		{
		}

		private bool CheckParameterAssignment(Slot Self, Type[] Constraints, Slot Other)
		{
			//TODO: What types?
			return (Other.StatementInstance.Statement.IsVariableOfOneOf(typeof(object)));
		}

		private void OnParameterAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			//Statement.As<FunctionStatement>().Condition = (FloatVariable)Other.StatementInstance.Statement.As<>;
		}

		private void OnRemoveParameterConnection(Slot Self)
		{
			UnsetConnection(Self);

			//Statement.As<FunctionStatement>().Condition = null;
		}
	}
}