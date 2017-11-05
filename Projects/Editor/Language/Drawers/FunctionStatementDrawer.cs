// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language.Drawers
{
    public class FunctionStatementDrawer : FlowStatementDrawer
	{
		protected override float MinimumWidth
		{
			get { return 200.0F; }
		}

		public override uint RowCount
		{
			get { return (uint)StatementInstance.Slots.Length; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(FunctionStatement) }; }
		}

        protected override Color HeaderBackColor
        {
            get { return Color.Red; }
        }

        protected override Color BodyBackColor
        {
            get { return Color.Black; }
        }
    }
}