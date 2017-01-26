// This File Has Generated By Compiler, Do Not Change It Manually
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor.Serializers
{
	class IfStatementInstance_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.IfStatementInstance); }
		}

		public override object CreateInstance()
		{
			return new VisualScriptTool.Editor.IfStatementInstance();
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
			ReferenceTable references = new ReferenceTable();
			SerializeInternal(Data, Instance, instanceType, references);
		}

		public override T Deserialize<T>(ISerializeData Data)
		{
			if (Data == null)
				throw new System.ArgumentNullException("Data cannot be null");
			GUIDTable references = new GUIDTable();
			ResolverList resolverList = new ResolverList();
			T returnValue = DeserializeInternal<T>(Data, references, resolverList);
			for (int i = 0; i < resolverList.Count; ++i)
				resolverList[i].Reslve(references);
			return returnValue;
		}

		public override void SerializeInternal(ISerializeData Data, object Instance, System.Type InstanceType, ReferenceTable References)
		{
			if (InstanceType.IsArrayOrList())
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Editor.IfStatementInstance[] IfStatementInstanceArray = null;
				if (InstanceType.IsArray())
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
						Set(elementObject, 1, elementType.AssemblyQualifiedName);
						GetSerializer(elementType).SerializeInternal(AddObject(elementObject, 2), element, elementType, References); 
					}
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.IfStatementInstance IfStatementInstance = (VisualScriptTool.Editor.IfStatementInstance)Instance;
				// Statement
				if (IfStatementInstance.Statement == null)
					Set(Object, 3, null);
				else
				{
					ISerializeObject StatementObject = AddObject(Object, 3); 
					string guid = string.Empty;
					if (References.ContainsKey(IfStatementInstance.Statement))
						guid = References[IfStatementInstance.Statement];
					else
					{
						guid = System.Guid.NewGuid().ToString();
						References[IfStatementInstance.Statement] = guid;
						System.Type StatementType = IfStatementInstance.Statement.GetType();
						Set(StatementObject, 1, StatementType.AssemblyQualifiedName);
						GetSerializer(StatementType).SerializeInternal(AddObject(StatementObject, 2), IfStatementInstance.Statement, StatementType, References);
					}
					Set(StatementObject, 0, guid);
				}
				// Position
				ISerializeObject PositionObject = AddObject(Object, 0); 
				System.Type PositionType = IfStatementInstance.Position.GetType();
				Set(PositionObject, 1, PositionType.AssemblyQualifiedName);
				GetSerializer(PositionType).SerializeInternal(AddObject(PositionObject, 2), IfStatementInstance.Position, PositionType, References);
				// HeaderSize
				ISerializeObject HeaderSizeObject = AddObject(Object, 1); 
				System.Type HeaderSizeType = IfStatementInstance.HeaderSize.GetType();
				Set(HeaderSizeObject, 1, HeaderSizeType.AssemblyQualifiedName);
				GetSerializer(HeaderSizeType).SerializeInternal(AddObject(HeaderSizeObject, 2), IfStatementInstance.HeaderSize, HeaderSizeType, References);
				// BodySize
				ISerializeObject BodySizeObject = AddObject(Object, 2); 
				System.Type BodySizeType = IfStatementInstance.BodySize.GetType();
				Set(BodySizeObject, 1, BodySizeType.AssemblyQualifiedName);
				GetSerializer(BodySizeType).SerializeInternal(AddObject(BodySizeObject, 2), IfStatementInstance.BodySize, BodySizeType, References);
			}
		}

		public override T DeserializeInternal<T>(ISerializeData Data, GUIDTable References, ResolverList ResolverList)
		{
			T returnValue = default(T);
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Editor.IfStatementInstance[] IfStatementInstanceArray = (VisualScriptTool.Editor.IfStatementInstance[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 1));
					if (arrayObj == null)
					{
						IfStatementInstanceArray[i] = null;
						continue;
					}
					IfStatementInstanceArray[i] = GetSerializer(targetType).DeserializeInternal<VisualScriptTool.Editor.IfStatementInstance>(Get<ISerializeObject>(arrayObj, 2), References, ResolverList); 
				}
				returnValue = (T)(object)IfStatementInstanceArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.IfStatementInstance IfStatementInstance = (VisualScriptTool.Editor.IfStatementInstance)CreateInstance();
				// Statement
				ISerializeObject StatementObject = Get<ISerializeObject>(Object, 3, null);
				if (StatementObject != null)
				{
					ISerializeObject StatementObjectValue = Get<ISerializeObject>(Object, 3); 
					string guid = Get<string>(StatementObjectValue, 0);
					if (Contains(StatementObjectValue, 1))
					{
						Serializer StatementSerializer = GetSerializer(System.Type.GetType(Get<string>(StatementObjectValue, 1)));
						IfStatementInstance.Statement = StatementSerializer.DeserializeInternal<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(StatementObjectValue, 2), References, ResolverList);
						References[guid] = IfStatementInstance.Statement;
					}
					else
						ResolverList.Add(new ReferenceResolver(guid, IfStatementInstance, IfStatementInstance.GetType().GetProperty("Statement", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)));
				}
				else
					IfStatementInstance.Statement = null;
				// Position
				ISerializeObject PositionObject = Get<ISerializeObject>(Object, 0, null);
				if (PositionObject != null)
				{
					ISerializeObject PositionObjectValue = Get<ISerializeObject>(Object, 0); 
					Serializer PositionSerializer = GetSerializer(System.Type.GetType(Get<string>(PositionObjectValue, 1)));
					IfStatementInstance.Position = PositionSerializer.DeserializeInternal<System.Drawing.PointF>(Get<ISerializeObject>(PositionObjectValue, 2), References, ResolverList);
				}
				// HeaderSize
				ISerializeObject HeaderSizeObject = Get<ISerializeObject>(Object, 1, null);
				if (HeaderSizeObject != null)
				{
					ISerializeObject HeaderSizeObjectValue = Get<ISerializeObject>(Object, 1); 
					Serializer HeaderSizeSerializer = GetSerializer(System.Type.GetType(Get<string>(HeaderSizeObjectValue, 1)));
					IfStatementInstance.HeaderSize = HeaderSizeSerializer.DeserializeInternal<System.Drawing.SizeF>(Get<ISerializeObject>(HeaderSizeObjectValue, 2), References, ResolverList);
				}
				// BodySize
				ISerializeObject BodySizeObject = Get<ISerializeObject>(Object, 2, null);
				if (BodySizeObject != null)
				{
					ISerializeObject BodySizeObjectValue = Get<ISerializeObject>(Object, 2); 
					Serializer BodySizeSerializer = GetSerializer(System.Type.GetType(Get<string>(BodySizeObjectValue, 1)));
					IfStatementInstance.BodySize = BodySizeSerializer.DeserializeInternal<System.Drawing.SizeF>(Get<ISerializeObject>(BodySizeObjectValue, 2), References, ResolverList);
				}
				returnValue = (T)(object)IfStatementInstance;
			}
			return returnValue;
		}

	}
}