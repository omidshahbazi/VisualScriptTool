// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor
{
	class Test_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.Test); }
		}

		public override object CreateInstance()
		{
			return VisualScriptTool.Editor.Test.Instantiate(Type, 1);
		}

		public override void Serialize(ISerializeData Data, object Instance)
		{
			if (Data == null || Instance == null)
				throw new System.ArgumentNullException("Data and Instance cannot be null");
			System.Type instanceType = Instance.GetType();
			if (instanceType.IsArray())
				instanceType = instanceType.GetArrayElementType();
			else if (instanceType.IsList())
				instanceType = instanceType.GetListElementType();
			if (Type != instanceType)
				throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
			instanceType = Instance.GetType();
			if ((Data is ISerializeObject && instanceType.IsArrayOrList()) || (Data is ISerializeArray && !instanceType.IsArrayOrList()))
				throw new System.ArgumentException("Data and Instance mismatch [" + Type.FullName + "]");
			if (instanceType.IsArrayOrList())
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Editor.Test[] TestArray = null;
				if (instanceType.IsArray())
					TestArray = (VisualScriptTool.Editor.Test[])Instance;
				else
					TestArray = ((System.Collections.Generic.List<VisualScriptTool.Editor.Test>)Instance).ToArray();
				for (int i = 0; i < TestArray.Length; ++i)
				{
					if (TestArray[i] == null)
						Add(Array, null);
					else
						Serialize(AddObject(Array), TestArray[i]);
					}
				}
				else
				{
					ISerializeObject Object = (ISerializeObject)Data; 
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
								ISerializeObject Points1ArrayObject = AddObject(Points1Array); 
								System.Type elementType = element.GetType();
								Set(Points1ArrayObject, 0, elementType.GUID.ToString());
								GetSerializer(elementType).Serialize(AddObject(Points1ArrayObject, 1), element); 
							}
						}
						// Points2
						// child
						ISerializeObject childObject = AddObject(Object, 6); 
						System.Type type = Test.child.GetType();
						Set(childObject, 0, type.FullName);
						GetSerializer(type).Serialize(AddObject(childObject, 1), Test.child);
					}
					}

				public override void Deserialize(ISerializeData Data, object Instance)
				{
					if (Data == null || Instance == null)
						throw new System.ArgumentNullException("Data and Instance cannot be null");
					System.Type instanceType = Instance.GetType();
					if (instanceType.IsArray())
						instanceType = instanceType.GetArrayElementType();
					else if (instanceType.IsList())
						instanceType = instanceType.GetListElementType();
					if (Type != instanceType)
						throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
					instanceType = Instance.GetType();
					if ((Data is ISerializeObject && instanceType.IsArrayOrList()) || (Data is ISerializeArray && !instanceType.IsArrayOrList()))
						throw new System.ArgumentException("Data and Instance mismatch [" + Type.FullName + "]");
					if (instanceType.IsArrayOrList())
					{
						System.Type elementType = (instanceType.IsArray() ? instanceType.GetArrayElementType() : instanceType.GetListElementType());
						ISerializeArray Array = (ISerializeArray)Data; 
						VisualScriptTool.Editor.Test[] TestArray = null;
						if (instanceType.IsArray())
							TestArray = (VisualScriptTool.Editor.Test[])Instance;
						else
							TestArray = (VisualScriptTool.Editor.Test[])System.Array.CreateInstance(instanceType, Array.Count);
						for (uint i = 0; i < Array.Count; ++i)
						{
							ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
							if (arrayObj == null)
							{
								TestArray[i] = null;
								continue;
							}
							if (TestArray[i] == null)
								TestArray[i] = (VisualScriptTool.Editor.Test)GetSerializer(elementType).CreateInstance();
							Deserialize(arrayObj, TestArray[i]);
						}
					}
					else
					{
						ISerializeObject Object = (ISerializeObject)Data; 
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
						// Points1
						ISerializeArray Points1Array = Get<ISerializeArray>(Object, 9, null);
						if (Points1Array == null)
							Test.Points1 = null;
						else
						{
							Test.Points1 = (System.Random[])System.Array.CreateInstance(typeof(System.Random), Points1Array.Count);
							for (uint i = 0; i < Points1Array.Count; ++i)
							{
								Test.Points1[(int)i] = (System.Random)GetSerializer(Test.Points1.GetType()).CreateInstance();
								GetSerializer(Test.Points1.GetType()).Deserialize(Get<ISerializeObject>(Points1Array, i), Test.Points1[(int)i]);
							}
						}
						// Points2
						// child
						ISerializeObject childObject = Get<ISerializeObject>(Object, 6, null);
						if (childObject != null)
						{
							ISerializeObject childObjectValue = Get<ISerializeObject>(Object, 6); 
							Serializer childSerializer = GetSerializer(System.Type.GetType(Get<string>(childObjectValue, 0)));
							Test.child = (VisualScriptTool.Editor.Test1)childSerializer.CreateInstance();
							childSerializer.Deserialize(Get<ISerializeObject>(childObjectValue, 1), Test.child);
						}
					}
					}

				}
			}