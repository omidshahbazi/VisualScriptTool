// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Editor.Language.Drawers;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor
{
	public class StatementCanvas : GridCanvas, IStatementInstanceHolder
	{
		private const float SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT = 20.0F;
		private const float HALF_SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT = SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT / 2.0F;

		private StatementDrawer drawer = null;
		private StatementInstanceList candidateToSelectStatements = new StatementInstanceList();
		private PointF lastMousePosition;
		private Pen selectedPen = null;
		private ContextMenuStrip contextMenu;
		private System.ComponentModel.IContainer components;
		private ToolStripMenuItem sampleToolStripMenuItem;
		private CubicSPLine newConnectionLine = new CubicSPLine();

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

		public Slot SelectedSlot
		{
			get;
			set;
		}

		public Slot MouseOverSlot
		{
			get;
			set;
		}

		public StatementCanvas()
		{
			InitializeComponent();

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

			if (SelectedSlot != null)
			{
				newConnectionLine.Draw(Graphics, Pens.White);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			PointF location = ScreenToCanvas(e.Location);

			if (e.Button == MouseButtons.Middle)
				return;

			SelectedStatements.Clear();
			candidateToSelectStatements.Clear();

			if ((SelectedSlot = GetSlotAtLocation(location)) != null)
			{
				SelectedStatements.Add(SelectedSlot.StatementInstance);
				return;
			}

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

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (SelectedSlot != null)
			{
				if (MouseOverSlot == null)
					contextMenu.Show(this, e.Location);
				else
				{
					MouseOverSlot.AssignConnection(SelectedSlot);
					SelectedSlot.AssignConnection(MouseOverSlot);
				}

				newConnectionLine.Clear();
			}

			if (candidateToSelectStatements.Count != 0)
			{
				SelectedStatements.Clear();
				SelectedStatements.AddRange(candidateToSelectStatements);
				candidateToSelectStatements.Clear();
			}

			Refresh();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			PointF location = ScreenToCanvas(e.Location);

			if (SelectedSlot != null && (MouseOverSlot = GetSlotAtLocation(location)) != null)
			{
				if (!SelectedSlot.IsAssignmentAllowed(MouseOverSlot))
					MouseOverSlot = null;
			}

			if (candidateToSelectStatements.Count != 0)
			{
				SelectedStatements.AddRange(candidateToSelectStatements);
				candidateToSelectStatements.Clear();
			}

			if (e.Button == MouseButtons.Left)
			{
				if (SelectedSlot != null)
				{
					PointF startOffset = PointF.Empty;
					PointF endOffset = PointF.Empty;
					if (SelectedSlot.IsLeftAligned)
					{
						startOffset.X = -ControlStatementDrawer.LINE_START_OFFSET_AMOUNT;
						endOffset.X = ControlStatementDrawer.LINE_START_OFFSET_AMOUNT;
					}
					else
					{
						startOffset.X = ControlStatementDrawer.LINE_START_OFFSET_AMOUNT;
						endOffset.X = -ControlStatementDrawer.LINE_START_OFFSET_AMOUNT;
					}

					newConnectionLine.Update(SelectedSlot.Center, startOffset, location, endOffset);
				}
				else if (SelectedStatements.Count != 0)
				{
					PointF delta = location.Subtract(lastMousePosition);

					for (int i = 0; i < SelectedStatements.Count; ++i)
					{
						StatementInstance statement = SelectedStatements[i];

						statement.Position = statement.Position.Add(delta);
					}

					lastMousePosition = location;
				}

				Refresh();
			}
		}

		private Slot GetSlotAtLocation(PointF Location)
		{
			for (int i = Statements.Count - 1; i >= 0; --i)
			{
				StatementInstance instance = Statements[i];

				for (uint j = 0; j < instance.Slots.Length; ++j)
				{
					Slot slot = instance.Slots[j];

					RectangleF bounds = slot.Bounds;

					bounds.Location.Add(new PointF(-HALF_SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT, -HALF_SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT));
					bounds.Inflate(SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT, SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT);

					if (bounds.Contains(Location))
						return slot;
				}
			}

			return null;
		}

		StatementInstance IStatementInstanceHolder.GetByStatement(Statement Statement)
		{
			for (int i = 0; i < Statements.Count; ++i)
				if (Statements[i].Statement == Statement)
					return Statements[i];

			return null;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.sampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.sampleToolStripMenuItem});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(153, 48);
			// 
			// sampleToolStripMenuItem
			// 
			this.sampleToolStripMenuItem.Name = "sampleToolStripMenuItem";
			this.sampleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.sampleToolStripMenuItem.Text = "sample";
			// 
			// StatementCanvas
			// 
			this.Name = "StatementCanvas";
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}