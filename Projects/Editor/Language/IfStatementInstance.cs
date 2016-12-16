// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor
{
	public class IfStatementInstance : ControlStatementInstance
	{
		public IfStatementInstance(IfStatement Statement, PointF Position) :
			base(Statement, Position)
		{
			AddSlot("Condition", Slot.Types.Argument, 1, CheckConditionAssignment, OnConditionAssigned);

			AddSlot("True", Slot.Types.Executer, 1, null, OnTrueAssigned);
			AddSlot("False", Slot.Types.Executer, 2, null, OnFalseAssigned);
		}

		private bool CheckConditionAssignment(Slot Other)
		{
			return (Other.StatementInstance.Statement is BooleanVariable);
		}

		private void OnConditionAssigned(Slot Self, Slot Other)
		{
			IfStatement statement = (IfStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.Condition = (BooleanVariable)Other.StatementInstance.Statement;
		}

		private void OnTrueAssigned(Slot Self, Slot Other)
		{
			IfStatement statement = (IfStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.Statement = Other.StatementInstance.Statement;
		}

		private void OnFalseAssigned(Slot Self, Slot Other)
		{
			IfStatement statement = (IfStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.ElseStatment = Other.StatementInstance.Statement;
		}
	}
}