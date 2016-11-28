//// Copyright 2016-2017 ?????????????. All Rights Reserved.
//using System;
//using System.Drawing;
//using System.Windows.Forms;

//namespace VisualScriptTool.Renderer
//{
//	public class CodeArea : TextBox
//	{
//		public CodeArea()
//		{
//			AcceptsReturn = true;
//			AcceptsTab = true;
//			BackColor = Color.FromArgb(64, 64, 64);
//			Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
//			ForeColor = Color.LightSkyBlue;
//			Multiline = true;
//			ScrollBars = ScrollBars.Both;
//		}

//		protected override void OnKeyDown(KeyEventArgs e)
//		{
//			if (e.KeyData == (Keys.Control | Keys.Space))
//			{
//				FormAutoCompletion fac = new FormAutoCompletion();
//				fac.Location = PointToScreen(ContextLocation());

//				if (fac.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(fac.SelectedText))
//				{
//					SendKeys.Send("{BS}");

//					SendKeys.Send(fac.SelectedText);
//					SendKeys.Send("( )");
//				}
//			}

//			base.OnKeyDown(e);
//		}

//		private Point ContextLocation()
//		{
//			Point lpos = GetPositionFromCharIndex(Math.Max(0, SelectionStart - 1));
//			if (SelectionStart != 0 && Convert.ToChar(Text.Substring(SelectionStart - 1, 1)) == '\n')
//				return new Point(0, lpos.Y + 2 * Font.Height);
//			return new Point(lpos.X, lpos.Y + Font.Height);
//		}
//	}
//}