// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Editor.Language.Drawers;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;

namespace VisualScriptTool.Editor
{
	public class StatementCanvas : GridCanvas, IStatementInspector
	{
		private class Item
		{
			private Func<PointF, object> instantiator = null;

			public string Title
			{
				get;
				private set;
			}

			public Item(string Title, Func<PointF, object> Instantiator)
			{
				this.Title = Title;
				instantiator = Instantiator;
			}

			public object Instantiate(PointF Position)
			{
				return instantiator(Position);
			}
		}

		private const float SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT = 20.0F;
		private const float HALF_SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT = SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT / 2.0F;
		private static readonly Item[] ITEMS = new Item[] {
			new Item("If", (Position)=>
			{
				IfStatementInstance statement = new IfStatementInstance(new IfStatement());
				statement.Position = Position;
				return statement;
			}),
			new Item("For", (Position) =>
			{
				ForStatementInstance statement = new ForStatementInstance(new ForStatement());
				statement.Position = Position;
				return statement;
			}) };

		private ContextMenuStrip generalContextMenu = null;
		private ContextMenuStrip slotContextMenu = null;
		private StatementDrawer drawer = null;
		private StatementInstanceList candidateToSelectStatements = new StatementInstanceList();
		private PointF lastMousePosition;
		private Pen selectedPen = null;
		private CubicSPLine newConnectionLine = new CubicSPLine();

		protected Point ClientMousePosition
		{
			get { return PointToClient(MousePosition); }
		}

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
			generalContextMenu = new ContextMenuStrip();
			generalContextMenu.Closed += new ToolStripDropDownClosedEventHandler(OnContextMenuClosed);
			for (int i = 0; i < ITEMS.Length; ++i)
			{
				Item item = ITEMS[i];
				generalContextMenu.Items.Add(item.Title, null, (s, e) => { OnItemClicked(item); });
			}

			slotContextMenu = new ContextMenuStrip();

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
				newConnectionLine.Draw(Graphics, ControlStatementDrawer.GetPen(SelectedSlot.Type));
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
			else
			{

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
				if (e.Button == MouseButtons.Right)
					ShowSlotMenu();
				else
				{
					if (MouseOverSlot == null)
					{
						Slot mouseHoverSlot = GetSlotAtLocation(ScreenToCanvas(e.Location));
						if (mouseHoverSlot == null || mouseHoverSlot == SelectedSlot)
							ShowGeneralMenu();
						else
							newConnectionLine.Clear();
					}
					else
					{
						MouseOverSlot.AssignConnection(SelectedSlot);
						SelectedSlot.AssignConnection(MouseOverSlot);

						newConnectionLine.Clear();
					}
				}
			}
			else if (e.Button == MouseButtons.Right)
				ShowGeneralMenu();

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

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (e.KeyCode == Keys.Delete)
			{
				for (int i = 0; i < SelectedStatements.Count; ++i)
					Statements.Remove(SelectedStatements[i]);

				SelectedStatements.Clear();

				Refresh();
			}
		}

		private void ShowGeneralMenu()
		{
			generalContextMenu.Show(this, ClientMousePosition);
		}

		private void ShowSlotMenu()
		{
			slotContextMenu.Items.Clear();

			if (SelectedSlot.Type == Slot.Types.Argument || SelectedSlot.Type == Slot.Types.Executer || SelectedSlot.Type == Slot.Types.Setter)
				slotContextMenu.Items.Add("Remove Connection", null, (s, e) => { OnRemoveConnection(SelectedSlot); });
			else
				slotContextMenu.Items.Add("Remove All Connections", null, (s, e) => { OnRemoveAllConnections(SelectedSlot); });

			slotContextMenu.Show(this, ClientMousePosition);
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

		private void OnContextMenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			newConnectionLine.Clear();

			Refresh();
		}

		private void OnItemClicked(Item Item)
		{
			PointF location = ScreenToCanvas(PointToClient(MousePosition));

			object obj = Item.Instantiate(location);

			if (obj is StatementInstance)
			{
				StatementInstance instance = (StatementInstance)obj;
				Statements.Add(instance);

				if (SelectedSlot != null)
				{
					Slot[] slots = instance.Slots;
					for (int i = 0; i < slots.Length; ++i)
					{
						Slot otherSlot = slots[i];

						if (SelectedSlot.IsAssignmentAllowed(otherSlot))
						{
							if (!SelectedSlot.AssignConnection(otherSlot))
								otherSlot.AssignConnection(SelectedSlot);

							break;
						}
					}
				}
			}
		}

		private void OnRemoveConnection(Slot Slot)
		{
			Slot.RemoveConnection();

			Refresh();
		}

		private void OnRemoveAllConnections(Slot Slot)
		{
			Slot[] relatedSlots = Slot.RelatedSlots.ToArray();

			for (int i = 0; i < relatedSlots.Length; ++i)
				relatedSlots[i].RemoveConnection();

			Refresh();
		}

		StatementInstance IStatementInspector.GetInstance(Statement Statement)
		{
			for (int i = 0; i < Statements.Count; ++i)
			{
				if (Statements[i].Statement == Statement)
					return Statements[i];
			}

			return null;
		}
	}
}