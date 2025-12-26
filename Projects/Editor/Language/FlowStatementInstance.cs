// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language
{
	public abstract class FlowStatementInstance : StatementInstance
	{
		public FlowStatementInstance(FlowStatement Statement) :
			base(Statement)
		{
			AddEntryPointSlot(0, CheckEntryPointAssignment);
			AddExecuterSlot(0, CheckExecuterAssignment, OnExecuterAssigned, OnExecuterRemoveConnection);
		}

		protected virtual bool CheckEntryPointAssignment(Slot Self, Type[] Constraints, Slot Other)
		{
			return !WillCauseCircularCall(Other);
		}

		protected virtual bool CheckExecuterAssignment(Slot Self, Type[] Constraints, Slot Other)
		{
			return !WillCauseCircularCall(Other);
		}

		private void OnExecuterAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			Statement.As<FlowStatement>().CompleteStatement = Other.StatementInstance.Statement;
		}

		private void OnExecuterRemoveConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<FlowStatement>().CompleteStatement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			UpdateConnectedSlot(Inspector, 1, Statement.As<FlowStatement>().CompleteStatement);
		}

		protected bool WillCauseCircularCall(Slot Slot)
		{
			throw new NotImplementedException();
		}
	}
}