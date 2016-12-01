// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;

namespace VisualScriptTool.Editor
{
	public class StatementCanvas : GridCanvas
	{
		public StatementInstanceList Statements
		{
			get;
			set;
		}

		public StatementCanvas()
		{
			Statements = new StatementInstanceList();
		}

		protected override void OnDrawCanvas(Graphics Graphics)
		{
			base.OnDrawCanvas(Graphics);

			for (int i = 0; i < Statements.Count; ++i)
			{
				StatementInstance statementInstance = Statements[i];

				StatementDrawer.Draw(Graphics, statementInstance);
			}
		}
	}
}