// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor
{
	class StatementInstance_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.StatementInstance); }
		}

		public override object CreateInstance()
		{
			return null;
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
				VisualScriptTool.Editor.StatementInstance[] StatementInstanceArray = null;
				if (instanceType.IsArray())
					StatementInstanceArray = (VisualScriptTool.Editor.StatementInstance[])Instance;
				else
					StatementInstanceArray = ((System.Collections.Generic.List<VisualScriptTool.Editor.StatementInstance>)Instance).ToArray();
				for (int i = 0; i < StatementInstanceArray.Length; ++i)
				{
					VisualScriptTool.Editor.StatementInstance element = StatementInstanceArray[i];
					if (element == null)
						Add(Array, null);
					else
					{
						ISerializeObject elementObject = AddObject(Array); 
						System.Type elementType = element.GetType();
						Set(elementObject, 0, elementType.AssemblyQualifiedName);
						GetSerializer(elementType).Serialize(AddObject(elementObject, 1), element); 
					}
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.StatementInstance StatementInstance = (VisualScriptTool.Editor.StatementInstance)Instance;
				// Position
				ISerializeObject PositionObject = AddObject(Object, 0); 
				System.Type PositionType = StatementInstance.Position.GetType();
				Set(PositionObject, 0, PositionType.AssemblyQualifiedName);
				GetSerializer(PositionType).Serialize(AddObject(PositionObject, 1), StatementInstance.Position);
				// HeaderSize
				ISerializeObject HeaderSizeObject = AddObject(Object, 1); 
				System.Type HeaderSizeType = StatementInstance.HeaderSize.GetType();
				Set(HeaderSizeObject, 0, HeaderSizeType.AssemblyQualifiedName);
				GetSerializer(HeaderSizeType).Serialize(AddObject(HeaderSizeObject, 1), StatementInstance.HeaderSize);
				// BodySize
				ISerializeObject BodySizeObject = AddObject(Object, 2); 
				System.Type BodySizeType = StatementInstance.BodySize.GetType();
				Set(BodySizeObject, 0, BodySizeType.AssemblyQualifiedName);
				GetSerializer(BodySizeType).Serialize(AddObject(BodySizeObject, 1), StatementInstance.BodySize);
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
				VisualScriptTool.Editor.StatementInstance[] StatementInstanceArray = null;
				if (instanceType.IsArray())
					StatementInstanceArray = (VisualScriptTool.Editor.StatementInstance[])Instance;
				else
					StatementInstanceArray = (VisualScriptTool.Editor.StatementInstance[])System.Array.CreateInstance(elementType, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					if (arrayObj == null)
					{
						StatementInstanceArray[i] = null;
						continue;
					}
					if (StatementInstanceArray[i] == null)
						StatementInstanceArray[i] = (VisualScriptTool.Editor.StatementInstance)GetSerializer(targetType).CreateInstance();
					GetSerializer(targetType).Deserialize(Get<ISerializeObject>(arrayObj, 1), StatementInstanceArray[i]); 
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.StatementInstance StatementInstance = (VisualScriptTool.Editor.StatementInstance)Instance;
				// Position
				ISerializeObject PositionObject = Get<ISerializeObject>(Object, 0, null);
				if (PositionObject != null)
				{
					ISerializeObject PositionObjectValue = Get<ISerializeObject>(Object, 0); 
					Serializer PositionSerializer = GetSerializer(System.Type.GetType(Get<string>(PositionObjectValue, 0)));
					StatementInstance.Position = (System.Drawing.PointF)PositionSerializer.CreateInstance();
					PositionSerializer.Deserialize(Get<ISerializeObject>(PositionObjectValue, 1), StatementInstance.Position);
				}
				// HeaderSize
				ISerializeObject HeaderSizeObject = Get<ISerializeObject>(Object, 1, null);
				if (HeaderSizeObject != null)
				{
					ISerializeObject HeaderSizeObjectValue = Get<ISerializeObject>(Object, 1); 
					Serializer HeaderSizeSerializer = GetSerializer(System.Type.GetType(Get<string>(HeaderSizeObjectValue, 0)));
					StatementInstance.HeaderSize = (System.Drawing.SizeF)HeaderSizeSerializer.CreateInstance();
					HeaderSizeSerializer.Deserialize(Get<ISerializeObject>(HeaderSizeObjectValue, 1), StatementInstance.HeaderSize);
				}
				// BodySize
				ISerializeObject BodySizeObject = Get<ISerializeObject>(Object, 2, null);
				if (BodySizeObject != null)
				{
					ISerializeObject BodySizeObjectValue = Get<ISerializeObject>(Object, 2); 
					Serializer BodySizeSerializer = GetSerializer(System.Type.GetType(Get<string>(BodySizeObjectValue, 0)));
					StatementInstance.BodySize = (System.Drawing.SizeF)BodySizeSerializer.CreateInstance();
					BodySizeSerializer.Deserialize(Get<ISerializeObject>(BodySizeObjectValue, 1), StatementInstance.BodySize);
				}
			}
		}

	}
}