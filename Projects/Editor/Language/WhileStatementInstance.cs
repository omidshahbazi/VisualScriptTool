// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language
{
	public class WhileStatementInstance : ControlStatementInstance
	{
		[Serialization.SerializableInstantiator((StatementInstance)null)]
		public WhileStatementInstance() :
			base(new WhileStatement())
		{
			AddArgumentSlot("Condition", 1, CheckConditionAssignment, OnConditionAssigned, OnRemoveConditionConnection);

			AddExecuterSlot("Body", 1, null, OnBodyAssigned, OnRemoveBodyConnection);
		}

		private bool CheckConditionAssignment(Slot Other)
		{
			return (Other.StatementInstance.Statement is BooleanVariable);
		}

		private void OnConditionAssigned(Slot Self, Slot Other)
		{
			WhileStatement statement = (WhileStatement)Statement;

			SetConnection(Self, Other);

			statement.Condition = (BooleanVariable)Other.StatementInstance.Statement;
		}

		private void OnBodyAssigned(Slot Self, Slot Other)
		{
			WhileStatement statement = (WhileStatement)Statement;

			SetConnection(Self, Other);

			statement.Statement = Other.StatementInstance.Statement;
		}

		private void OnRemoveConditionConnection(Slot Self)
		{
			UnsetConnection(Self);

			WhileStatement statement = (WhileStatement)Statement;
			statement.Condition = null;
		}

		private void OnRemoveBodyConnection(Slot Self)
		{
			UnsetConnection(Self);

			WhileStatement statement = (WhileStatement)Statement;
			statement.Statement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			WhileStatement statement = (WhileStatement)Statement;

			UpdateConnectedSlot(Inspector, 2, statement.Condition);
			UpdateConnectedSlot(Inspector, 3, statement.Statement);
		}
	}
}