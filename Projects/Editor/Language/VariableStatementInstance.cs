// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language
{
	public class VariableStatementInstance : StatementInstance
	{
		public enum Modes
		{
			Getter = 0,
			Setter
		}

		public Slot Slot
		{
			get;
			private set;
		}

		[Serialization.SerializableInstantiator((StatementInstance)null)]
		public VariableStatementInstance(VariableStatement Statement, Modes Mode) :
			base(Statement)
		{
			Slot = AddSlot((Mode == Modes.Getter ? Slot.Types.Getter : Slot.Types.Setter), 0);
		}
	}
}