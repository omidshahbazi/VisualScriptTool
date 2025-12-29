// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using VisualScriptTool.Editor.Extensions;
using VisualScriptTool.Editor.Language;
using VisualScriptTool.Editor.Language.Drawers;
using VisualScriptTool.Language.Extensions;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Language.Statements.Declaration;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor
{
	public class StatementCanvas : GridCanvas, IStatementInspector
	{
		public delegate void VariableStatementChanged(UserDefinedStatement Instance);
		public delegate void VariableStatementsChanged();
		public delegate void StatementInstanceChanged(StatementInstance Instance);
		public delegate void StatementInstancesChanged();

		private class MenuItemInfo
		{
			private Func<PointF, object> instantiator = null;

			public string Title
			{
				get;
				private set;
			}

			public MenuItemInfo(string Title, Func<PointF, object> Instantiator)
			{
				this.Title = Title;
				instantiator = Instantiator;
			}

			public object Instantiate(PointF Position)
			{
				return instantiator(Position);
			}
		}

		private static readonly Type[] ROOT_MENU_ITEM_TYPES = new Type[] {
		};

		private static readonly Type[] FLOW_MENU_ITEM_TYPES = new Type[] {
			typeof(EntrypointStatementInstance),
			typeof(IfStatementInstance),
			typeof(ForStatementInstance),
			typeof(WhileStatementInstance)
		};

		private ContextMenuStrip generalContextMenu = null;
		private ToolStripMenuItem flowToolStripMenuItem = null;
		private ToolStripMenuItem functionsToolStripMenuItem = null;
		private ContextMenuStrip slotContextMenu = null;
		private ContextMenuStrip variableContextMenu = null;
		private StatementDrawer drawer = null;
		private VariableStatementList variableStatements = new VariableStatementList();
		private StatementInstanceList statementInstances = new StatementInstanceList();
		private StatementInstanceList selectedStatementInstances = new StatementInstanceList();
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

		public VariableStatement[] VariableStatements
		{
			get { return variableStatements.ToArray(); }
		}

		public StatementInstance[] StatementInstances
		{
			get { return statementInstances.ToArray(); }
		}

		public StatementInstance[] SelectedStatementInstances
		{
			get { return selectedStatementInstances.ToArray(); }
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

		public event VariableStatementChanged OnVariableStatementAdded;
		public event VariableStatementChanged OnVariableStatementRemoved;
		public event VariableStatementsChanged OnVariableStatementsChanged;

		public event StatementInstanceChanged OnStatementInstanceAdded;
		public event StatementInstanceChanged OnStatementInstanceRemoved;
		public event StatementInstancesChanged OnStatementInstancesChanged;

		public StatementCanvas()
		{
			generalContextMenu = new ContextMenuStrip();
			{
				generalContextMenu.Closed += new ToolStripDropDownClosedEventHandler(OnContextMenuClosed);

				flowToolStripMenuItem = CreateAndFillMenuItems("Flow", FLOW_MENU_ITEM_TYPES);

				MenuItemInfo functionItem = new MenuItemInfo("Function", (Position) =>
				{
					FunctionStatementInstance statement = new FunctionStatementInstance();
					statement.Statement.Name = "NewFunction";
					statement.Position = Position;
					return statement;
				});

				AddMenuItems(generalContextMenu.Items, functionItem);

				FillMenuItems(generalContextMenu.Items, ROOT_MENU_ITEM_TYPES);

				functionsToolStripMenuItem = new ToolStripMenuItem("Functions");
				{
					generalContextMenu.Items.Add(functionsToolStripMenuItem);
				}
			}

			slotContextMenu = new ContextMenuStrip();

			variableContextMenu = new ContextMenuStrip();
			{
				variableContextMenu.Items.Add("Getter", null, (s, e) => { AddVariableStatementForDropItem(false); });
				variableContextMenu.Items.Add("Setter", null, (s, e) => { AddVariableStatementForDropItem(true); });
			}

			drawer = new StatementDrawer(this);

			selectedPen = new Pen(Color.Orange, 1.5F);

			groupSelectionPen = new Pen(Color.Black);
			groupSelectionPen.DashStyle = DashStyle.Dash;
		}

		public void RemoveStatement(Statement Statement)
		{
			if (Statement is VariableStatement)
			{
				RemoveStatementInstancesByStatement(Statement);

				RemoveVariableStatement((VariableStatement)Statement);
			}
			else
				throw new NotImplementedException();

			Refresh();
		}

		public void AddVariableStatement(VariableStatement Statement)
		{
			variableStatements.Add(Statement);

			OnVariableStatementAdded?.Invoke(Statement);
			OnVariableStatementsChanged?.Invoke();
		}

		public void AddVariableStatement(IEnumerable<VariableStatement> Statements)
		{
			IEnumerator<VariableStatement> it = Statements.GetEnumerator();
			while (it.MoveNext())
				AddVariableStatement(it.Current);
		}

		public void RemoveVariableStatement(VariableStatement Statement)
		{
			variableStatements.Remove(Statement);

			OnVariableStatementRemoved?.Invoke(Statement);
			OnVariableStatementsChanged?.Invoke();
		}

		public void AddStatementInstance(StatementInstance Instance)
		{
			Instance.OnStatementInstanceSelected += OnStatementInstanceSelected;
			Instance.OnSlotSelected += OnSlotSelected;
			Instance.OnSlotOver += OnSlotOver;
			Instance.OnSlotExit += OnSlotExit;
			statementInstances.Add(Instance);

			OnStatementInstanceAdded?.Invoke(Instance);
			OnStatementInstancesChanged?.Invoke();

			Refresh();
		}

		public void AddStatementInstance(IEnumerable<StatementInstance> Instances)
		{
			IEnumerator<StatementInstance> it = Instances.GetEnumerator();
			while (it.MoveNext())
				AddStatementInstance(it.Current);
		}

		public void RemoveStatementInstance(StatementInstance Instance)
		{
			FreeStatementInstance(Instance);

			Refresh();
		}

		public void BindClassFunctions(Type Type)
		{
			MethodInfo[] methods = Type.GetMethods(BindingFlags.Static | BindingFlags.Public);

			for (int i = 0; i < methods.Length; ++i)
			{
				MethodInfo method = methods[i];

				MenuItemInfo item = new MenuItemInfo(method.GetPrettyName(), (Position) =>
				{
					FunctionCallStatement fnstmt = new FunctionCallStatement();
					fnstmt.Method = method;
					FunctionCallStatementInstance statement = new FunctionCallStatementInstance();
					statement.Statement = fnstmt;
					statement.Position = Position;
					return statement;
				});

				functionsToolStripMenuItem.DropDownItems.Add(item.Title, null, (s, e) => { OnItemClicked(item); });
			}
		}

		public void ResetView()
		{
			RectangleF bounds = StatementInstances.GetBounds();

			LookAt(bounds);

			Refresh();
		}

		protected override void OnDrawCanvas(IDevice Device)
		{
			base.OnDrawCanvas(Device);

			for (int i = 0; i < statementInstances.Count; ++i)
				drawer.Draw(Device, StatementInstances[i]);

			for (int i = 0; i < selectedStatementInstances.Count; ++i)
			{
				RectangleF rect = selectedStatementInstances[i].Bounds;

				Device.DrawRectangle(rect.X, rect.Y, rect.Width, rect.Height, selectedPen);
			}

			for (int i = 0; i < statementInstances.Count; ++i)
				drawer.DrawConections(Device, StatementInstances[i]);

			if (SelectedSlot != null)
				newConnectionLine.Draw(Device, Drawer.GetPen(SelectedSlot.Type));

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
			for (int i = statementInstances.Count - 1; i >= 0; --i)
			{
				StatementInstance statement = StatementInstances[i];

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
				selectedStatementInstances.Clear();
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

			for (int i = statementInstances.Count - 1; i >= 0; --i)
			{
				StatementInstance statement = StatementInstances[i];

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

			for (int i = statementInstances.Count - 1; i >= 0; --i)
			{
				StatementInstance statement = StatementInstances[i];
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
						startOffset.X = -Drawer.LINE_START_OFFSET_AMOUNT;
						endOffset.X = Drawer.LINE_START_OFFSET_AMOUNT;
					}
					else
					{
						startOffset.X = Drawer.LINE_START_OFFSET_AMOUNT;
						endOffset.X = -Drawer.LINE_START_OFFSET_AMOUNT;
					}

					newConnectionLine.Update(SelectedSlot.Center, startOffset, location, endOffset);
				}
				else if (isGroupSelection)
				{
					endGroupSelectionLocation = location;

					RectangleF rect = RectangleFExtensions.GetRectBetweenPoints(startGroupSelectionLocation, endGroupSelectionLocation);

					selectedStatementInstances.Clear();

					for (int i = statementInstances.Count - 1; i >= 0; --i)
					{
						StatementInstance statement = StatementInstances[i];

						if (statement.Bounds.IntersectsWith(rect))
							selectedStatementInstances.Add(statement);
					}
				}
				else if (selectedStatementInstances.Count != 0)
				{
					PointF delta = location.Subtract(lastMousePosition);

					for (int i = 0; i < selectedStatementInstances.Count; ++i)
					{
						StatementInstance statement = SelectedStatementInstances[i];

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
				for (int i = 0; i < selectedStatementInstances.Count; ++i)
				{
					FreeStatementInstance(selectedStatementInstances[i--]);
				}

				selectedStatementInstances.Clear();

				Refresh();
			}
			else if (e.KeyCode == Keys.F)
			{
				ResetView();
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

		private void RemoveStatementInstancesByStatement(Statement Instance)
		{
			StatementInstance[] statementInstances = ((IStatementInspector)this).GetInstances(Instance);

			for (int i = 0; i < statementInstances.Length; ++i)
			{
				FreeStatementInstance(statementInstances[i]);
			}
		}

		private void FreeStatementInstance(StatementInstance Instance)
		{
			Instance.OnStatementInstanceSelected -= OnStatementInstanceSelected;
			Instance.OnSlotSelected -= OnSlotSelected;
			Instance.OnSlotOver -= OnSlotOver;
			Instance.OnSlotExit -= OnSlotExit;

			Instance.RemoveConnections();

			statementInstances.Remove(Instance);
			RemoveFromSelected(Instance);

			OnStatementInstanceRemoved?.Invoke(Instance);
			OnStatementInstancesChanged?.Invoke();
		}

		private void RemoveFromSelected(StatementInstance StatementInstance)
		{
			selectedStatementInstances.Remove(StatementInstance);
		}

		private ToolStripMenuItem CreateAndFillMenuItems(string Title, params Type[] Types)
		{
			ToolStripMenuItem menuItem = new ToolStripMenuItem(Title);

			generalContextMenu.Items.Add(menuItem);

			FillMenuItems(menuItem.DropDownItems, Types);

			return menuItem;
		}

		private void FillMenuItems(ToolStripItemCollection Collection, params Type[] Types)
		{
			for (int i = 0; i < Types.Length; ++i)
			{
				Type type = Types[i];

				MenuItemInfo item = new MenuItemInfo(type.Name.Replace(typeof(StatementInstance).Name, string.Empty), (Position) =>
				{
					StatementInstance statement = (StatementInstance)Activator.CreateInstance(type);
					statement.Position = Position;
					return statement;
				});

				AddMenuItems(Collection, item);

			}
		}

		private void AddMenuItems(ToolStripItemCollection Collection, params MenuItemInfo[] Items)
		{
			for (int i = 0; i < Items.Length; ++i)
			{
				MenuItemInfo item = Items[i];

				Collection.Add(item.Title, null, (s, e) => { OnItemClicked(item); });
			}
		}

		private void ShowGeneralMenu()
		{
			flowToolStripMenuItem.DropDownItems[0].Enabled = (StatementInstances.Find<EntrypointStatementInstance>() == null);

			generalContextMenu.Show(this, ClientMousePosition);
		}

		private void ShowSlotMenu()
		{
			slotContextMenu.Items.Clear();

			if (SelectedSlot.Type == Slot.Types.Argument || SelectedSlot.Type == Slot.Types.Executer)
				slotContextMenu.Items.Add("Remove Connection", null, (s, e) => { RemoveConnection(SelectedSlot); });
			else
				slotContextMenu.Items.Add("Remove All Connections", null, (s, e) => { RemoveAllConnections(SelectedSlot); });

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

		private void OnItemClicked(MenuItemInfo Item)
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
			if (selectedStatementInstances.Contains(Instance))
				return;

			selectedStatementInstances.Clear();

			selectedStatementInstances.Add(Instance);
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

		private void RemoveConnection(Slot Slot)
		{
			Slot.RemoveConnection();

			Refresh();
		}

		private void RemoveAllConnections(Slot Slot)
		{
			Slot[] relatedSlots = Slot.RelatedSlots.ToArray();

			for (int i = 0; i < relatedSlots.Length; ++i)
				relatedSlots[i].RemoveConnection();

			Refresh();
		}

		private void AddVariableStatementForDropItem(bool IsSetter)
		{
			StatementInstance instance = null;

			VariableStatement statement = (VariableStatement)DragAndDropManager.GetData();

			if (IsSetter)
			{
				VisualScriptTool.Language.Statements.Control.VariableSetterStatement setterStatement = new VisualScriptTool.Language.Statements.Control.VariableSetterStatement();
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
			for (int i = 0; i < StatementInstances.Length; ++i)
			{
				if (StatementInstances[i].Statement == Statement)
					return StatementInstances[i];
			}

			return null;
		}

		StatementInstance[] IStatementInspector.GetInstances(Statement Statement)
		{
			StatementInstanceList statementInstances = new StatementInstanceList();

			for (int i = 0; i < StatementInstances.Length; ++i)
			{
				if (StatementInstances[i].Statement == Statement)
					statementInstances.Add(StatementInstances[i]);
			}

			return statementInstances.ToArray();
		}
	}
}