// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class IntegerVariableDrawer : Drawer
	{
		public override Type StatementType
		{
			get { return typeof(IntegerVariable); }
		}

		protected override void Draw(StatementInstance StatementInstance)
		{
		}
	}
}
