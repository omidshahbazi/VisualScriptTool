﻿// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Editor.Language.Drawers;
using VisualScriptTool.Language.Statements;

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

		private static readonly Item[] ITEMS = new Item[] {
			new Item("Execter", (Position)=>
			{
				ExecuterStatementInstance statement = new ExecuterStatementInstance();
				statement.Position = Position;
				return statement;
			}),
			new Item("If", (Position)=>
			{
				IfStatementInstance statement = new IfStatementInstance();
				statement.Position = Position;
				return statement;
			}),
			new Item("For", (Position) =>
			{
				ForStatementInstance statement = new ForStatementInstance();
				statement.Position = Position;
				return statement;
			}),
			new Item("While", (Position) =>
			{
				WhileStatementInstance statement = new WhileStatementInstance();
				statement.Position = Position;
				return statement;
			}) };

		private ContextMenuStrip generalContextMenu = null;
		private ContextMenuStrip slotContextMenu = null;
		private StatementDrawer drawer = null;
		private StatementInstanceList statements = new StatementInstanceList();
		private StatementInstanceList selectedStatements = new StatementInstanceList();
		private StatementInstanceList candidateToSelectStatements = new StatementInstanceList();
		private PointF lastMousePosition;
		private Pen selectedPen = null;
		private bool candidateToShowGeneralMenu = false;
		private CubicSPLine newConnectionLine = new CubicSPLine();
		private Slot lastMouseOverSlot = null;

		protected Point ClientMousePosition
		{
			get { return PointToClient(MousePosition); }
		}

		public StatementInstance[] Statements
		{
			get { return statements.ToArray(); }
		}

		public StatementInstance[] SelectedStatements
		{
			get { return selectedStatements.ToArray(); }
		}

		public Slot MouseOverSlot
		{
			get;
			private set;
		}

		public Slot SelectedSlot
		{
			get;
			private set;
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

			selectedPen = new Pen(Color.Orange, 1.5F);
		}

		public void AddStatementInstance(StatementInstance Instance)
		{
			Instance.StatementInstanceSelected += OnStatementInstanceSelected;
			Instance.SlotSelected += OnSlotSelected;
			Instance.SlotOver += OnSlotOver;
			Instance.SlotExit += OnSlotExit;
			statements.Add(Instance);
		}

		public void AddStatementInstance(IEnumerable<StatementInstance> Instances)
		{
			IEnumerator<StatementInstance> it = Instances.GetEnumerator();
			while (it.MoveNext())
				AddStatementInstance(it.Current);
		}

		protected override void OnDrawCanvas(Graphics Graphics)
		{
			base.OnDrawCanvas(Graphics);

			for (int i = 0; i < statements.Count; ++i)
				drawer.Draw(Graphics, Statements[i]);

			for (int i = 0; i < selectedStatements.Count; ++i)
			{
				RectangleF rect = selectedStatements[i].Bounds;

				Graphics.DrawRectangle(selectedPen, rect.X, rect.Y, rect.Width, rect.Height);
			}

			for (int i = 0; i < statements.Count; ++i)
				drawer.DrawConections(Graphics, Statements[i]);

			if (SelectedSlot != null)
				newConnectionLine.Draw(Graphics, ControlStatementDrawer.GetPen(SelectedSlot.Type));
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			OnStatementInstanceSelected(null);
			OnSlotSelected(null);

			PointF location = ScreenToCanvas(e.Location);

			for (int i = statements.Count - 1; i >= 0; --i)
			{
				StatementInstance statement = Statements[i];

				if (statement.Bounds.Contains(location))
				{
					candidateToSelectStatements.Add(statement);
					statement.OnMouseDown(e.Button, location);
				}
			}

			if (e.Button == MouseButtons.Right)
				candidateToShowGeneralMenu = true;

			if (e.Button == MouseButtons.Middle)
				return;

			selectedStatements.Clear();

			lastMousePosition = ScreenToCanvas(e.Location);

			Refresh();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			PointF location = ScreenToCanvas(e.Location);

			for (int i = statements.Count - 1; i >= 0; --i)
			{
				StatementInstance statement = Statements[i];

				if (statement.Bounds.Contains(location))
				{
					candidateToSelectStatements.Add(statement);
					statement.OnMouseUp(e.Button, location);
				}
			}

			if (SelectedSlot != null)
			{
				if (e.Button == MouseButtons.Right)
				{
					ShowSlotMenu();
				}
				else
				{
					if (MouseOverSlot == null)
					{
						if (lastMouseOverSlot == null || lastMouseOverSlot == SelectedSlot)
							ShowGeneralMenu();
						else
							newConnectionLine.Clear();
					}
					else
					{
						if (SelectedSlot.IsAssignmentAllowed(MouseOverSlot))
						{
							MouseOverSlot.AssignConnection(SelectedSlot);
							SelectedSlot.AssignConnection(MouseOverSlot);
						}

						newConnectionLine.Clear();
					}
				}
			}
			else if (e.Button == MouseButtons.Right && candidateToShowGeneralMenu)
			{
				IsPanning = false;
				ShowGeneralMenu();
			}

			if (candidateToSelectStatements.Count != 0)
			{
				selectedStatements.Clear();
				selectedStatements.AddRange(candidateToSelectStatements);
				candidateToSelectStatements.Clear();
			}

			Refresh();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			PointF location = ScreenToCanvas(e.Location);

			for (int i = statements.Count - 1; i >= 0; --i)
			{
				StatementInstance statement = Statements[i];
				bool wasInArea = statement.Bounds.Contains(lastMousePosition);
				bool isInArea = statement.Bounds.Contains(location);

				if (isInArea)
				{
					if (!wasInArea)
						statement.OnMouseEnter(location);

					candidateToSelectStatements.Add(statement);
					statement.OnMouseMove(e.Button, location);
				}
				else if (!isInArea && wasInArea)
					statement.OnMouseExit(location);
			}

			candidateToShowGeneralMenu = !IsPanning;

			if (candidateToSelectStatements.Count != 0)
			{
				selectedStatements.AddRange(candidateToSelectStatements);
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
				else if (selectedStatements.Count != 0)
				{
					PointF delta = location.Subtract(lastMousePosition);

					for (int i = 0; i < selectedStatements.Count; ++i)
					{
						StatementInstance statement = SelectedStatements[i];

						statement.Position = statement.Position.Add(delta);
					}

					lastMousePosition = location;
				}

				Refresh();
			}

			lastMousePosition = location;
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (e.KeyCode == Keys.Delete)
			{
				for (int i = 0; i < selectedStatements.Count; ++i)
					statements.Remove(SelectedStatements[i]);

				selectedStatements.Clear();

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

			//if (SelectedSlot.Type == Slot.Types.Argument || SelectedSlot.Type == Slot.Types.Executer || SelectedSlot.Type == Slot.Types.Setter)
			//	slotContextMenu.Items.Add("Remove Connection", null, (s, e) => { OnRemoveConnection(SelectedSlot); });
			//else
			//	slotContextMenu.Items.Add("Remove All Connections", null, (s, e) => { OnRemoveAllConnections(SelectedSlot); });

			slotContextMenu.Show(this, ClientMousePosition);
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
				AddStatementInstance(instance);

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

		private void OnStatementInstanceSelected(StatementInstance Instance)
		{
			selectedStatements.Add(Instance);
		}

		private void OnSlotSelected(Slot Slot)
		{
			SelectedSlot = Slot;
		}

		private void OnSlotOver(Slot Slot)
		{
			MouseOverSlot = Slot;
		}

		private void OnSlotExit(Slot Slot)
		{
			lastMouseOverSlot = Slot;
			MouseOverSlot = null;
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
			for (int i = 0; i < Statements.Length; ++i)
			{
				if (Statements[i].Statement == Statement)
					return Statements[i];
			}

			return null;
		}
	}
}