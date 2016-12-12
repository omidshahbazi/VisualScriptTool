// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using System.Drawing;
using VisualScriptTool.Language.Statements;

namespace VisualScriptTool.Editor
{
	public abstract class StatementInstance
	{
		private RectangleF bounds;
		private SlotList slots = null;

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

		protected Slot AddSlot(Slot.Types Type, uint Index)
		{
			return AddSlot(string.Empty, Type, Index);
		}

		protected Slot AddSlot(string Name, Slot.Types Type, uint Index)
		{
			Slot slot = new Slot(this, Name, Type, Index);
			slots.Add(slot);
			return slot;
		}
	}

	public class StatementInstanceList : List<StatementInstance>
	{ }
}