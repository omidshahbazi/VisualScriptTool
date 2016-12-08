// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor
{
	public class StatementInstance
	{
		private RectangleF bounds;

		public Statement Statement
		{
			get;
			private set;
		}

		public PointF Position
		{
			get { return bounds.Location; }
			set { bounds.Location = value; }
		}

		public SizeF HeaderSize
		{
			get;
			set;
		}

		public SizeF BodySize
		{
			get;
			set;
		}

		public RectangleF Bounds
		{
			get { return bounds; }
		}

		public StatementInstance(Statement Statement, PointF Position)
		{
			bounds = new RectangleF();
			this.Statement = Statement;
			bounds.Location = Position;
		}

		public void UpdateBounds()
		{
			bounds.Size = new SizeF(HeaderSize.Width, HeaderSize.Height + BodySize.Height);
		}
	}

	public class StatementInstanceList : List<StatementInstance>
	{ }
}
