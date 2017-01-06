// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor
{
	class ForStatementInstance_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.ForStatementInstance); }
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
				VisualScriptTool.Editor.ForStatementInstance[] ForStatementInstanceArray = null;
				if (instanceType.IsArray())
					ForStatementInstanceArray = (VisualScriptTool.Editor.ForStatementInstance[])Instance;
				else
					ForStatementInstanceArray = ((System.Collections.Generic.List<VisualScriptTool.Editor.ForStatementInstance>)Instance).ToArray();
				for (int i = 0; i < ForStatementInstanceArray.Length; ++i)
				{
					VisualScriptTool.Editor.ForStatementInstance element = ForStatementInstanceArray[i];
					if (element == null)
						Add(Array, null);
					else
					{
						ISerializeObject elementObject = AddObject(Array); 
						System.Type elementType = element.GetType();
						Set(elementObject, 0, elementType.FullName);
						GetSerializer(elementType).Serialize(AddObject(elementObject, 1), element); 
					}
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.ForStatementInstance ForStatementInstance = (VisualScriptTool.Editor.ForStatementInstance)Instance;
				// Position
				ISerializeObject PositionObject = AddObject(Object, 0); 
				System.Type PositionType = ForStatementInstance.Position.GetType();
				Set(PositionObject, 0, PositionType.FullName);
				GetSerializer(PositionType).Serialize(AddObject(PositionObject, 1), ForStatementInstance.Position);
				// HeaderSize
				ISerializeObject HeaderSizeObject = AddObject(Object, 1); 
				System.Type HeaderSizeType = ForStatementInstance.HeaderSize.GetType();
				Set(HeaderSizeObject, 0, HeaderSizeType.FullName);
				GetSerializer(HeaderSizeType).Serialize(AddObject(HeaderSizeObject, 1), ForStatementInstance.HeaderSize);
				// BodySize
				ISerializeObject BodySizeObject = AddObject(Object, 2); 
				System.Type BodySizeType = ForStatementInstance.BodySize.GetType();
				Set(BodySizeObject, 0, BodySizeType.FullName);
				GetSerializer(BodySizeType).Serialize(AddObject(BodySizeObject, 1), ForStatementInstance.BodySize);
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
				VisualScriptTool.Editor.ForStatementInstance[] ForStatementInstanceArray = null;
				if (instanceType.IsArray())
					ForStatementInstanceArray = (VisualScriptTool.Editor.ForStatementInstance[])Instance;
				else
					ForStatementInstanceArray = (VisualScriptTool.Editor.ForStatementInstance[])System.Array.CreateInstance(instanceType, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					if (arrayObj == null)
					{
						ForStatementInstanceArray[i] = null;
						continue;
					}
					if (ForStatementInstanceArray[i] == null)
						ForStatementInstanceArray[i] = (VisualScriptTool.Editor.ForStatementInstance)GetSerializer(targetType).CreateInstance();
					GetSerializer(targetType).Deserialize(Get<ISerializeObject>(arrayObj, 1), ForStatementInstanceArray[i]); 
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.ForStatementInstance ForStatementInstance = (VisualScriptTool.Editor.ForStatementInstance)Instance;
				// Position
				ISerializeObject PositionObject = Get<ISerializeObject>(Object, 0, null);
				if (PositionObject != null)
				{
					ISerializeObject PositionObjectValue = Get<ISerializeObject>(Object, 0); 
					Serializer PositionSerializer = GetSerializer(System.Type.GetType(Get<string>(PositionObjectValue, 0)));
					ForStatementInstance.Position = (System.Drawing.PointF)PositionSerializer.CreateInstance();
					PositionSerializer.Deserialize(Get<ISerializeObject>(PositionObjectValue, 1), ForStatementInstance.Position);
				}
				// HeaderSize
				ISerializeObject HeaderSizeObject = Get<ISerializeObject>(Object, 1, null);
				if (HeaderSizeObject != null)
				{
					ISerializeObject HeaderSizeObjectValue = Get<ISerializeObject>(Object, 1); 
					Serializer HeaderSizeSerializer = GetSerializer(System.Type.GetType(Get<string>(HeaderSizeObjectValue, 0)));
					ForStatementInstance.HeaderSize = (System.Drawing.SizeF)HeaderSizeSerializer.CreateInstance();
					HeaderSizeSerializer.Deserialize(Get<ISerializeObject>(HeaderSizeObjectValue, 1), ForStatementInstance.HeaderSize);
				}
				// BodySize
				ISerializeObject BodySizeObject = Get<ISerializeObject>(Object, 2, null);
				if (BodySizeObject != null)
				{
					ISerializeObject BodySizeObjectValue = Get<ISerializeObject>(Object, 2); 
					Serializer BodySizeSerializer = GetSerializer(System.Type.GetType(Get<string>(BodySizeObjectValue, 0)));
					ForStatementInstance.BodySize = (System.Drawing.SizeF)BodySizeSerializer.CreateInstance();
					BodySizeSerializer.Deserialize(Get<ISerializeObject>(BodySizeObjectValue, 1), ForStatementInstance.BodySize);
				}
			}
		}

	}
}