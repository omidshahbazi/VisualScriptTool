// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;
using System.Drawing;

namespace VisualScriptTool.Editor
{
	public class Slot
	{
		public enum Types
		{
			EntryPoint = 0,
			Executer,
			Setter,
			Getter,
			Argument
		}

		private RectangleF bounds;
		private System.Func<Slot, bool> checkAssignment = null;
		private System.Action<Slot, Slot> onAssignment = null;
		private System.Action<Slot> onRemoveConnection = null;

		public StatementInstance StatementInstance
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			private set;
		}

		public Types Type
		{
			get;
			private set;
		}

		public uint Index
		{
			get;
			private set;
		}

		public PointF Position
		{
			get { return bounds.Location; }
			set { bounds.Location = value; }
		}

		public PointF Center
		{
			get { return new PointF(Position.X + (bounds.Width / 2.0F), Position.Y + (bounds.Height / 2.0F)); }
		}

		public SizeF Size
		{
			get { return bounds.Size; }
			set { bounds.Size = value; }
		}

		public RectangleF Bounds
		{
			get { return bounds; }
			set { bounds = value; }
		}

		public bool IsLeftAligned
		{
			get { return (Type == Types.Setter || Type == Types.EntryPoint || Type == Types.Argument); }
		}

		public bool IsRightAligned
		{
			get { return !IsLeftAligned; }
		}

		public Slot ConnectedSlot
		{
			get;
			set;
		}

		public SlotList RelatedSlots
		{
			get;
			private set;
		}

		public Slot(StatementInstance StatementInstance, string Name, Types Type, uint Index, System.Func<Slot, bool> CheckAssignment, System.Action<Slot, Slot> OnAssignment, System.Action<Slot> OnRemoveConnection)
		{
			RelatedSlots = new SlotList();

			this.StatementInstance = StatementInstance;
			this.Name = Name;
			this.Type = Type;
			this.Index = Index;
			checkAssignment = CheckAssignment;
			onAssignment = OnAssignment;
			onRemoveConnection = OnRemoveConnection;
		}

		public bool AssignConnection(Slot Slot)
		{
			if (onAssignment != null && (checkAssignment == null || checkAssignment(Slot)))
			{
				onAssignment(this, Slot);
				return true;
			}

			return false;
		}

		public void RemoveConnection()
		{
			if (onRemoveConnection != null)
				onRemoveConnection(this);
		}

		public bool IsAssignmentAllowed(Slot Slot)
		{
			if (CombinitionTypeAllowedCheck(this, Slot, Types.EntryPoint, Types.Executer))
				return true;

			if (CombinitionTypeAllowedCheck(this, Slot, Types.Argument, Types.Getter))
				return true;

			if (CombinitionTypeAllowedCheck(this, Slot, Types.Getter, Types.Setter))
				return true;

			return false;
		}

		private static bool CombinitionTypeAllowedCheck(Slot FirstSlot, Slot SecondSlot, Types FirstType, Types SecondType)
		{
			return ((FirstSlot.Type == FirstType && SecondSlot.Type == SecondType) ||
				(FirstSlot.Type == SecondType && SecondSlot.Type == FirstType));
		}
	}

	public class SlotList : List<Slot>
	{ }
}
