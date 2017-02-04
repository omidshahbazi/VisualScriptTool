// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language
{
	public class VariableGetterStatementInstance : StatementInstance
	{
		[Serialization.SerializableInstantiator((StatementInstance)null)]
		public VariableGetterStatementInstance(VariableStatement Statement) :
					base(Statement)
		{
			AddSlot(Slot.Types.Getter, 0);
		}
	}
}