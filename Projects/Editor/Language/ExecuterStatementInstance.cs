// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor.Language
{
	public class ExecuterStatementInstance : StatementInstance
	{
		[Serialization.SerializableInstantiator]
		public ExecuterStatementInstance() :
			base(new ExecuterStatement())
		{
			AddExecuterSlot("Execute", 0, null, OnTrueAssigned, OnRemoveTrueConnection);
		}

		private void OnTrueAssigned(Slot Self, Slot Other)
		{
			ExecuterStatement statement = (ExecuterStatement)Statement;

			SetConnection(Self, Other);

			statement.Statement = Other.StatementInstance.Statement;
		}

		private void OnRemoveTrueConnection(Slot Self)
		{
			UnsetConnection(Self);

			ExecuterStatement statement = (ExecuterStatement)Statement;
			statement.Statement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			ExecuterStatement statement = (ExecuterStatement)Statement;

			UpdateConnectedSlot(Inspector, 0, statement.Statement);
		}
	}
}