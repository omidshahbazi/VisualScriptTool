// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor
{
	public abstract class ControlStatementInstance : StatementInstance
	{
		public ControlStatementInstance(ControlStatement Statement) :
			base(Statement)
		{
			AddSlot(Slot.Types.EntryPoint, 0);
			AddSlot(Slot.Types.Executer, 0, null, OnExecuterAssigned);
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

			Slot slot = GetSlot(1);

			StatementInstance instance = Inspector.GetInstance(statement.CompleteStatement);

			if (instance != null)
				for (int i = 0; i < instance.slots.Count; ++i)
					if (slot.IsAssignmentAllowed(instance.slots[i]))
					{
						slot.ConnectedSlot = instance.slots[i];
						break;
					}
		}
	}
}