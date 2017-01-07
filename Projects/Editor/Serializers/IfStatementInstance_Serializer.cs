// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor
{
	class IfStatementInstance_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.IfStatementInstance); }
		}

		public override object CreateInstance()
		{
			return new VisualScriptTool.Editor.IfStatementInstance(null);
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
				VisualScriptTool.Editor.IfStatementInstance[] IfStatementInstanceArray = null;
				if (instanceType.IsArray())
					IfStatementInstanceArray = (VisualScriptTool.Editor.IfStatementInstance[])Instance;
				else
					IfStatementInstanceArray = ((System.Collections.Generic.List<VisualScriptTool.Editor.IfStatementInstance>)Instance).ToArray();
				for (int i = 0; i < IfStatementInstanceArray.Length; ++i)
				{
					VisualScriptTool.Editor.IfStatementInstance element = IfStatementInstanceArray[i];
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
				VisualScriptTool.Editor.IfStatementInstance IfStatementInstance = (VisualScriptTool.Editor.IfStatementInstance)Instance;
				// Position
				ISerializeObject PositionObject = AddObject(Object, 0); 
				System.Type PositionType = IfStatementInstance.Position.GetType();
				Set(PositionObject, 0, PositionType.AssemblyQualifiedName);
				GetSerializer(PositionType).Serialize(AddObject(PositionObject, 1), IfStatementInstance.Position);
				// HeaderSize
				ISerializeObject HeaderSizeObject = AddObject(Object, 1); 
				System.Type HeaderSizeType = IfStatementInstance.HeaderSize.GetType();
				Set(HeaderSizeObject, 0, HeaderSizeType.AssemblyQualifiedName);
				GetSerializer(HeaderSizeType).Serialize(AddObject(HeaderSizeObject, 1), IfStatementInstance.HeaderSize);
				// BodySize
				ISerializeObject BodySizeObject = AddObject(Object, 2); 
				System.Type BodySizeType = IfStatementInstance.BodySize.GetType();
				Set(BodySizeObject, 0, BodySizeType.AssemblyQualifiedName);
				GetSerializer(BodySizeType).Serialize(AddObject(BodySizeObject, 1), IfStatementInstance.BodySize);
			}
		}

		public override T Deserialize<T>(ISerializeData Data)
		{
			if (Data == null)
				throw new System.ArgumentNullException("Data cannot be null");
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Editor.IfStatementInstance[] IfStatementInstanceArray = (VisualScriptTool.Editor.IfStatementInstance[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					if (arrayObj == null)
					{
						IfStatementInstanceArray[i] = null;
						continue;
					}
					IfStatementInstanceArray[i] = GetSerializer(targetType).Deserialize<VisualScriptTool.Editor.IfStatementInstance>(Get<ISerializeObject>(arrayObj, 1)); 
				}
				return (T)(object)IfStatementInstanceArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.IfStatementInstance IfStatementInstance = (VisualScriptTool.Editor.IfStatementInstance)CreateInstance();
				// Position
				ISerializeObject PositionObject = Get<ISerializeObject>(Object, 0, null);
				if (PositionObject != null)
				{
					ISerializeObject PositionObjectValue = Get<ISerializeObject>(Object, 0); 
					Serializer PositionSerializer = GetSerializer(System.Type.GetType(Get<string>(PositionObjectValue, 0)));
					IfStatementInstance.Position = PositionSerializer.Deserialize<System.Drawing.PointF>(Get<ISerializeObject>(PositionObjectValue, 1));
				}
				// HeaderSize
				ISerializeObject HeaderSizeObject = Get<ISerializeObject>(Object, 1, null);
				if (HeaderSizeObject != null)
				{
					ISerializeObject HeaderSizeObjectValue = Get<ISerializeObject>(Object, 1); 
					Serializer HeaderSizeSerializer = GetSerializer(System.Type.GetType(Get<string>(HeaderSizeObjectValue, 0)));
					IfStatementInstance.HeaderSize = HeaderSizeSerializer.Deserialize<System.Drawing.SizeF>(Get<ISerializeObject>(HeaderSizeObjectValue, 1));
				}
				// BodySize
				ISerializeObject BodySizeObject = Get<ISerializeObject>(Object, 2, null);
				if (BodySizeObject != null)
				{
					ISerializeObject BodySizeObjectValue = Get<ISerializeObject>(Object, 2); 
					Serializer BodySizeSerializer = GetSerializer(System.Type.GetType(Get<string>(BodySizeObjectValue, 0)));
					IfStatementInstance.BodySize = BodySizeSerializer.Deserialize<System.Drawing.SizeF>(Get<ISerializeObject>(BodySizeObjectValue, 1));
				}
				return (T)(object)IfStatementInstance;
			}
		}

	}
}