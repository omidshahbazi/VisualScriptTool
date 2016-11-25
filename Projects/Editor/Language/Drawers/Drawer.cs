// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;

namespace VisualScriptTool.Editor.Language.Drawers
{
	public abstract class Drawer
	{
		protected Graphics Graphics
		{
			get;
			private set;
		}

		public abstract Type StatementType
		{
			get;
		}

		public void Draw(Graphics Graphics, StatementInstance StatementInstance)
		{
			this.Graphics = Graphics;

			Draw(StatementInstance);
		}

		protected abstract void Draw(StatementInstance StatementInstance);
	}
}
