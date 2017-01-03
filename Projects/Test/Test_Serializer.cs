// Generaterd file
using VisualScriptTool.Serialization;
namespace VisualScriptTool.Editor
{
	class Test_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.Test); }
		}

		public override void Serialize(ISerializeObject Object, object Instance)
		{
			if (Type != Instance.GetType())
				throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
			VisualScriptTool.Editor.Test Test = (VisualScriptTool.Editor.Test)Instance;
			// Index
			Set(Object, 0, Test.Index);
			// Flag
			Set(Object, 1, Test.Flag);
			// Name2
			if (Test.Name2 == null)
				Set(Object, 2, null);
			else
			{
				ISerializeArray Name2Array = AddArray(Object, 2);
				for (int i = 0; i < Test.Name2.Length; ++i)
				{
					System.String element = Test.Name2[i];
					if (element == null)
					{
						Add(Name2Array, null);
						continue;
					}
					Add(Name2Array, element);
				}
			}
			// Name3
			if (Test.Name3 == null)
				Set(Object, 7, null);
			else
			{
				ISerializeArray Name3Array = AddArray(Object, 7);
				for (int i = 0; i < Test.Name3.Count; ++i)
				{
					System.String element = Test.Name3[i];
					if (element == null)
					{
						Add(Name3Array, null);
						continue;
					}
					Add(Name3Array, element);
				}
			}
			// Points1
			if (Test.Points1 == null)
				Set(Object, 9, null);
			else
			{
				ISerializeArray Points1Array = AddArray(Object, 9);
				for (int i = 0; i < Test.Points1.Length; ++i)
				{
					System.Random element = Test.Points1[i];
					if (element == null)
					{
						Add(Points1Array, null);
						continue;
					}
					GetSerializer(element.GetType()).Serialize(AddObject(Points1Array), element); 
				}
			}
			// Points2
			if (Test.Points2 == null)
				Set(Object, 3, null);
			else
			{
				ISerializeArray Points2Array = AddArray(Object, 3);
				for (int i = 0; i < Test.Points2.Count; ++i)
				{
					System.Random element = Test.Points2[i];
					if (element == null)
					{
						Add(Points2Array, null);
						continue;
					}
					GetSerializer(element.GetType()).Serialize(AddObject(Points2Array), element); 
				}
			}
			// child
			if (Test.child == null)
				Set(Object, 6, null);
			else
				GetSerializer(Test.child.GetType()).Serialize(AddObject(Object, 6), Test.child);
		}

		public override void Deserialize(ISerializeObject Object, object Instance)
		{
			if (Type != Instance.GetType())
				throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
			VisualScriptTool.Editor.Test Test = (VisualScriptTool.Editor.Test)Instance;
			// Index
			Test.Index = Get<System.Int32>(Object, 0, 10123);
			// Flag
			Test.Flag = Get<System.Boolean>(Object, 1, true);
			// Name2
			ISerializeArray Name2Array = Get<ISerializeArray>(Object, 2, null);
			if (Name2Array == null)
				Test.Name2 = null;
			else
			{
				Test.Name2 = (System.String[])System.Array.CreateInstance(typeof(System.String), Name2Array.Count);
				for (uint i = 0; i < Name2Array.Count; ++i)
				{
					Test.Name2[(int)i] = Name2Array.Get<System.String>(i);
				}
			}
			// Name3
			ISerializeArray Name3Array = Get<ISerializeArray>(Object, 7, null);
			if (Name3Array == null)
				Test.Name3 = null;
			else
			{
				// Test.Name3 Allocation
				for (uint i = 0; i < Name3Array.Count; ++i)
				{
					Test.Name3.Add(Name3Array.Get<System.String>(i));
				}
			}
			// Points1
			ISerializeArray Points1Array = Get<ISerializeArray>(Object, 9, null);
			if (Points1Array == null)
				Test.Points1 = null;
			else
			{
				Test.Points1 = (System.Random[])System.Array.CreateInstance(typeof(System.Random), Points1Array.Count);
				for (uint i = 0; i < Points1Array.Count; ++i)
				{
					// Test.Points1[(int)i] Allocation
					GetSerializer(Test.Points1.GetType()).Deserialize(Get<ISerializeObject>(Points1Array, i), Test.Points1[(int)i]);
				}
			}
			// Points2
			ISerializeArray Points2Array = Get<ISerializeArray>(Object, 3, null);
			if (Points2Array == null)
				Test.Points2 = null;
			else
			{
				// Test.Points2 Allocation
				for (uint i = 0; i < Points2Array.Count; ++i)
				{
					// Test.Points2.Add(Allocated);
					GetSerializer(Test.Points2.GetType()).Deserialize(Get<ISerializeObject>(Points2Array, i), Test.Points2[(int)i]);
				}
			}
			// child
			ISerializeObject childObject = Get<ISerializeObject>(Object, 6, null);
			if (childObject == null)
				Test.child = null;
			else
			{
				// Test.child Allocation
				GetSerializer(Test.child.GetType()).Deserialize(childObject, Test.child);
			}
		}

	}
}