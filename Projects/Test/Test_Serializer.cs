// Generaterd file
using VisualScriptTool.Serialization;
namespace VisualScriptTool.Editor
{
	class Test_Serializer : Serializer
	{

		public void Serialize(ISerializeObject Object, Test Instance)
		{
			Object.Set("0", Instance.Index);
			Object.Set("1", Instance.Flag);
			Object.Set("2", Instance.Name2);
			Object.Set("3", Instance.Points);
		}

		public void Deserialize(ISerializeObject Object, Test Instance)
		{
			Instance.Index = Object.Get<System.Int32>("0");
			Instance.Flag = Object.Get<System.Boolean>("1");
			Instance.Name2 = Object.Get<System.String[]>("2");
			Instance.Points = Object.Get<System.Random[]>("3");
		}

	}
}