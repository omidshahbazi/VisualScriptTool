// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Drawing;

namespace VisualScriptTool.Editor
{
	public struct BoundsF
	{
		private PointF position;
		private SizeF size;

		public PointF Position
		{
			get { return position; }
			set
			{
				position = value;

				CalculateMaxPosition();
			}
		}

		public SizeF Size
		{
			get { return size; }
			set
			{
				size = value;

				CalculateMaxPosition();
            }
		}

		public PointF MaxPosition
		{
			get;
			private set;
		}

		public BoundsF(PointF Position, SizeF Size)
		{
			position = Position;
			size = Size;
			MaxPosition = PointF.Empty;

			CalculateMaxPosition();
        }

		public bool Contains(PointF Point)
		{
			return (Point.X >= Position.X && Point.X <= MaxPosition.X && Point.Y >= Position.Y && Point.Y <= MaxPosition.Y);
        }

		private void CalculateMaxPosition()
		{
			MaxPosition = new PointF(Position.X + size.Width, Position.Y + size.Height);
		}
	}
}