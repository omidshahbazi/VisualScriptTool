// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Declaration;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor.Language
{
	public class FunctionStatementInstance : StatementInstance
	{
		[SerializableElement(3)]
		public override Statement Statement
		{
			get { return base.Statement; }
			set
			{
				base.Statement = value;

				SetValues();
			}
		}

		public FunctionStatementInstance() :
			base(new FunctionStatement())
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

		private void SetValues()
		{
			//TODO: CLEAR SLOTS FIRST?

			FunctionStatement statement = Statement.As<FunctionStatement>();

			for (uint i = 0; i < statement.Parameters.Length; ++i)
				AddArgumentSlot(statement.Parameters[i].Name, i + 1, CheckParameterAssignment, OnParameterAssigned, OnRemoveParameterConnection);

			if (statement.HasReturnValue)
				AddGetterSlot(1);
		}
	}
}