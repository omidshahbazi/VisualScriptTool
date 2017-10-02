// Copyright 2016-2017 ?????????????. All Rights Reserved.

namespace VisualScriptTool.Editor
{
	static class DragAndDropManager
	{
		public static object data = null;

		public static void SetData(object Data)
		{
			data = Data;
		}

		public static object GetData()
		{
			object temp = data;
			data = null;
			return temp;
		}
	}
}