// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor
{
	public class ForStatementInstance : ControlStatementInstance
	{
		[Serialization.SerializableInstantiator((StatementInstance)null)]
		public ForStatementInstance(ForStatement Statement) :
			base(Statement)
		{
			AddSlot("Body", Slot.Types.Executer, 1, null, OnBodyAssigned, OnRemoveBodyConnection);

			AddSlot("Minimum", Slot.Types.Argument, 1, CheckVariableAssignment, OnMinimumAssigned);
			AddSlot("Maximum", Slot.Types.Argument, 2, CheckVariableAssignment, OnMaximumAssigned);
			AddSlot("Step", Slot.Types.Argument, 3, CheckVariableAssignment, OnStepAssigned);
		}

		private void OnBodyAssigned(Slot Self, Slot Other)
		{
			ForStatement statement = (ForStatement)Statement;
			
			statement.Statement = Other.StatementInstance.Statement;

			SetConnection(Self, Other);

		}

		private bool CheckVariableAssignment(Slot Other)
		{
			return (Other.StatementInstance.Statement is IntegerVariable);
		}

		private void OnMinimumAssigned(Slot Self, Slot Other)
		{
			ForStatement statement = (ForStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.MinimumValue = (IntegerVariable)Other.StatementInstance.Statement;
		}

		private void OnMaximumAssigned(Slot Self, Slot Other)
		{
			ForStatement statement = (ForStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.MaximumValue = (IntegerVariable)Other.StatementInstance.Statement;
		}

		private void OnStepAssigned(Slot Self, Slot Other)
		{
			ForStatement statement = (ForStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.StepValue = (IntegerVariable)Other.StatementInstance.Statement;
		}

		private void OnRemoveBodyConnection(Slot Self)
		{
			if (Self.ConnectedSlot != null)
				Self.ConnectedSlot.RelatedSlots.Remove(Self);

			ForStatement statement = (ForStatement)Statement;

			Self.ConnectedSlot = null;
			statement.Statement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			ForStatement statement = (ForStatement)Statement;

			UpdateConnectedSlot(Inspector, 2, statement.Statement);
			UpdateConnectedSlot(Inspector, 3, statement.MinimumValue);
			UpdateConnectedSlot(Inspector, 4, statement.MaximumValue);
			UpdateConnectedSlot(Inspector, 5, statement.StepValue);
		}
	}
}