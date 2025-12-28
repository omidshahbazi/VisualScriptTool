// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor.Language
{
	public class EntrypointStatementInstance : StatementInstance
	{
		[Serialization.SerializableInstantiator]
		public EntrypointStatementInstance() :
			base(new EntrypointStatement())
		{
			AddExecuterSlot("Entrypoint", 0, null, OnTrueAssigned, OnRemoveTrueConnection);
		}

		private void OnTrueAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			Statement.As<EntrypointStatement>().Statement = Other.StatementInstance.Statement;
		}

		private void OnRemoveTrueConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<EntrypointStatement>().Statement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			UpdateConnectedSlot(Inspector, 0, Statement.As<EntrypointStatement>().Statement);
		}
	}
}