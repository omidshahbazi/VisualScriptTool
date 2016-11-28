// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class IntegerVariableDrawer : VariableDrawer
	{
		public override Type StatementType
		{
			get { return typeof(IntegerVariable); }
		}

		protected override void Draw(StatementInstance StatementInstance)
		{
			base.Draw(StatementInstance);
		}
	}
}