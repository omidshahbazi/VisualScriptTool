// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace System.Drawing
{
	class PointF_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(System.Drawing.PointF); }
		}

		public override object CreateInstance()
		{
			return new System.Drawing.PointF(0, 0);
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
				System.Drawing.PointF[] PointFArray = null;
				if (instanceType.IsArray())
					PointFArray = (System.Drawing.PointF[])Instance;
				else
					PointFArray = ((System.Collections.Generic.List<System.Drawing.PointF>)Instance).ToArray();
				for (int i = 0; i < PointFArray.Length; ++i)
				{
					System.Drawing.PointF element = PointFArray[i];
					ISerializeObject elementObject = AddObject(Array); 
					System.Type elementType = element.GetType();
					Set(elementObject, 0, elementType.AssemblyQualifiedName);
					GetSerializer(elementType).Serialize(AddObject(elementObject, 1), element); 
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				System.Drawing.PointF PointF = (System.Drawing.PointF)Instance;
				// X
				Set(Object, 0, PointF.X);
				// Y
				Set(Object, 1, PointF.Y);
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
				System.Drawing.PointF[] PointFArray = null;
				if (instanceType.IsArray())
					PointFArray = (System.Drawing.PointF[])Instance;
				else
					PointFArray = (System.Drawing.PointF[])System.Array.CreateInstance(elementType, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					GetSerializer(targetType).Deserialize(Get<ISerializeObject>(arrayObj, 1), PointFArray[i]); 
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				System.Drawing.PointF PointF = (System.Drawing.PointF)Instance;
				// X
				PointF.X = Get<System.Single>(Object, 0, 0);
				// Y
				PointF.Y = Get<System.Single>(Object, 1, 0);
			}
		}

	}
}