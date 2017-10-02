// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Editor.Extensions;
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Editor.Language.Drawers;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Renderer;

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
		private ContextMenuStrip variableContextMenu = null;
		private StatementDrawer drawer = null;
		private StatementInstanceList statements = new StatementInstanceList();
		private StatementInstanceList selectedStatements = new StatementInstanceList();
		private PointF lastMousePosition;
		private Pen selectedPen = null;
		private bool candidateToShowGeneralMenu = false;
		private CubicSPLine newConnectionLine = new CubicSPLine();
		private Slot lastMouseOverSlot = null;

		private bool isGroupSelection = false;
		private PointF startGroupSelectionLocation;
		private PointF endGroupSelectionLocation;
		private Pen groupSelectionPen = null;

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

			variableContextMenu = new ContextMenuStrip();
			variableContextMenu.Items.Add("Getter", null, (s, e) => { AddVariableStatementFropDropItem(false); });
			variableContextMenu.Items.Add("Setter", null, (s, e) => { AddVariableStatementFropDropItem(true); });

			drawer = new StatementDrawer(this);

			selectedPen = new Pen(Color.Orange, 1.5F);

			groupSelectionPen = new Pen(Color.Black);
			groupSelectionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

			//VisualScriptTool.Language.Statements.Declaration.Variables.IntegerVariable var = new VisualScriptTool.Language.Statements.Declaration.Variables.IntegerVariable();
			//var.Name = "Test_a";

			//AddStatementInstance(new VariableStatementInstance(var));

			//VisualScriptTool.Language.Statements.Declaration.Variables.VariableSetterStatement varSetter = new VisualScriptTool.Language.Statements.Declaration.Variables.VariableSetterStatement();
			//varSetter.Variable = var;

			//AddStatementInstance(new VariableSetterStatementInstance(varSetter));
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

		protected override void OnDrawCanvas(IDevice Device)
		{
			base.OnDrawCanvas(Device);

			for (int i = 0; i < statements.Count; ++i)
				drawer.Draw(Device, Statements[i]);

			for (int i = 0; i < selectedStatements.Count; ++i)
			{
				RectangleF rect = selectedStatements[i].Bounds;

				Device.DrawRectangle(rect.X, rect.Y, rect.Width, rect.Height, selectedPen);
			}

			for (int i = 0; i < statements.Count; ++i)
				drawer.DrawConections(Device, Statements[i]);

			if (SelectedSlot != null)
				newConnectionLine.Draw(Device, ControlStatementDrawer.GetPen(SelectedSlot.Type));

			if (isGroupSelection)
			{
				RectangleF rect = RectangleFExtensions.GetRectBetweenPoints(startGroupSelectionLocation, endGroupSelectionLocation);
				Device.DrawRectangle(rect.X, rect.Y, rect.Width, rect.Height, groupSelectionPen);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			OnSlotSelected(null);

			PointF location = ScreenToCanvas(e.Location);

			bool isAnythingUnderMouse = false;
			for (int i = statements.Count - 1; i >= 0; --i)
			{
				StatementInstance statement = Statements[i];

				if (statement.Bounds.Contains(location))
				{
					isAnythingUnderMouse = true;
					statement.OnMouseDown(e.Button, location);
				}
			}

			if (e.Button == MouseButtons.Right)
				candidateToShowGeneralMenu = true;
			else if (!isAnythingUnderMouse && e.Button == MouseButtons.Left)
			{
				selectedStatements.Clear();
				isGroupSelection = true;
				startGroupSelectionLocation = endGroupSelectionLocation = location;
			}

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
					statement.OnMouseUp(e.Button, location);
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
			else if (candidateToShowGeneralMenu && e.Button == MouseButtons.Right)
			{
				IsPanning = false;
				ShowGeneralMenu();
			}
			else if (isGroupSelection && e.Button == MouseButtons.Left)
				isGroupSelection = false;

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

					statement.OnMouseMove(e.Button, location);
				}
				else if (!isInArea && wasInArea)
					statement.OnMouseExit(location);
			}

			candidateToShowGeneralMenu = !IsPanning;

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
				else if (isGroupSelection)
				{
					endGroupSelectionLocation = location;

					RectangleF rect = RectangleFExtensions.GetRectBetweenPoints(startGroupSelectionLocation, endGroupSelectionLocation);

					selectedStatements.Clear();

					for (int i = statements.Count - 1; i >= 0; --i)
					{
						StatementInstance statement = Statements[i];

						if (statement.Bounds.IntersectsWith(rect))
							selectedStatements.Add(statement);
					}
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

		protected override void OnDragEnter(DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		protected override void OnDragDrop(DragEventArgs e)
		{
			ShowVariableMenu();
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

		private void ShowVariableMenu()
		{
			variableContextMenu.Show(this, ClientMousePosition);
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
				instance.OnPostLoad();
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
			if (selectedStatements.Contains(Instance))
				return;

			selectedStatements.Clear();

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

		private void AddVariableStatementFropDropItem(bool IsSetter)
		{
			StatementInstance instance = null;

			VisualScriptTool.Language.Statements.Declaration.Variables.VariableStatement statement = (VisualScriptTool.Language.Statements.Declaration.Variables.VariableStatement)DragAndDropManager.GetData();

			if (IsSetter)
			{
				VisualScriptTool.Language.Statements.Declaration.Variables.VariableSetterStatement setterStatement = new VisualScriptTool.Language.Statements.Declaration.Variables.VariableSetterStatement();
				setterStatement.Variable = statement;
				instance = new VariableSetterStatementInstance(setterStatement);
			}
			else
				instance = new VariableStatementInstance(statement);

			instance.Position = ScreenToCanvas(PointToClient(MousePosition));
			AddStatementInstance(instance);

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