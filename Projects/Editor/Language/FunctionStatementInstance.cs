// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Diagnostics;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration.Variables;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor.Language
{
	public class FunctionStatementInstance : FlowStatementInstance
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

				Debug.Assert(value is FunctionStatement);

				FunctionStatement fnstmt = (FunctionStatement)Statement;

				for (uint i = 0; i < fnstmt.ParametersName.Length; ++i)
					AddArgumentSlot(fnstmt.ParametersName[i], i + 1, CheckParameterAssignment, OnParameterAssigned, OnRemoveParameterConnection);

				if (fnstmt.HasReturnValue)
					AddGetterSlot(1);
			}
		}

		public FunctionStatementInstance() :
			base(null)
		{
		}

		private bool CheckParameterAssignment(Slot Other)
		{
			return (Other.StatementInstance.Statement is FloatVariable);
		}

		private void OnParameterAssigned(Slot Self, Slot Other)
		{
			FunctionStatement statement = (FunctionStatement)Statement;

			SetConnection(Self, Other);

			//statement.Condition = (FloatVariable)Other.StatementInstance.Statement;
		}

		private void OnRemoveParameterConnection(Slot Self)
		{
			UnsetConnection(Self);

			FunctionStatement statement = (FunctionStatement)Statement;
			//statement.Condition = null;
		}
	}
}