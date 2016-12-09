// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Editor.Language.Drawers;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor
{
	public class StatementDrawer : IStatementInstanceHolder
	{
		private Dictionary<Type, Drawer> drawers = new Dictionary<Type, Drawer>();

		public StatementCanvas Canvas
		{
			get;
			private set;
		}

		public StatementDrawer(StatementCanvas Canvas)
		{
			this.Canvas = Canvas;

			Type[] types = TypeHelper.GetDrievedTypesOf<Drawer>();

			if (types == null)
				return;

			for (int i = 0; i < types.Length; ++i)
			{
				Type type = types[i];

				if (type.IsAbstract)
					continue;

				Drawer drawer = (Drawer)Activator.CreateInstance(types[i], this);

				Type[] handleTypes = drawer.StatementTypes;
				if (handleTypes != null)
					for (int j = 0; j < handleTypes.Length; ++j)
						drawers[handleTypes[j]] = drawer;
			}
		}

		public void Draw(Graphics Graphics, StatementInstance StatementInstance)
		{
			Drawer drawer = GetDrawer(StatementInstance.Statement.GetType());
			drawer.Draw(Graphics, StatementInstance);
			StatementInstance.UpdateBounds();
		}

		public void DrawConections(Graphics Graphics, StatementInstance StatementInstance)
		{
			Drawer drawer = GetDrawer(StatementInstance.Statement.GetType());
			drawer.DrawConections(Graphics, StatementInstance);
		}

		private Drawer GetDrawer(Type StatementType)
		{
			if (!drawers.ContainsKey(StatementType))
				return null;

			return drawers[StatementType];
		}

		StatementInstance IStatementInstanceHolder.GetByStatement(Statement Statement)
		{
			return null;
		}
	}
}