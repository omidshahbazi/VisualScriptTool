// Copyright 2016-2017 ?????????????. All Rights Reserved.
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
			get { return 140.0F; }
		}

		public override uint SlotsCount
		{
			get { return 3; }
		}

		public override Type[] StatementTypes
		{
			get { return new Type[] { typeof(IfStatement) }; }
		}

		public IfStatementDrawer(IStatementInstanceHolder StatementInstanceHolder) :
			base(StatementInstanceHolder)
		{
		}

		protected override void DrawBody(StatementInstance StatementInstance)
		{
			base.DrawBody(StatementInstance);

			DrawExecuteSlot(GetRightSlotPosition(StatementInstance, 1));
			DrawExecuteSlot(GetRightSlotPosition(StatementInstance, 2));

			DrawVariableSlot(GetLeftSlotPosition(StatementInstance, 1));
		}

		public override void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
			base.DrawConections(Graphics, StatementInstance);

			IfStatement statement = (IfStatement)StatementInstance.Statement;

			if (statement.Statement != null)
				DrawLine(GetRightSlotConnectionPosition(StatementInstance, 1), Directions.Out, GetLeftSlotConnectionPosition(GetInstanceByStatement(statement.Statement), 0), Directions.In, ExecuteConnectionPen);

			if (statement.ElseStatment != null)
				DrawLine(GetRightSlotConnectionPosition(StatementInstance, 2), Directions.Out, GetLeftSlotConnectionPosition(GetInstanceByStatement(statement.ElseStatment), 0), Directions.In, ExecuteConnectionPen);

			if (statement.Condition != null)
				DrawLine(GetLeftSlotConnectionPosition(StatementInstance, 1), Directions.In, GetRightSlotConnectionPosition(GetInstanceByStatement(statement.Condition), 0), Directions.Out, VariableConnectionPen);
		}

		public override bool IsLeftSlotActive(uint Index)
		{
			if (base.IsLeftSlotActive(Index))
				return true;

			return (Index == 1);
		}

		public override bool IsRightSlotActive(uint Index)
		{
			if (base.IsRightSlotActive(Index))
				return true;

			return (Index == 1 || Index == 2);
		}
	}
}