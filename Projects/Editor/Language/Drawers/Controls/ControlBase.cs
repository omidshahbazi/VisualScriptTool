// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor.Language.Drawers.Controls
{
	public abstract class ControlBase : IMouseIntractible
	{
		public delegate void ValueChangedEventHandler(ControlBase Control);

		public StatementInstance Owner
		{
			get;
			private set;
		}

		public PointF OwnerLocation
		{
			get { return Owner.Bounds.Location; }
		}

		public PointF Location
		{
			get;
			set;
		}

		public abstract RectangleF Bounds
		{
			get;
		}

		public event ValueChangedEventHandler ValueChanged = null;

		public ControlBase(StatementInstance Owner)
		{
			this.Owner = Owner;
		}

		public abstract void Draw(IDevice Device);

		public virtual void OnMouseEnter(PointF Location)
		{
		}

		public virtual void OnMouseExit(PointF Location)
		{
		}

		public virtual void OnMouseDown(MouseButtons Button, PointF Location)
		{
		}

		public virtual void OnMouseUp(MouseButtons Button, PointF Location)
		{
		}

		public virtual void OnMouseMove(MouseButtons Button, PointF Location)
		{
		}

		protected virtual void OnValueChanged()
		{
			if (ValueChanged != null)
				ValueChanged(this);
		}
	}

	class ControlList : List<ControlBase>
	{ }
}