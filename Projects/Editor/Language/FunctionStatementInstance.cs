// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Diagnostics;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor.Language
{
	public class FunctionStatementInstance : FlowStatementInstance
    {
        [SerializableElement(3)]
        public override Statement Statement
		{
			get { return base.Statement; }
			set
			{
                base.Statement = value;

                if (base.Statement == null)
                    return;

                Debug.Assert(value is FunctionStatement);

                FunctionStatement fnstmt = (FunctionStatement)Statement;

				for (uint i = 0; i < fnstmt.ParametersName.Length; ++i)
					AddArgumentSlot(fnstmt.ParametersName[i], i + 1);

				if (fnstmt.HasReturnValue)
					AddGetterSlot(1);
			}
		}

		public FunctionStatementInstance() :
			base(null)
		{
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