// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor
{
	public class StatementInstance
	{
		public Statement Statement
		{
			get;
			private set;
		}

		public PointF Position
		{
			get;
			set;
		}

		public StatementInstance(Statement Statement, PointF Position)
		{
			this.Statement = Statement;
			this.Position = Position;
		}
	}

	public class StatementInstanceList : List<StatementInstance>
	{ }
}
