// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Declaration.Variables;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class VariableDrawer : Drawer
	{
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
			get { return Color.Purple; }
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

		protected override void DrawBody(StatementInstance StatementInstance)
		{
			base.DrawBody(StatementInstance);

			//DrawSlots(GetRightSlotPosition(StatementInstance, 0));
			SlotList slots = StatementInstance.Slots;
			for (int i = 0; i < slots.Count; ++i)
				DrawSlot(slots[i]);
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