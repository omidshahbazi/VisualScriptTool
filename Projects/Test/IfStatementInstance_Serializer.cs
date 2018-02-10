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
				VisualScriptTool.Editor.IfStatementInstance[] IfStatementInstanceArray = null;
				if (instanceType.IsArray())
					IfStatementInstanceArray = (VisualScriptTool.Editor.IfStatementInstance[])Instance;
				else
					IfStatementInstanceArray = ((System.Collections.Generic.List<VisualScriptTool.Editor.IfStatementInstance>)Instance).ToArray();
				for (int i = 0; i < IfStatementInstanceArray.Length; ++i)
				{
					if (IfStatementInstanceArray[i] == null)
						Add(Array, null);
					else
						Serialize(AddObject(Array), IfStatementInstanceArray[i]);
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.IfStatementInstance IfStatementInstance = (VisualScriptTool.Editor.IfStatementInstance)Instance;
				// BodySize
				ISerializeObject BodySizeObject = AddObject(Object, 0); 
				System.Type type = StatementInstance.BodySize.GetType();
				Set(BodySizeObject, 0, type.FullName);
				GetSerializer(type).Serialize(AddObject(BodySizeObject, 1), StatementInstance.BodySize);
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
				VisualScriptTool.Editor.IfStatementInstance[] IfStatementInstanceArray = null;
				if (instanceType.IsArray())
					IfStatementInstanceArray = (VisualScriptTool.Editor.IfStatementInstance[])Instance;
				else
					IfStatementInstanceArray = (VisualScriptTool.Editor.IfStatementInstance[])System.Array.CreateInstance(instanceType, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					if (arrayObj == null)
					{
						IfStatementInstanceArray[i] = null;
						continue;
					}
					if (IfStatementInstanceArray[i] == null)
						IfStatementInstanceArray[i] = (VisualScriptTool.Editor.IfStatementInstance)GetSerializer(elementType).CreateInstance();
					Deserialize(arrayObj, IfStatementInstanceArray[i]);
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.IfStatementInstance IfStatementInstance = (VisualScriptTool.Editor.IfStatementInstance)Instance;
				// BodySize
				ISerializeObject BodySizeObject = Get<ISerializeObject>(Object, 0, null);
				if (BodySizeObject != null)
				{
					ISerializeObject BodySizeObjectValue = Get<ISerializeObject>(Object, 0); 
					Serializer BodySizeSerializer = GetSerializer(System.Type.GetType(Get<string>(BodySizeObjectValue, 0)));
					StatementInstance.BodySize = (System.Drawing.SizeF)BodySizeSerializer.CreateInstance();
					BodySizeSerializer.Deserialize(Get<ISerializeObject>(BodySizeObjectValue, 1), StatementInstance.BodySize);
				}
			}
		}

	}
}