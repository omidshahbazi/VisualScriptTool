// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration.Variables;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor.Language
{
	public class VariableStatementInstance : StatementInstance
	{
		public enum Modes
		{
			None = 0,
			Getter,
			Setter
		}

		private Modes mode = Modes.None;
		private Slot slot = null;

		[SerializableElement(4)]
		public Modes Mode
		{
			get { return mode; }
			set
			{
				if (mode == value)
					return;

				mode = value;

				if (slot != null)
					RemoveSlot(slot);

				if (mode == Modes.Getter)
					slot = AddSlot(Slot.Types.Getter, 0);
				else if (mode == Modes.Setter)
					slot = AddSlot(Slot.Types.Setter, 0, CheckConditionAssignment, OnConditionAssigned, OnRemoveConditionConnection);
			}
		}

		[Serialization.SerializableInstantiator((StatementInstance)null)]
		public VariableStatementInstance(VariableStatement Statement) :
			base(Statement)
		{
		}

		private bool CheckConditionAssignment(Slot Other)
		{
			VariableSetterStatement statement = (VariableSetterStatement)Statement;

			return (statement.Variable.GetType() == Other.StatementInstance.Statement.GetType());
		}

		private void OnConditionAssigned(Slot Self, Slot Other)
		{
			VariableSetterStatement statement = (VariableSetterStatement)Statement;

			SetConnection(Self, Other);

			statement.Statement = (VariableStatement)Other.StatementInstance.Statement;
		}

		private void OnRemoveConditionConnection(Slot Self)
		{
			UnsetConnection(Self);

			VariableSetterStatement statement = (VariableSetterStatement)Statement;
			statement.Statement = null;
		}
	}
}