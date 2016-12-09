// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor
{
	public class StatementCanvas : GridCanvas, IStatementInstanceHolder
	{
		private StatementDrawer drawer = null;
		private StatementInstanceList candidateToSelectStatements = new StatementInstanceList();
		private PointF lastMousePosition;
		private Pen selectedPen = null;

		public StatementInstanceList Statements
		{
			get;
			private set;
		}

		public StatementInstanceList SelectedStatements
		{
			get;
			private set;
		}

		public StatementCanvas()
		{
			drawer = new StatementDrawer(this);
			Statements = new StatementInstanceList();
			SelectedStatements = new StatementInstanceList();

			selectedPen = new Pen(Color.Orange, 1.5F);
		}

		protected override void OnDrawCanvas(Graphics Graphics)
		{
			base.OnDrawCanvas(Graphics);

			for (int i = 0; i < Statements.Count; ++i)
				drawer.Draw(Graphics, Statements[i]);

			for (int i = 0; i < SelectedStatements.Count; ++i)
			{
				RectangleF rect = SelectedStatements[i].Bounds;

				Graphics.DrawRectangle(selectedPen, rect.X, rect.Y, rect.Width, rect.Height);
			}

			for (int i = 0; i < Statements.Count; ++i)
				drawer.DrawConections(Graphics, Statements[i]);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == MouseButtons.Middle)
				return;

			SelectedStatements.Clear();
			candidateToSelectStatements.Clear();

			PointF location = ScreenToCanvas(e.Location);

			for (int i = Statements.Count - 1; i >= 0; --i)
			{
				StatementInstance statement = Statements[i];

				if (statement.Bounds.Contains(location))
				{
					candidateToSelectStatements.Add(statement);
					break;
				}
			}

			lastMousePosition = ScreenToCanvas(e.Location);

			Refresh();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (candidateToSelectStatements.Count != 0)
			{
				SelectedStatements.AddRange(candidateToSelectStatements);
				candidateToSelectStatements.Clear();
			}

			if (e.Button == MouseButtons.Left && SelectedStatements.Count != 0)
			{
				PointF location = ScreenToCanvas(e.Location);
				PointF delta = location.Subtract(lastMousePosition);

				for (int i = 0; i < SelectedStatements.Count; ++i)
				{
					StatementInstance statement = SelectedStatements[i];

					statement.Position = statement.Position.Add(delta);
				}

				lastMousePosition = location;

				Refresh();
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (candidateToSelectStatements.Count == 0)
				return;

			SelectedStatements.Clear();
			SelectedStatements.AddRange(candidateToSelectStatements);
			candidateToSelectStatements.Clear();

			Refresh();
		}

		StatementInstance IStatementInstanceHolder.GetByStatement(Statement Statement)
		{
			for (int i = 0; i < Statements.Count; ++i)
				if (Statements[i].Statement == Statement)
					return Statements[i];

			return null;
		}
	}
}