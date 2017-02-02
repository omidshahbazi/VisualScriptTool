// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class VariableSetterDrawer : VariableDrawer
	{
		protected override float MinimumWidth
		{
			get { return 100.0F; }
		}

		public override uint RowCount
		{
			get { return 1; }
		}

		protected override Color HeaderBackColor
		{
			get { return HEADER_COLOR; }
		}

		protected override Color BodyBackColor
		{
			get { return Color.Black; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(VariableSetterStatement) }; }
		}

		public VariableSetterDrawer()
		{
		}
	}
}