﻿// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class IfStatementDrawer : ControlStatementDrawer
	{
		private CubicSPLine spline = new CubicSPLine();

		protected override float MinimumWidth
		{
			get { return 170.0F; }
		}

		public override uint RowCount
		{
			get { return 3; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(IfStatement) }; }
		}

		public IfStatementDrawer()
		{
		}

		public override void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
			base.DrawConections(Graphics, StatementInstance);

			IfStatement statement = (IfStatement)StatementInstance.Statement;
		}
	}
}