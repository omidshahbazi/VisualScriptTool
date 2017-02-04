// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Editor.Language.Drawers;
using VisualScriptTool.Reflection;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor
{
	public class StatementDrawer
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

			Type[] types = TypeUtils.GetDrievedTypesOf<Drawer>();

			if (types == null)
				return;

			for (int i = 0; i < types.Length; ++i)
			{
				Type type = types[i];

				if (type.IsAbstract)
					continue;

				Drawer drawer = (Drawer)Activator.CreateInstance(types[i]);

				Type[] handleTypes = drawer.StatementTypes;
				if (handleTypes != null)
					for (int j = 0; j < handleTypes.Length; ++j)
						drawers[handleTypes[j]] = drawer;
			}
		}

		public void Draw(IDevice Device, StatementInstance StatementInstance)
		{
			Drawer drawer = GetDrawer(StatementInstance);
			drawer.Draw(Device, StatementInstance);
		}

		public void DrawConections(IDevice Device, StatementInstance StatementInstance)
		{
			Drawer drawer = GetDrawer(StatementInstance);
			drawer.DrawConections(StatementInstance);
		}

		public Drawer GetDrawer(StatementInstance StatementInstance)
		{
			return GetDrawer(StatementInstance.Statement.GetType());
		}

		private Drawer GetDrawer(Type StatementType)
		{
			if (!drawers.ContainsKey(StatementType))
				return null;

			return drawers[StatementType];
		}
	}
}