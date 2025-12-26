// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Extensions;
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
			SetConnection(Self, Other);

			Statement.As<ExecuterStatement>().Statement = Other.StatementInstance.Statement;
		}

		private void OnRemoveTrueConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<ExecuterStatement>().Statement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			UpdateConnectedSlot(Inspector, 0, Statement.As<ExecuterStatement>().Statement);
		}
	}
}