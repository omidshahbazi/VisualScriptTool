// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language
{
	public class VariableSetterStatementInstance : StatementInstance
	{
		[Serialization.SerializableInstantiator((StatementInstance)null)]
		public VariableSetterStatementInstance(VariableStatement Statement) :
			base(Statement)
		{
			AddSlot(Slot.Types.Setter, 0, CheckConditionAssignment, OnConditionAssigned, OnRemoveConditionConnection);
		}

		private bool CheckConditionAssignment(Slot Other)
		{
			VariableSetterStatement statement = (VariableSetterStatement)Statement;

			return (statement.Variable.GetType() == Other.StatementInstance.Statement.GetType());
		}

		private void OnConditionAssigned(Slot Self, Slot Other)
		{
			VariableSetterStatement statement = (VariableSetterStatement)Statement;

			SetConnection(Self, Other);

			statement.Statement = (VariableStatement)Other.StatementInstance.Statement;
		}

		private void OnRemoveConditionConnection(Slot Self)
		{
			UnsetConnection(Self);

			VariableSetterStatement statement = (VariableSetterStatement)Statement;
			statement.Statement = null;
		}
	}
}