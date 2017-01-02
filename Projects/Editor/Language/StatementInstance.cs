// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Language.Statements;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
{
	public abstract class StatementInstance
	{
		[Serializable(0)]
		private RectangleF bounds;

		//[Serializable]
		private SlotList slots = null;

		//[Serializable]
		public Statement Statement
		{
			get;
			private set;
		}

		[Serializable(1)]
		private int Position1
		{
			get;
			set;
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

		public Slot[] Slots
		{
			get { return slots.ToArray(); }
		}

		public StatementInstance(Statement Statement, PointF Position)
		{
			bounds = new RectangleF();
			this.Statement = Statement;
			bounds.Location = Position;

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
	}

	public class StatementInstanceList : List<StatementInstance>
	{ }
}