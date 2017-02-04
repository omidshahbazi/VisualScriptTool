// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language
{
	public abstract class FlowStatementInstance : StatementInstance
	{
		public FlowStatementInstance(FlowStatement Statement) :
			base(Statement)
		{
			AddSlot(Slot.Types.EntryPoint, 0, CheckEntryPointAssignment);
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

		private void OnExecuterAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			FlowStatement statement = (FlowStatement)Statement;
			statement.CompleteStatement = Other.StatementInstance.Statement;
		}

		private void OnExecuterRemoveConnection(Slot Self)
		{
			UnsetConnection(Self);

			FlowStatement statement = (FlowStatement)Statement;
			statement.CompleteStatement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			FlowStatement statement = (FlowStatement)Statement;

			UpdateConnectedSlot(Inspector, 1, statement.CompleteStatement);
		}

		protected bool WillCauseCircularCall(Slot Slot)
		{
			return false;
		}
	}
}