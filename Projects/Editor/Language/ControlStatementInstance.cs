// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor
{
	public abstract class ControlStatementInstance : StatementInstance
	{
		public ControlStatementInstance(ControlStatement Statement) :
			base(Statement)
		{
			AddSlot(Slot.Types.EntryPoint, 0, CheckEntryPointAssignment);
			AddSlot(Slot.Types.Executer, 0, CheckExecuterAssignment, OnExecuterAssigned);
		}

		protected virtual bool CheckEntryPointAssignment(Slot Slot)
		{
			return !WillCauseCircularCall(Slot);
		}

		protected virtual bool CheckExecuterAssignment(Slot Slot)
		{
			return !WillCauseCircularCall(Slot);
		}

		private void OnExecuterAssigned(Slot Self, Slot Other)
		{
			ControlStatement statement = (ControlStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.CompleteStatement = Other.StatementInstance.Statement;
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