// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration;

namespace VisualScriptTool.Editor.Language
{
	public class WhileStatementInstance : ControlStatementInstance
	{
		[Serialization.SerializableInstantiator((StatementInstance)null)]
		public WhileStatementInstance() :
			base(new WhileStatement())
		{
			AddArgumentSlot("Condition", 1, CheckConditionAssignment, OnConditionAssigned, OnRemoveConditionConnection);

			AddExecuterSlot("Body", 1, null, OnBodyAssigned, OnRemoveBodyConnection);
		}

		private bool CheckConditionAssignment(Slot Self, Type[] Constraints, Slot Other)
		{
			return (Other.StatementInstance.Statement.IsVariableOfOneOf(Constraints));
		}

		private void OnConditionAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			Statement.As<WhileStatement>().Condition = Other.StatementInstance.Statement.As<VariableStatement>();
		}

		private void OnBodyAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			Statement.As<WhileStatement>().Statement = Other.StatementInstance.Statement;
		}

		private void OnRemoveConditionConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<WhileStatement>().Condition = null;
		}

		private void OnRemoveBodyConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<WhileStatement>().Statement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			WhileStatement statement = Statement.As<WhileStatement>();

			UpdateConnectedSlot(Inspector, 2, statement.Condition);
			UpdateConnectedSlot(Inspector, 3, statement.Statement);
		}
	}
}