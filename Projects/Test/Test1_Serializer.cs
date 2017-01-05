// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor
{
	class Test1_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.Test1); }
		}

		public override object CreateInstance()
		{
			return new VisualScriptTool.Editor.Test1();
		}

		public override void Serialize(ISerializeData Data, object Instance)
		{
			if (Instance == null)
				throw new System.ArgumentNullException("Instance cannot be null");
			System.Type instanceType = Instance.GetType();
			if (instanceType.IsArray())
				instanceType = instanceType.GetArrayElementType();
			else if (instanceType.IsList())
				instanceType = instanceType.GetListElementType();
			if (Type != instanceType)
				throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
			instanceType = Instance.GetType();
			if (instanceType.IsArrayOrList())
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Editor.Test1[] Test1Array = null;
				if (instanceType.IsArray())
					Test1Array = (VisualScriptTool.Editor.Test1[])Instance;
				else
					Test1Array = ((System.Collections.Generic.List<VisualScriptTool.Editor.Test1>)Instance).ToArray();
				for (int i = 0; i < Test1Array.Length; ++i)
					Serialize(AddObject(Array), Test1Array[i]); 
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
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
					{
						ISerializeObject childObject = AddObject(Object, 3); 
						System.Type type = Test1.child.GetType();
						Set(childObject, 0, type.FullName);
						GetSerializer(type).Serialize(AddObject(childObject, 1), Test1.child);
					}
				}
				}

			public override void Deserialize(ISerializeData Data, object Instance)
			{
				//if (Instance == null)
				//	throw new System.ArgumentNullException("Instance cannot be null");
				//System.Type instanceType = Instance.GetType();
				//if (instanceType.IsArray())
				//	instanceType = instanceType.GetArrayElementType();
				//else if (instanceType.IsList())
				//	instanceType = instanceType.GetListElementType();
				//if (Type != instanceType)
				//	throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
				//	// Name
				//	ISerializeArray NameArray = Get<ISerializeArray>(Object, 1, null);
				//	if (NameArray == null)
				//		Test1.Name = null;
				//	else
				//	{
				//		Test1.Name = (System.String[])System.Array.CreateInstance(typeof(System.String), NameArray.Count);
				//		for (uint i = 0; i < NameArray.Count; ++i)
				//		{
				//			Test1.Name[(int)i] = NameArray.Get<System.String>(i);
				//		}
				//	}
				//	// child
				//	ISerializeObject childObject = Get<ISerializeObject>(Object, 3, null);
				//	if (childObject != null)
				//	{
				//		ISerializeObject childObjectValue = Get<ISerializeObject>(Object, 3); 
				//		Serializer childSerializer = GetSerializer(System.Type.GetType(Get<string>(childObjectValue, 0)));
				//		Test1.child = (System.Random)childSerializer.CreateInstance();
				//		childSerializer.Deserialize(Get<ISerializeObject>(childObjectValue, 1), Test1.child);
				//	}
				//	else
				//		Test1.child = null;
				}

			}
		}