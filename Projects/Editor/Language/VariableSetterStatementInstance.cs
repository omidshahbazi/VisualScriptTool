// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Extensions;
using System;

namespace VisualScriptTool.Editor.Language
{
	public class VariableSetterStatementInstance : FlowStatementInstance
	{
		[Serialization.SerializableInstantiator((StatementInstance)null)]
		public VariableSetterStatementInstance(VariableSetterStatement Statement) :
			base(Statement)
		{
			AddArgumentSlot(1, CheckConditionAssignment, OnConditionAssigned, OnRemoveConditionConnection);
		}

		private bool CheckConditionAssignment(Slot Self, Type[] Constraints, Slot Other)
		{
			return (Other.StatementInstance.Statement.IsVariableOfOneOf(Statement.As<VariableSetterStatement>().Variable.Value.Type));
		}

		private void OnConditionAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			Statement.As<VariableSetterStatement>().ValueStatement = Other.StatementInstance.Statement.As<VariableStatement>();
		}

		private void OnRemoveConditionConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<VariableSetterStatement>().ValueStatement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			UpdateConnectedSlot(Inspector, 2, Statement.As<VariableSetterStatement>().ValueStatement);
		}
	}
}