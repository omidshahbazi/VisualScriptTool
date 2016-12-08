// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Editor.Language.Drawers;

namespace VisualScriptTool.Editor
{
	public static class StatementDrawer
	{
		private static Dictionary<Type, Drawer> drawers = null;

		public static void Initialize()
		{
			drawers = new Dictionary<Type, Drawer>();

			Type[] types = TypeHelper.GetDrievedTypesOf<Drawer>();

			if (types == null)
				return;

			for (int i = 0; i < types.Length; ++i)
			{
				Type type = types[i];

				if (type.IsAbstract)
					continue;

				Drawer drawer = (Drawer)Activator.CreateInstance(types[i]);
				drawers[drawer.StatementType] = drawer;
			}
		}

		public static void Draw(Graphics Graphics, StatementInstance StatementInstance)
		{
			Drawer drawer = GetDrawer(StatementInstance.Statement.GetType());
			drawer.Draw(Graphics, StatementInstance);
			StatementInstance.UpdateBounds();
		}

		private static Drawer GetDrawer(Type StatementType)
		{
			if (!drawers.ContainsKey(StatementType))
				return null;

			return drawers[StatementType];
        }
	}
}