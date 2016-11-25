// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Editor.Language.Drawers;

namespace VisualScriptTool.Editor
{
	public static class StatementDrawer
	{
		private static Drawer[] drawers = null;

		public static void Initialize()
		{
			Type[] types = TypeHelper.GetDrievedTypesOf<Drawer>();

			if (types == null)
				return;

			drawers = new Drawer[types.Length];

			for (int i = 0; i < types.Length; ++i)
				drawers[i] = (Drawer)Activator.CreateInstance(types[i]);
		}

		public static void Draw(Graphics Graphics, StatementInstance StatementInstance)
		{
			Drawer drawer = GetDrawer(StatementInstance.Statement.GetType());

			drawer.Draw(Graphics, StatementInstance);
		}

		private static Drawer GetDrawer(Type StatementType)
		{
			for (int i = 0; i < drawers.Length;++i)
			{
				Drawer drawer = drawers[i];

				if (drawer.StatementType == StatementType)
					return drawer;
			}

			return null;
        }
	}
}