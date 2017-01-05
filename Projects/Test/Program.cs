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

			ISerializeArray obj = Creator.Create<ISerializeArray>();

			VisualScriptTool.Editor.Test1[] test = new VisualScriptTool.Editor.Test1[2] { new Test1(), null };
			//Creator.GetSerializer(test.GetType()).Serialize(obj, test);

			//System.IO.File.WriteAllText("D:/1.json", obj.Content);


			obj = Creator.Create<ISerializeArray>(System.IO.File.ReadAllText("D:/1.json"));

			test = new VisualScriptTool.Editor.Test1[2];
			Creator.GetSerializer(test.GetType()).Deserialize(obj, test);
		}
	}
}
