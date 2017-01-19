// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor
{
	public abstract class ControlStatementInstance : StatementInstance
	{
		public ControlStatementInstance(ControlStatement Statement) :
			base(Statement)
		{
			AddSlot(Slot.Types.EntryPoint, 0, CheckEntryPointAssignment, OnEntryPointAssigned);
			AddSlot(Slot.Types.Executer, 0, CheckExecuterAssignment, OnExecuterAssigned, OnExecuterRemoveConnection);
		}

		protected virtual bool CheckEntryPointAssignment(Slot Other)
		{
			return !WillCauseCircularCall(Other);
		}

		protected virtual bool CheckExecuterAssignment(Slot Other)
		{
			return !WillCauseCircularCall(Other);
		}

		private void OnEntryPointAssigned(Slot Self, Slot Other)
		{
			Self.RelatedSlots.Add(Other);
		}

		private void OnExecuterAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			ControlStatement statement = (ControlStatement)Statement;
			statement.CompleteStatement = Other.StatementInstance.Statement;
		}

		private void OnExecuterRemoveConnection(Slot Self)
		{
			if (Self.ConnectedSlot != null)
				Self.ConnectedSlot.RelatedSlots.Remove(Self);

			ControlStatement statement = (ControlStatement)Statement;

			Self.ConnectedSlot = null;
			statement.CompleteStatement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			ControlStatement statement = (ControlStatement)Statement;

			UpdateConnectedSlot(Inspector, 1, statement.CompleteStatement);
		}

		protected bool WillCauseCircularCall(Slot Slot)
		{
			return false;
		}
	}
}