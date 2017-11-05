// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language
{
	public class FunctionStatementInstance : FlowStatementInstance
	{
		[Serialization.SerializableInstantiator]
		public FunctionStatementInstance(FunctionStatement Statement) :
			base(Statement)
		{
			for (uint i = 0; i < Statement.ParametersName.Length; ++i)
				AddArgumentSlot(Statement.ParametersName[i], i + 1);

			if (Statement.HasReturnValue)
				AddGetterSlot(1);
		}

		public override void OnPostLoad()
		{
			base.OnPostLoad();

			//CheckBox conditionCheckbox = new CheckBox(this);
			//conditionCheckbox.Location = new PointF(100, 55);
			//conditionCheckbox.Value = ((IfStatement)Statement).ConditionDefaultValue;
			//conditionCheckbox.ValueChanged += (control) => { ((IfStatement)Statement).ConditionDefaultValue = conditionCheckbox.Value; };
			//AddControl(conditionCheckbox);
		}
	}
}