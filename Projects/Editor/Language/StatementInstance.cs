// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Editor.Extensions;
using VisualScriptTool.Editor.Language.Drawers.Controls;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor.Language
{
	public abstract class StatementInstance : IMouseIntractible
	{
		public delegate void StatementInstanceSelectedHanlder(StatementInstance Instance);
		public delegate void SlotHandler(Slot Slot);

		private const float SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT = 20.0F;
		private const float HALF_SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT = SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT / 2.0F;

		private RectangleF bounds;

		private SlotList slots = null;
		private ControlList controls = null;

		private bool isSelected = false;
		private Slot lastSlotOver = null;

		public event StatementInstanceSelectedHanlder StatementInstanceSelected = null;
		public event SlotHandler SlotSelected = null;
		public event SlotHandler SlotOver = null;
		public event SlotHandler SlotExit = null;

		[SerializableElement(3)]
		public Statement Statement
		{
			get;
			set;
		}

		[SerializableElement(0)]
		public PointF Position
		{
			get { return bounds.Location; }
			set { bounds.Location = value; }
		}

		[SerializableElement(1)]
		public SizeF HeaderSize
		{
			get;
			set;
		}

		[SerializableElement(2)]
		public SizeF BodySize
		{
			get;
			set;
		}

		public RectangleF Bounds
		{
			get { return bounds; }
		}

		public Slot[] Slots
		{
			get { return slots.ToArray(); }
		}

		public ControlBase[] Controls
		{
			get { return controls.ToArray(); }
		}

		public StatementInstance(Statement Statement)
		{
			bounds = new RectangleF();
			this.Statement = Statement;

			slots = new SlotList();
			controls = new ControlList();
		}

		[SerializablePostDeserialize]
		public virtual void OnPostLoad()
		{
		}

		public void UpdateBounds()
		{
			bounds.Size = new SizeF(HeaderSize.Width, HeaderSize.Height + BodySize.Height);
		}

		protected Slot AddEntryPointSlot(uint Index, System.Func<Slot, bool> CheckAssignment = null, System.Action<Slot, Slot> OnAssignment = null, System.Action<Slot> OnRemoveConnection = null)
		{
			return AddSlot(string.Empty, Slot.Types.EntryPoint, Index, CheckAssignment, OnAssignment, OnRemoveConnection);
		}

		protected Slot AddExecuterSlot(uint Index, System.Func<Slot, bool> CheckAssignment = null, System.Action<Slot, Slot> OnAssignment = null, System.Action<Slot> OnRemoveConnection = null)
		{
			return AddExecuterSlot(string.Empty, Index, CheckAssignment, OnAssignment, OnRemoveConnection);
		}

		protected Slot AddExecuterSlot(string Name, uint Index, System.Func<Slot, bool> CheckAssignment = null, System.Action<Slot, Slot> OnAssignment = null, System.Action<Slot> OnRemoveConnection = null)
		{
			return AddSlot(Name, Slot.Types.Executer, Index, CheckAssignment, OnAssignment, OnRemoveConnection);
		}

		protected Slot AddArgumentSlot(uint Index, System.Func<Slot, bool> CheckAssignment = null, System.Action<Slot, Slot> OnAssignment = null, System.Action<Slot> OnRemoveConnection = null)
		{
			return AddArgumentSlot(string.Empty, Index, CheckAssignment, OnAssignment, OnRemoveConnection);
		}

		protected Slot AddArgumentSlot(string Name, uint Index, System.Func<Slot, bool> CheckAssignment = null, System.Action<Slot, Slot> OnAssignment = null, System.Action<Slot> OnRemoveConnection = null)
		{
			return AddSlot(Name, Slot.Types.Argument, Index, CheckAssignment, OnAssignment, OnRemoveConnection);
		}

		protected Slot AddGetterSlot(uint Index, System.Func<Slot, bool> CheckAssignment = null, System.Action<Slot, Slot> OnAssignment = null, System.Action<Slot> OnRemoveConnection = null)
		{
			return AddSlot(string.Empty, Slot.Types.Getter, Index, CheckAssignment, OnAssignment, OnRemoveConnection);
		}

		protected void RemoveSlot(Slot Slot)
		{
			slots.Remove(Slot);
		}

		protected void AddControl(ControlBase Control)
		{
			controls.Add(Control);
		}

		protected virtual void UpdateConnectedSlot(IStatementInspector Inspector, uint Index, Statement ConnectedStatement)
		{
			if (ConnectedStatement == null)
				return;

			Slot slot = slots[(int)Index];

			StatementInstance instance = Inspector.GetInstance(ConnectedStatement);

			if (instance != null)
				for (int i = 0; i < instance.slots.Count; ++i)
				{
					Slot otherSlot = instance.slots[i];

					if (slot.IsAssignmentAllowed(otherSlot))
					{
						SetConnection(slot, otherSlot);
						return;
					}
				}
		}

		protected void SetConnection(Slot From, Slot To)
		{
			From.ConnectedSlot = To;

			To.RelatedSlots.Add(From);
		}

		protected void UnsetConnection(Slot Slot)
		{
			if (Slot.ConnectedSlot != null)
				Slot.ConnectedSlot.RelatedSlots.Remove(Slot);

			Slot.ConnectedSlot = null;
		}

		public virtual void ResolveSlotConnections(IStatementInspector Inspector)
		{
		}

		public virtual void OnMouseEnter(PointF Location)
		{

		}

		public virtual void OnMouseExit(PointF Location)
		{
			if (lastSlotOver != null)
			{
				lastSlotOver = null;
				OnSlotExit(lastSlotOver);
			}
		}

		public virtual void OnMouseDown(MouseButtons Button, PointF Location)
		{
			OnStatementInstanceSelectedd(this);

			Slot underMouse = GetSlotAtLocation(Location);
			if (underMouse != null)
				OnSlotSelected(underMouse);
		}

		public virtual void OnMouseUp(MouseButtons Button, PointF Location)
		{
			for (int i = 0; i < controls.Count;++i)
			{
				ControlBase control = controls[i];

				if (control.Bounds.Contains(Location))
					control.OnMouseUp(Button, Location);
			}
		}

		public virtual void OnMouseMove(MouseButtons Button, PointF Location)
		{
			Slot underMouse = GetSlotAtLocation(Location);

			if (underMouse != lastSlotOver)
			{
				if (lastSlotOver != null)
				{
					OnSlotExit(lastSlotOver);
					lastSlotOver = null;
				}

				if (underMouse != null)
					OnSlotOver(underMouse);

				lastSlotOver = underMouse;
			}
		}

		private Slot AddSlot(string Name, Slot.Types Type, uint Index, System.Func<Slot, bool> CheckAssignment = null, System.Action<Slot, Slot> OnAssignment = null, System.Action<Slot> OnRemoveConnection = null)
		{
			Slot slot = new Slot(this, Name, Type, Index, CheckAssignment, OnAssignment, OnRemoveConnection);
			slots.Add(slot);
			return slot;
		}

		private Slot GetSlotAtLocation(PointF Location)
		{
			for (uint j = 0; j < Slots.Length; ++j)
			{
				Slot slot = Slots[j];

				RectangleF bounds = slot.Bounds;

				bounds.Location.Add(new PointF(-HALF_SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT, -HALF_SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT));
				bounds.Inflate(SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT, SLOT_SELECTION_RECTANGLE_ENLARGE_AMOUNT);

				if (bounds.Contains(Location))
					return slot;
			}

			return null;
		}

		protected virtual void OnStatementInstanceSelectedd(StatementInstance Statement)
		{
			if (StatementInstanceSelected != null)
				StatementInstanceSelected(Statement);
		}

		protected virtual void OnSlotSelected(Slot Slot)
		{
			if (SlotSelected != null)
				SlotSelected(Slot);
		}

		protected virtual void OnSlotOver(Slot Slot)
		{
			if (SlotOver != null)
				SlotOver(Slot);
		}

		protected virtual void OnSlotExit(Slot Slot)
		{
			if (SlotExit != null)
				SlotExit(Slot);
		}
	}

	public class StatementInstanceList : List<StatementInstance>
	{ }
}