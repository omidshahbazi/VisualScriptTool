// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration;

namespace VisualScriptTool.Editor.Language
{
	public class ForStatementInstance : ControlStatementInstance
	{
		[Serialization.SerializableInstantiator]
		public ForStatementInstance() :
			base(new ForStatement())
		{
			AddExecuterSlot("Body", 1, null, OnBodyAssigned, OnRemoveBodyConnection);

			AddArgumentSlot("First", 1, CheckVariableAssignment, OnMinimumAssigned, OnRemoveMinimumConnection);
			AddArgumentSlot("Last", 2, CheckVariableAssignment, OnMaximumAssigned, OnRemoveMaximumConnection);
			AddArgumentSlot("Step", 3, CheckVariableAssignment, OnStepAssigned, OnRemoveStepConnection);
		}

		private void OnBodyAssigned(Slot Self, Slot Other)
		{
			Statement.As<ForStatement>().Statement = Other.StatementInstance.Statement;

			SetConnection(Self, Other);
		}

		private bool CheckVariableAssignment(Slot Self, Type[] Constraints, Slot Other)
		{
			return (Other.StatementInstance.Statement.IsVariableOfOneOf(Constraints));
		}

		private void OnMinimumAssigned(Slot Self, Slot Other)
		{
			Self.ConnectedSlot = Other;
			Statement.As<ForStatement>().First = Other.StatementInstance.Statement.As<VariableStatement>();
		}

		private void OnMaximumAssigned(Slot Self, Slot Other)
		{
			Self.ConnectedSlot = Other;
			Statement.As<ForStatement>().Last = Other.StatementInstance.Statement.As<VariableStatement>();
		}

		private void OnStepAssigned(Slot Self, Slot Other)
		{
			Self.ConnectedSlot = Other;
			Statement.As<ForStatement>().Step = Other.StatementInstance.Statement.As<VariableStatement>();
		}

		private void OnRemoveMinimumConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<ForStatement>().First = null;
		}

		private void OnRemoveMaximumConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<ForStatement>().Last = null;
		}

		private void OnRemoveStepConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<ForStatement>().Step = null;
		}

		private void OnRemoveBodyConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<ForStatement>().Statement = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			ForStatement statement = Statement.As<ForStatement>();

			UpdateConnectedSlot(Inspector, 2, statement.Statement);
			UpdateConnectedSlot(Inspector, 3, statement.First);
			UpdateConnectedSlot(Inspector, 4, statement.Last);
			UpdateConnectedSlot(Inspector, 5, statement.Step);
		}
	}
}