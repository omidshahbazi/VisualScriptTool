// Generaterd file
using VisualScriptTool.Serialization;
namespace VisualScriptTool.Editor
{
	class Test1_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.Test1); }
		}

		public override void Serialize(ISerializeObject Object, object Instance)
		{
			if (Type != Instance.GetType())
				throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
			VisualScriptTool.Editor.Test1 Test1 = (VisualScriptTool.Editor.Test1)Instance;
			// Name
			if (Test1.Name == null)
				Set(Object, 1, null);
			else
			{
				ISerializeArray NameArray = AddArray(Object, 1);
				for (int i = 0; i < Test1.Name.Length; ++i)
				{
					System.String element = Test1.Name[i];
					if (element == null)
					{
						Add(NameArray, null);
						continue;
					}
					Add(NameArray, element);
				}
			}
			// child
			if (Test1.child == null)
				Set(Object, 3, null);
			else
				GetSerializer(Test1.child.GetType()).Serialize(AddObject(Object, 3), Test1.child);
		}

		public override void Deserialize(ISerializeObject Object, object Instance)
		{
			if (Type != Instance.GetType())
				throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
			VisualScriptTool.Editor.Test1 Test1 = (VisualScriptTool.Editor.Test1)Instance;
			// Name
			ISerializeArray NameArray = Get<ISerializeArray>(Object, 1, null);
			if (NameArray == null)
				Test1.Name = null;
			else
			{
				Test1.Name = (System.String[])System.Array.CreateInstance(typeof(System.String), NameArray.Count);
				for (uint i = 0; i < NameArray.Count; ++i)
				{
					Test1.Name[(int)i] = NameArray.Get<System.String>(i);
				}
			}
			// child
			ISerializeObject childObject = Get<ISerializeObject>(Object, 3, null);
			if (childObject == null)
				Test1.child = null;
			else
			{
				// Test1.child Allocation
				GetSerializer(Test1.child.GetType()).Deserialize(childObject, Test1.child);
			}
		}

	}
}