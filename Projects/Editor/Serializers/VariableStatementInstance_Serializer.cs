// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor
{
	class VariableStatementInstance_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.VariableStatementInstance); }
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
				VisualScriptTool.Editor.VariableStatementInstance[] VariableStatementInstanceArray = null;
				if (instanceType.IsArray())
					VariableStatementInstanceArray = (VisualScriptTool.Editor.VariableStatementInstance[])Instance;
				else
					VariableStatementInstanceArray = ((System.Collections.Generic.List<VisualScriptTool.Editor.VariableStatementInstance>)Instance).ToArray();
				for (int i = 0; i < VariableStatementInstanceArray.Length; ++i)
				{
					VisualScriptTool.Editor.VariableStatementInstance element = VariableStatementInstanceArray[i];
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
				VisualScriptTool.Editor.VariableStatementInstance VariableStatementInstance = (VisualScriptTool.Editor.VariableStatementInstance)Instance;
				// Position
				ISerializeObject PositionObject = AddObject(Object, 0); 
				System.Type PositionType = VariableStatementInstance.Position.GetType();
				Set(PositionObject, 0, PositionType.FullName);
				GetSerializer(PositionType).Serialize(AddObject(PositionObject, 1), VariableStatementInstance.Position);
				// HeaderSize
				ISerializeObject HeaderSizeObject = AddObject(Object, 1); 
				System.Type HeaderSizeType = VariableStatementInstance.HeaderSize.GetType();
				Set(HeaderSizeObject, 0, HeaderSizeType.FullName);
				GetSerializer(HeaderSizeType).Serialize(AddObject(HeaderSizeObject, 1), VariableStatementInstance.HeaderSize);
				// BodySize
				ISerializeObject BodySizeObject = AddObject(Object, 2); 
				System.Type BodySizeType = VariableStatementInstance.BodySize.GetType();
				Set(BodySizeObject, 0, BodySizeType.FullName);
				GetSerializer(BodySizeType).Serialize(AddObject(BodySizeObject, 1), VariableStatementInstance.BodySize);
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
				VisualScriptTool.Editor.VariableStatementInstance[] VariableStatementInstanceArray = null;
				if (instanceType.IsArray())
					VariableStatementInstanceArray = (VisualScriptTool.Editor.VariableStatementInstance[])Instance;
				else
					VariableStatementInstanceArray = (VisualScriptTool.Editor.VariableStatementInstance[])System.Array.CreateInstance(instanceType, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					if (arrayObj == null)
					{
						VariableStatementInstanceArray[i] = null;
						continue;
					}
					if (VariableStatementInstanceArray[i] == null)
						VariableStatementInstanceArray[i] = (VisualScriptTool.Editor.VariableStatementInstance)GetSerializer(targetType).CreateInstance();
					GetSerializer(targetType).Deserialize(Get<ISerializeObject>(arrayObj, 1), VariableStatementInstanceArray[i]); 
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.VariableStatementInstance VariableStatementInstance = (VisualScriptTool.Editor.VariableStatementInstance)Instance;
				// Position
				ISerializeObject PositionObject = Get<ISerializeObject>(Object, 0, null);
				if (PositionObject != null)
				{
					ISerializeObject PositionObjectValue = Get<ISerializeObject>(Object, 0); 
					Serializer PositionSerializer = GetSerializer(System.Type.GetType(Get<string>(PositionObjectValue, 0)));
					VariableStatementInstance.Position = (System.Drawing.PointF)PositionSerializer.CreateInstance();
					PositionSerializer.Deserialize(Get<ISerializeObject>(PositionObjectValue, 1), VariableStatementInstance.Position);
				}
				// HeaderSize
				ISerializeObject HeaderSizeObject = Get<ISerializeObject>(Object, 1, null);
				if (HeaderSizeObject != null)
				{
					ISerializeObject HeaderSizeObjectValue = Get<ISerializeObject>(Object, 1); 
					Serializer HeaderSizeSerializer = GetSerializer(System.Type.GetType(Get<string>(HeaderSizeObjectValue, 0)));
					VariableStatementInstance.HeaderSize = (System.Drawing.SizeF)HeaderSizeSerializer.CreateInstance();
					HeaderSizeSerializer.Deserialize(Get<ISerializeObject>(HeaderSizeObjectValue, 1), VariableStatementInstance.HeaderSize);
				}
				// BodySize
				ISerializeObject BodySizeObject = Get<ISerializeObject>(Object, 2, null);
				if (BodySizeObject != null)
				{
					ISerializeObject BodySizeObjectValue = Get<ISerializeObject>(Object, 2); 
					Serializer BodySizeSerializer = GetSerializer(System.Type.GetType(Get<string>(BodySizeObjectValue, 0)));
					VariableStatementInstance.BodySize = (System.Drawing.SizeF)BodySizeSerializer.CreateInstance();
					BodySizeSerializer.Deserialize(Get<ISerializeObject>(BodySizeObjectValue, 1), VariableStatementInstance.BodySize);
				}
			}
		}

	}
}