// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor
{
	public class ForStatementInstance : ControlStatementInstance
	{
		public ForStatementInstance(ForStatement Statement, PointF Position) :
			base(Statement, Position)
		{
			AddSlot("Body", Slot.Types.Executer, 1, OnBodyAssigned);

			AddSlot("Minimum", Slot.Types.Argument, 1, OnMinimumAssigned);
			AddSlot("Maximum", Slot.Types.Argument, 2, OnMaximumAssigned);
			AddSlot("Step", Slot.Types.Argument, 3, OnStepAssigned);
		}

		private void OnBodyAssigned(Slot Self, Slot Other)
		{
			ForStatement statement = (ForStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.Statement = Other.StatementInstance.Statement;
		}

		private void OnMinimumAssigned(Slot Self, Slot Other)
		{
			ForStatement statement = (ForStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.MinimumValue = (IntegerVariable)Other.StatementInstance.Statement;
		}

		private void OnMaximumAssigned(Slot Self, Slot Other)
		{
			ForStatement statement = (ForStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.MaximumValue = (IntegerVariable)Other.StatementInstance.Statement;
		}

		private void OnStepAssigned(Slot Self, Slot Other)
		{
			ForStatement statement = (ForStatement)Statement;

			Self.ConnectedSlot = Other;
			statement.StepValue = (IntegerVariable)Other.StatementInstance.Statement;
		}
	}
}