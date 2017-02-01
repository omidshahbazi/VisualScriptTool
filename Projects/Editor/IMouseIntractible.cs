// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;
using System.Windows.Forms;

namespace VisualScriptTool.Editor
{
	interface IMouseIntractible
	{
		void OnMouseEnter(PointF Location);
		void OnMouseExit(PointF Location);

		void OnMouseDown(MouseButtons Button, PointF Location);
		void OnMouseUp(MouseButtons Button, PointF Location);
		void OnMouseMove(MouseButtons Button, PointF Location);
	}
}
