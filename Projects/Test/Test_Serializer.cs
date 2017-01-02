// Generaterd file
using VisualScriptTool.Serialization;
namespace VisualScriptTool.Editor
{
	static class Test_Schema
	{

		public static void Serialize(ISerializeObject Object, Test Instance)
		{
			Serializer.Set(Object, 0, Instance.Index);
			Serializer.Set(Object, 1, Instance.Flag);
			if (Instance.Name2 == null)
				Serializer.Set(Object, 2, null);
			else
			{
				System.Array Name2Array = (System.Array)Instance.Name2;
				for (int i = 0; i < Name2Array.Length; ++i)
				{
				}
			}
			if (Instance.Points == null)
				Serializer.Set(Object, 3, null);
			else
			{
				System.Array PointsArray = (System.Array)Instance.Points;
				for (int i = 0; i < PointsArray.Length; ++i)
				{
				}
			}
			if (Instance.child == null)
				Serializer.Set(Object, 6, null);
			else
				Test_Schema.Serialize(Serializer.AddObject(Object, 6), Instance.child);
		}

		public static void Deserialize(ISerializeObject Object, Test Instance)
		{
			Instance.Index = Serializer.Get<System.Int32>(Object, 0, 10123);
			Instance.Flag = Serializer.Get<System.Boolean>(Object, 1, true);
		}

	}
}