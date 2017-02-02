// This File Has Generated By Compiler, Do Not Change It Manually
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor.Serializers
{
	class VariableStatementInstance_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Editor.Language.VariableStatementInstance); }
		}

		public override object CreateInstance()
		{
			return new VisualScriptTool.Editor.Language.VariableStatementInstance(null);
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
				VisualScriptTool.Editor.Language.VariableStatementInstance[] VariableStatementInstanceArray = null;
				if (InstanceType.IsArray())
					VariableStatementInstanceArray = (VisualScriptTool.Editor.Language.VariableStatementInstance[])Instance;
				else
					VariableStatementInstanceArray = ((System.Collections.Generic.List<VisualScriptTool.Editor.Language.VariableStatementInstance>)Instance).ToArray();
				for (int i = 0; i < VariableStatementInstanceArray.Length; ++i)
				{
					VisualScriptTool.Editor.Language.VariableStatementInstance element = VariableStatementInstanceArray[i];
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
				VisualScriptTool.Editor.Language.VariableStatementInstance VariableStatementInstance = (VisualScriptTool.Editor.Language.VariableStatementInstance)Instance;
				// Mode
				ISerializeObject ModeObject = AddObject(Object, 4); 
				System.Type ModeType = VariableStatementInstance.Mode.GetType();
				Set(ModeObject, 1, ModeType.AssemblyQualifiedName);
				GetSerializer(ModeType).SerializeInternal(AddObject(ModeObject, 2), VariableStatementInstance.Mode, ModeType, References);
				// Statement
				if (VariableStatementInstance.Statement == null)
					Set(Object, 3, null);
				else
				{
					ISerializeObject StatementObject = AddObject(Object, 3); 
					string guid = string.Empty;
					if (References.ContainsKey(VariableStatementInstance.Statement))
						guid = References[VariableStatementInstance.Statement];
					else
					{
						guid = System.Guid.NewGuid().ToString();
						References[VariableStatementInstance.Statement] = guid;
						System.Type StatementType = VariableStatementInstance.Statement.GetType();
						Set(StatementObject, 1, StatementType.AssemblyQualifiedName);
						GetSerializer(StatementType).SerializeInternal(AddObject(StatementObject, 2), VariableStatementInstance.Statement, StatementType, References);
					}
					Set(StatementObject, 0, guid);
				}
				// Position
				ISerializeObject PositionObject = AddObject(Object, 0); 
				System.Type PositionType = VariableStatementInstance.Position.GetType();
				Set(PositionObject, 1, PositionType.AssemblyQualifiedName);
				GetSerializer(PositionType).SerializeInternal(AddObject(PositionObject, 2), VariableStatementInstance.Position, PositionType, References);
				// HeaderSize
				ISerializeObject HeaderSizeObject = AddObject(Object, 1); 
				System.Type HeaderSizeType = VariableStatementInstance.HeaderSize.GetType();
				Set(HeaderSizeObject, 1, HeaderSizeType.AssemblyQualifiedName);
				GetSerializer(HeaderSizeType).SerializeInternal(AddObject(HeaderSizeObject, 2), VariableStatementInstance.HeaderSize, HeaderSizeType, References);
				// BodySize
				ISerializeObject BodySizeObject = AddObject(Object, 2); 
				System.Type BodySizeType = VariableStatementInstance.BodySize.GetType();
				Set(BodySizeObject, 1, BodySizeType.AssemblyQualifiedName);
				GetSerializer(BodySizeType).SerializeInternal(AddObject(BodySizeObject, 2), VariableStatementInstance.BodySize, BodySizeType, References);
			}
		}

		public override T DeserializeInternal<T>(ISerializeData Data, GUIDTable References, ResolverList ResolverList)
		{
			T returnValue = default(T);
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Editor.Language.VariableStatementInstance[] VariableStatementInstanceArray = (VisualScriptTool.Editor.Language.VariableStatementInstance[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 1));
					if (arrayObj == null)
					{
						VariableStatementInstanceArray[i] = null;
						continue;
					}
					VariableStatementInstanceArray[i] = GetSerializer(targetType).DeserializeInternal<VisualScriptTool.Editor.Language.VariableStatementInstance>(Get<ISerializeObject>(arrayObj, 2), References, ResolverList); 
				}
				returnValue = (T)(object)VariableStatementInstanceArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Editor.Language.VariableStatementInstance VariableStatementInstance = (VisualScriptTool.Editor.Language.VariableStatementInstance)CreateInstance();
				// Mode
				ISerializeObject ModeObject = Get<ISerializeObject>(Object, 4, null);
				if (ModeObject != null)
				{
					ISerializeObject ModeObjectValue = Get<ISerializeObject>(Object, 4); 
					Serializer ModeSerializer = GetSerializer(System.Type.GetType(Get<string>(ModeObjectValue, 1)));
					VariableStatementInstance.Mode = ModeSerializer.DeserializeInternal<VisualScriptTool.Editor.Language.VariableStatementInstance.Modes>(Get<ISerializeObject>(ModeObjectValue, 2), References, ResolverList);
				}
				// Statement
				ISerializeObject StatementObject = Get<ISerializeObject>(Object, 3, null);
				if (StatementObject != null)
				{
					ISerializeObject StatementObjectValue = Get<ISerializeObject>(Object, 3); 
					string guid = Get<string>(StatementObjectValue, 0);
					if (Contains(StatementObjectValue, 1))
					{
						Serializer StatementSerializer = GetSerializer(System.Type.GetType(Get<string>(StatementObjectValue, 1)));
						VariableStatementInstance.Statement = StatementSerializer.DeserializeInternal<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(StatementObjectValue, 2), References, ResolverList);
						References[guid] = VariableStatementInstance.Statement;
					}
					else
						ResolverList.Add(new ReferenceResolver(guid, VariableStatementInstance, VariableStatementInstance.GetType().GetProperty("Statement", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)));
				}
				else
					VariableStatementInstance.Statement = null;
				// Position
				ISerializeObject PositionObject = Get<ISerializeObject>(Object, 0, null);
				if (PositionObject != null)
				{
					ISerializeObject PositionObjectValue = Get<ISerializeObject>(Object, 0); 
					Serializer PositionSerializer = GetSerializer(System.Type.GetType(Get<string>(PositionObjectValue, 1)));
					VariableStatementInstance.Position = PositionSerializer.DeserializeInternal<System.Drawing.PointF>(Get<ISerializeObject>(PositionObjectValue, 2), References, ResolverList);
				}
				// HeaderSize
				ISerializeObject HeaderSizeObject = Get<ISerializeObject>(Object, 1, null);
				if (HeaderSizeObject != null)
				{
					ISerializeObject HeaderSizeObjectValue = Get<ISerializeObject>(Object, 1); 
					Serializer HeaderSizeSerializer = GetSerializer(System.Type.GetType(Get<string>(HeaderSizeObjectValue, 1)));
					VariableStatementInstance.HeaderSize = HeaderSizeSerializer.DeserializeInternal<System.Drawing.SizeF>(Get<ISerializeObject>(HeaderSizeObjectValue, 2), References, ResolverList);
				}
				// BodySize
				ISerializeObject BodySizeObject = Get<ISerializeObject>(Object, 2, null);
				if (BodySizeObject != null)
				{
					ISerializeObject BodySizeObjectValue = Get<ISerializeObject>(Object, 2); 
					Serializer BodySizeSerializer = GetSerializer(System.Type.GetType(Get<string>(BodySizeObjectValue, 1)));
					VariableStatementInstance.BodySize = BodySizeSerializer.DeserializeInternal<System.Drawing.SizeF>(Get<ISerializeObject>(BodySizeObjectValue, 2), References, ResolverList);
				}
				returnValue = (T)(object)VariableStatementInstance;
			}
			return returnValue;
		}

	}
}