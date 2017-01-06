// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
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
	}
}