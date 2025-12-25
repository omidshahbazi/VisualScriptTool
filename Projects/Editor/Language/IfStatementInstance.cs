// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Editor.Language.Drawers.Controls;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration;

namespace VisualScriptTool.Editor.Language
{
	public class IfStatementInstance : ControlStatementInstance
	{
		[Serialization.SerializableInstantiator]
		public IfStatementInstance() :
			base(new IfStatement())
		{
			AddArgumentSlot("Condition", 1, CheckConditionAssignment, OnConditionAssigned, OnRemoveConditionConnection);

			AddExecuterSlot("True", 1, null, OnTrueAssigned, OnRemoveTrueConnection);
			AddExecuterSlot("False", 2, null, OnFalseAssigned, OnRemoveFalseConnection);
		}

		public override void OnPostLoad()
		{
			base.OnPostLoad();

			CheckBox conditionCheckbox = new CheckBox(this);
			conditionCheckbox.Location = new PointF(100, 55);
			conditionCheckbox.Value = ((IfStatement)Statement).ConditionDefaultValue;
			conditionCheckbox.ValueChanged += (control) => { ((IfStatement)Statement).ConditionDefaultValue = conditionCheckbox.Value; };
			AddControl(conditionCheckbox);
		}

		private bool CheckConditionAssignment(Slot Self, Type[] Constraints, Slot Other)
		{
			return (Other.StatementInstance.Statement.IsVariableOfOneOf(Constraints));
		}

		private void OnConditionAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			Statement.As<IfStatement>().Condition = Other.StatementInstance.Statement.As<VariableStatement>();
		}

		private void OnTrueAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			Statement.As<IfStatement>().Statement = Other.StatementInstance.Statement;
		}

		private void OnFalseAssigned(Slot Self, Slot Other)
		{
			SetConnection(Self, Other);

			Statement.As<IfStatement>().ElseStatment = Other.StatementInstance.Statement;
		}

		private void OnRemoveConditionConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<IfStatement>().Condition = null;
		}

		private void OnRemoveTrueConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<IfStatement>().Statement = null;
		}

		private void OnRemoveFalseConnection(Slot Self)
		{
			UnsetConnection(Self);

			Statement.As<IfStatement>().ElseStatment = null;
		}

		public override void ResolveSlotConnections(IStatementInspector Inspector)
		{
			base.ResolveSlotConnections(Inspector);

			IfStatement statement = Statement.As<IfStatement>();

			UpdateConnectedSlot(Inspector, 2, statement.Condition);
			UpdateConnectedSlot(Inspector, 3, statement.Statement);
			UpdateConnectedSlot(Inspector, 4, statement.ElseStatment);

			OnPostLoad();
		}
	}
}