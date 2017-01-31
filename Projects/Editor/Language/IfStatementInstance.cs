// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language
{
	public class IfStatementInstance : ControlStatementInstance
	{
		[Serialization.SerializableInstantiator]
		public IfStatementInstance() :
			base(new IfStatement())
		{
			AddSlot("Condition", Slot.Types.Argument, 1, CheckConditionAssignment, OnConditionAssigned, OnRemoveConditionConnection);

			AddSlot("True", Slot.Types.Executer, 1, null, OnTrueAssigned, OnRemoveTrueConnection);
			AddSlot("False", Slot.Types.Executer, 2, null, OnFalseAssigned, OnRemoveFalseConnection);
		}

		private bool CheckConditionAssignment(Slot Other)
		{
			return (Other.StatementInstance.Statement is BooleanVariable);
		}

		private void OnConditionAssigned(Slot Self, Slot Other)
		{
			IfStatement statement = (IfStatement)Statement;

			SetConnection(Self, Other);

			statement.Condition = (BooleanVariable)Other.StatementInstance.Statement;
		}

		private void OnTrueAssigned(Slot Self, Slot Other)
		{
			IfStatement statement = (IfStatement)Statement;

			SetConnection(Self, Other);

			statement.Statement = Other.StatementInstance.Statement;
		}

		private void OnFalseAssigned(Slot Self, Slot Other)
		{
			IfStatement statement = (IfStatement)Statement;

			SetConnection(Self, Other);

			statement.ElseStatment = Other.StatementInstance.Statement;
		}

		private void OnRemoveConditionConnection(Slot Self)
		{
			UnsetConnection(Self);

			IfStatement statement = (IfStatement)Statement;
			statement.Condition = null;
		}

		private void OnRemoveTrueConnection(Slot Self)
		{
			UnsetConnection(Self);

			IfStatement statement = (IfStatement)Statement;
			statement.Statement = null;
		}

		private void OnRemoveFalseConnection(Slot Self)
		{
			UnsetConnection(Self);

			IfStatement statement = (IfStatement)Statement;
			statement.ElseStatment = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			IfStatement statement = (IfStatement)Statement;

			UpdateConnectedSlot(Inspector, 2, statement.Condition);
			UpdateConnectedSlot(Inspector, 3, statement.Statement);
			UpdateConnectedSlot(Inspector, 4, statement.ElseStatment);
		}
	}
}