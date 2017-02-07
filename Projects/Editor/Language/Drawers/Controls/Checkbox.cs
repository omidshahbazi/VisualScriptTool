// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;
using VisualScriptTool.Editor.Extensions;
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor.Language.Drawers.Controls
{
	class CheckBox : ControlBase
	{
		private static readonly float CHECK_RADIUS = 6.0F;

		public bool Value
		{
			get;
			set;
		}

		public override RectangleF Bounds
		{
			get { return new RectangleF(OwnerLocation.Add(Location), new SizeF(10, 10)); }
		}

		public CheckBox(StatementInstance Owner) :
			base(Owner)
		{
		}

		public override void Draw(IDevice Device)
		{
			RectangleF bounds = Bounds;

			Device.DrawFillRectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height, Brushes.DimGray);

			if (Value)
				Device.DrawFillCircle(bounds.X + 2, bounds.Y + 2, CHECK_RADIUS, Brushes.Green);
		}

		public override void OnMouseUp(MouseButtons Button, PointF Location)
		{
			base.OnMouseUp(Button, Location);

			Value = !Value;

			OnValueChanged();
		}
	}
}