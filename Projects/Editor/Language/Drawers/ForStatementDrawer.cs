// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public class ForStatementDrawer : ControlStatementDrawer
	{
		private Brush backBrush = null;

		protected override float MinimumWidth
		{
			get { return 160.0F; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(ForStatement) }; }
		}

		protected override float BodyHeight
		{
			get { return SLOT_HEIGHT * 4; }
		}

		public ForStatementDrawer(IStatementInstanceHolder StatementInstanceHolder) :
			base(StatementInstanceHolder)
		{
			backBrush = new SolidBrush(Color.Black);
		}

		protected override void DrawBody(StatementInstance StatementInstance)
		{
			base.DrawBody(StatementInstance);

			DrawExecuteSlot(GetRightSlotPosition(StatementInstance, 1));

			DrawVariableSlot(GetLeftSlotPosition(StatementInstance, 1));
			DrawVariableSlot(GetLeftSlotPosition(StatementInstance, 2));
			DrawVariableSlot(GetLeftSlotPosition(StatementInstance, 3));
		}

		public override void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
			base.DrawConections(Graphics, StatementInstance);

			ForStatement statement = (ForStatement)StatementInstance.Statement;

			if (statement.MinimumValue != null)
				DrawLine(GetLeftSlotConnectionPosition(StatementInstance, 1), Directions.In, GetRightSlotConnectionPosition(GetInstanceByStatement(statement.MinimumValue), 0), Directions.Out, VariableConnectionPen);

			if (statement.MaximumValue != null)
				DrawLine(GetLeftSlotConnectionPosition(StatementInstance, 2), Directions.In, GetRightSlotConnectionPosition(GetInstanceByStatement(statement.MaximumValue), 0), Directions.Out, VariableConnectionPen);

			if (statement.StepValue != null)
				DrawLine(GetLeftSlotConnectionPosition(StatementInstance, 3), Directions.In, GetRightSlotConnectionPosition(GetInstanceByStatement(statement.StepValue), 0), Directions.Out, VariableConnectionPen);

			if (statement.Statement != null)
				DrawLine(GetRightSlotConnectionPosition(StatementInstance, 1), Directions.Out, GetLeftSlotConnectionPosition(GetInstanceByStatement(statement.Statement), 0), Directions.In, ExecuteConnectionPen);
		}
	}
}