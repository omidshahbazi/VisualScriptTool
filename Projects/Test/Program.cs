using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualScriptTool.Editor;
using VisualScriptTool.Serialization;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			//Creator.AddSerializer(new Test_Serializer());
			Creator.AddSerializer(new Test1_Serializer());

			//ISerializeObject obj = Creator.Create();

			//VisualScriptTool.Editor.Test test = new VisualScriptTool.Editor.Test();

			//Creator.GetSerializer(test.GetType()).Serialize(obj, test);

			//System.IO.File.WriteAllText("D:/1.json", obj.Content);


			ISerializeData obj = Creator.Create<ISerializeData>(System.IO.File.ReadAllText("D:/1.json"));

			VisualScriptTool.Editor.Test1[] test = new VisualScriptTool.Editor.Test1[2];
			Creator.GetSerializer(typeof(VisualScriptTool.Editor.Test1)).Serialize(obj, test);
		}
	}
}
