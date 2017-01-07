// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace System.Drawing
{
	class SizeF_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(System.Drawing.SizeF); }
		}

		public override object CreateInstance()
		{
			return new System.Drawing.SizeF(0, 0);
		}

		public override void Serialize(ISerializeData Data, object Instance)
		{
			if (Data == null || Instance == null)
				throw new System.ArgumentNullException("Data and/or Instance cannot be null");
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
				System.Drawing.SizeF[] SizeFArray = null;
				if (instanceType.IsArray())
					SizeFArray = (System.Drawing.SizeF[])Instance;
				else
					SizeFArray = ((System.Collections.Generic.List<System.Drawing.SizeF>)Instance).ToArray();
				for (int i = 0; i < SizeFArray.Length; ++i)
				{
					System.Drawing.SizeF element = SizeFArray[i];
					ISerializeObject elementObject = AddObject(Array); 
					System.Type elementType = element.GetType();
					Set(elementObject, 0, elementType.AssemblyQualifiedName);
					GetSerializer(elementType).Serialize(AddObject(elementObject, 1), element); 
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				System.Drawing.SizeF SizeF = (System.Drawing.SizeF)Instance;
				// Width
				Set(Object, 0, SizeF.Width);
				// Height
				Set(Object, 1, SizeF.Height);
			}
		}

		public override T Deserialize<T>(ISerializeData Data)
		{
			if (Data == null)
				throw new System.ArgumentNullException("Data cannot be null");
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				System.Drawing.SizeF[] SizeFArray = (System.Drawing.SizeF[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					SizeFArray[i] = GetSerializer(targetType).Deserialize<System.Drawing.SizeF>(Get<ISerializeObject>(arrayObj, 1)); 
				}
				return (T)(object)SizeFArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				System.Drawing.SizeF SizeF = (System.Drawing.SizeF)CreateInstance();
				// Width
				SizeF.Width = Get<System.Single>(Object, 0, 0);
				// Height
				SizeF.Height = Get<System.Single>(Object, 1, 0);
				return (T)(object)SizeF;
			}
		}

	}
}