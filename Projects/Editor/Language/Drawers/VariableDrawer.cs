﻿// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class VariableDrawer : Drawer
	{
		public static readonly Color HEADER_COLOR =  Color.Purple;

		protected override float MinimumWidth
		{
			get { return 100.0F; }
		}

		public override uint SlotsCount
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
			get { return new Type[] { typeof(BooleanVariable), typeof(FloatVariable), typeof(IntegerVariable), typeof(StringVariable) }; }
		}

		public VariableDrawer(IStatementInstanceHolder StatementInstanceHolder) :
			base(StatementInstanceHolder)
		{
		}

		public override bool IsLeftSlotActive(uint Index)
		{
			return false;
		}

		public override bool IsRightSlotActive(uint Index)
		{
			return (Index == 0);
		}
	}
}