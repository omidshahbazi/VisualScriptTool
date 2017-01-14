// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
{
	public abstract class StatementInstance
	{
		private RectangleF bounds;

		public SlotList slots = null;

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

		public StatementInstance(Statement Statement)
		{
			bounds = new RectangleF();
			this.Statement = Statement;

			slots = new SlotList();
		}

		public void UpdateBounds()
		{
			bounds.Size = new SizeF(HeaderSize.Width, HeaderSize.Height + BodySize.Height);
		}

		protected Slot AddSlot(Slot.Types Type, uint Index, System.Func<Slot, bool> CheckAssignment = null, System.Action<Slot, Slot> OnAssignment = null)
		{
			return AddSlot(string.Empty, Type, Index, CheckAssignment, OnAssignment);
		}

		protected Slot AddSlot(string Name, Slot.Types Type, uint Index, System.Func<Slot, bool> CheckAssignment = null, System.Action<Slot, Slot> OnAssignment = null)
		{
			Slot slot = new Slot(this, Name, Type, Index, CheckAssignment, OnAssignment);
			slots.Add(slot);
			return slot;
		}

		protected virtual void UpdateConnectedSlot(IStatementInspector Inspector, uint Index, Statement ConnectedStatement)
		{
			if (ConnectedStatement == null)
				return;

			Slot slot = slots[(int)Index];

			StatementInstance instance = Inspector.GetInstance(ConnectedStatement);

			if (instance != null)
				for (int i = 0; i < instance.slots.Count; ++i)
					if (slot.IsAssignmentAllowed(instance.slots[i]))
						slot.ConnectedSlot = instance.slots[i];
						return;
		}

		public virtual void ResolveSlotConnections(IStatementInspector Inspector)
		{
		}
	}

	public class StatementInstanceList : List<StatementInstance>
	{ }
}