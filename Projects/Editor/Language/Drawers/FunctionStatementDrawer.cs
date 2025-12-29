// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements.Declaration;

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
			get { return (uint)(StatementInstance.Statement.As<FunctionStatement>()).Parameters.Length + 1; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(FunctionStatement) }; }
		}

		protected override Color HeaderBackColor
		{
			get { return Color.DarkBlue; }
		}

		protected override Color BodyBackColor
		{
			get { return Color.Black; }
		}
	}
}