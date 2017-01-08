// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor.Serializers
{
	class VariableStatementInstance_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.VariableStatementInstance); }
		}

		public override object CreateInstance()
		{
			return new VisualScriptTool.Editor.VariableStatementInstance(null);
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
						Set(elementObject, 0, elementType.AssemblyQualifiedName);
						GetSerializer(elementType).Serialize(AddObject(elementObject, 1), element); 
					}
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.VariableStatementInstance VariableStatementInstance = (VisualScriptTool.Editor.VariableStatementInstance)Instance;
				// Statement
				if (VariableStatementInstance.Statement == null)
					Set(Object, 3, null);
				else
				{
					ISerializeObject StatementObject = AddObject(Object, 3); 
					System.Type StatementType = VariableStatementInstance.Statement.GetType();
					Set(StatementObject, 0, StatementType.AssemblyQualifiedName);
					GetSerializer(StatementType).Serialize(AddObject(StatementObject, 1), VariableStatementInstance.Statement);
				}
				// Position
				ISerializeObject PositionObject = AddObject(Object, 0); 
				System.Type PositionType = VariableStatementInstance.Position.GetType();
				Set(PositionObject, 0, PositionType.AssemblyQualifiedName);
				GetSerializer(PositionType).Serialize(AddObject(PositionObject, 1), VariableStatementInstance.Position);
				// HeaderSize
				ISerializeObject HeaderSizeObject = AddObject(Object, 1); 
				System.Type HeaderSizeType = VariableStatementInstance.HeaderSize.GetType();
				Set(HeaderSizeObject, 0, HeaderSizeType.AssemblyQualifiedName);
				GetSerializer(HeaderSizeType).Serialize(AddObject(HeaderSizeObject, 1), VariableStatementInstance.HeaderSize);
				// BodySize
				ISerializeObject BodySizeObject = AddObject(Object, 2); 
				System.Type BodySizeType = VariableStatementInstance.BodySize.GetType();
				Set(BodySizeObject, 0, BodySizeType.AssemblyQualifiedName);
				GetSerializer(BodySizeType).Serialize(AddObject(BodySizeObject, 1), VariableStatementInstance.BodySize);
			}
		}

		public override T Deserialize<T>(ISerializeData Data)
		{
			if (Data == null)
				throw new System.ArgumentNullException("Data cannot be null");
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Editor.VariableStatementInstance[] VariableStatementInstanceArray = (VisualScriptTool.Editor.VariableStatementInstance[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					if (arrayObj == null)
					{
						VariableStatementInstanceArray[i] = null;
						continue;
					}
					VariableStatementInstanceArray[i] = GetSerializer(targetType).Deserialize<VisualScriptTool.Editor.VariableStatementInstance>(Get<ISerializeObject>(arrayObj, 1)); 
				}
				return (T)(object)VariableStatementInstanceArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.VariableStatementInstance VariableStatementInstance = (VisualScriptTool.Editor.VariableStatementInstance)CreateInstance();
				// Statement
				ISerializeObject StatementObject = Get<ISerializeObject>(Object, 3, null);
				if (StatementObject != null)
				{
					ISerializeObject StatementObjectValue = Get<ISerializeObject>(Object, 3); 
					Serializer StatementSerializer = GetSerializer(System.Type.GetType(Get<string>(StatementObjectValue, 0)));
					VariableStatementInstance.Statement = StatementSerializer.Deserialize<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(StatementObjectValue, 1));
				}
				else
					VariableStatementInstance.Statement = null;
				// Position
				ISerializeObject PositionObject = Get<ISerializeObject>(Object, 0, null);
				if (PositionObject != null)
				{
					ISerializeObject PositionObjectValue = Get<ISerializeObject>(Object, 0); 
					Serializer PositionSerializer = GetSerializer(System.Type.GetType(Get<string>(PositionObjectValue, 0)));
					VariableStatementInstance.Position = PositionSerializer.Deserialize<System.Drawing.PointF>(Get<ISerializeObject>(PositionObjectValue, 1));
				}
				// HeaderSize
				ISerializeObject HeaderSizeObject = Get<ISerializeObject>(Object, 1, null);
				if (HeaderSizeObject != null)
				{
					ISerializeObject HeaderSizeObjectValue = Get<ISerializeObject>(Object, 1); 
					Serializer HeaderSizeSerializer = GetSerializer(System.Type.GetType(Get<string>(HeaderSizeObjectValue, 0)));
					VariableStatementInstance.HeaderSize = HeaderSizeSerializer.Deserialize<System.Drawing.SizeF>(Get<ISerializeObject>(HeaderSizeObjectValue, 1));
				}
				// BodySize
				ISerializeObject BodySizeObject = Get<ISerializeObject>(Object, 2, null);
				if (BodySizeObject != null)
				{
					ISerializeObject BodySizeObjectValue = Get<ISerializeObject>(Object, 2); 
					Serializer BodySizeSerializer = GetSerializer(System.Type.GetType(Get<string>(BodySizeObjectValue, 0)));
					VariableStatementInstance.BodySize = BodySizeSerializer.Deserialize<System.Drawing.SizeF>(Get<ISerializeObject>(BodySizeObjectValue, 1));
				}
				return (T)(object)VariableStatementInstance;
			}
		}

	}
}