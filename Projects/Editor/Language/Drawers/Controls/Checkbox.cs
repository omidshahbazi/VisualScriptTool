// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Renderer;

namespace VisualScriptTool.Editor.Language.Drawers.Controls
{
	class Checkbox : ControlBase
	{
		public override void Draw(IDevice Device)
		{
			Device.DrawFillRectangle(slot.Bounds.Left + size.Width + 30, slot.Position.Y, 10, 10, System.Drawing.Brushes.DimGray);

			Device.DrawFillCircle(slot.Bounds.Left + size.Width + 32, slot.Position.Y + 2, 6, System.Drawing.Brushes.Green);
		}
	}
}