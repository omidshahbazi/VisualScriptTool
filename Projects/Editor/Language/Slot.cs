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
			Executer = 0,
			Setter,
			Getter
		}

		private RectangleF bounds;

		public StatementInstance StatementInstance
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

		public Slot(StatementInstance StatementInstance, Types Type, uint Index)
		{
			this.StatementInstance = StatementInstance;
			this.Type = Type;
			this.Index = Index;
		}
	}

	public class SlotList : List<Slot>
	{ }
}
