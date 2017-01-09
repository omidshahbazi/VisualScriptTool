// This File Has Generated By Compiler, Do Not Change It Manually
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor.Serializers
{
	class ForStatement_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Language.Statements.Control.ForStatement); }
		}

		public override object CreateInstance()
		{
			return new VisualScriptTool.Language.Statements.Control.ForStatement();
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
				VisualScriptTool.Language.Statements.Control.ForStatement[] ForStatementArray = null;
				if (InstanceType.IsArray())
					ForStatementArray = (VisualScriptTool.Language.Statements.Control.ForStatement[])Instance;
				else
					ForStatementArray = ((System.Collections.Generic.List<VisualScriptTool.Language.Statements.Control.ForStatement>)Instance).ToArray();
				for (int i = 0; i < ForStatementArray.Length; ++i)
				{
					VisualScriptTool.Language.Statements.Control.ForStatement element = ForStatementArray[i];
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
				VisualScriptTool.Language.Statements.Control.ForStatement ForStatement = (VisualScriptTool.Language.Statements.Control.ForStatement)Instance;
				// MinimumValue
				if (ForStatement.MinimumValue == null)
					Set(Object, 2, null);
				else
				{
					ISerializeObject MinimumValueObject = AddObject(Object, 2); 
					string guid = string.Empty;
					if (References.ContainsKey(ForStatement.MinimumValue))
						guid = References[ForStatement.MinimumValue];
					else
					{
						guid = System.Guid.NewGuid().ToString();
						References[ForStatement.MinimumValue] = guid;
						System.Type MinimumValueType = ForStatement.MinimumValue.GetType();
						Set(MinimumValueObject, 1, MinimumValueType.AssemblyQualifiedName);
						GetSerializer(MinimumValueType).SerializeInternal(AddObject(MinimumValueObject, 2), ForStatement.MinimumValue, MinimumValueType, References);
					}
					Set(MinimumValueObject, 0, guid);
				}
				// MaximumValue
				if (ForStatement.MaximumValue == null)
					Set(Object, 3, null);
				else
				{
					ISerializeObject MaximumValueObject = AddObject(Object, 3); 
					string guid = string.Empty;
					if (References.ContainsKey(ForStatement.MaximumValue))
						guid = References[ForStatement.MaximumValue];
					else
					{
						guid = System.Guid.NewGuid().ToString();
						References[ForStatement.MaximumValue] = guid;
						System.Type MaximumValueType = ForStatement.MaximumValue.GetType();
						Set(MaximumValueObject, 1, MaximumValueType.AssemblyQualifiedName);
						GetSerializer(MaximumValueType).SerializeInternal(AddObject(MaximumValueObject, 2), ForStatement.MaximumValue, MaximumValueType, References);
					}
					Set(MaximumValueObject, 0, guid);
				}
				// StepValue
				if (ForStatement.StepValue == null)
					Set(Object, 4, null);
				else
				{
					ISerializeObject StepValueObject = AddObject(Object, 4); 
					string guid = string.Empty;
					if (References.ContainsKey(ForStatement.StepValue))
						guid = References[ForStatement.StepValue];
					else
					{
						guid = System.Guid.NewGuid().ToString();
						References[ForStatement.StepValue] = guid;
						System.Type StepValueType = ForStatement.StepValue.GetType();
						Set(StepValueObject, 1, StepValueType.AssemblyQualifiedName);
						GetSerializer(StepValueType).SerializeInternal(AddObject(StepValueObject, 2), ForStatement.StepValue, StepValueType, References);
					}
					Set(StepValueObject, 0, guid);
				}
				// Statement
				if (ForStatement.Statement == null)
					Set(Object, 5, null);
				else
				{
					ISerializeObject StatementObject = AddObject(Object, 5); 
					string guid = string.Empty;
					if (References.ContainsKey(ForStatement.Statement))
						guid = References[ForStatement.Statement];
					else
					{
						guid = System.Guid.NewGuid().ToString();
						References[ForStatement.Statement] = guid;
						System.Type StatementType = ForStatement.Statement.GetType();
						Set(StatementObject, 1, StatementType.AssemblyQualifiedName);
						GetSerializer(StatementType).SerializeInternal(AddObject(StatementObject, 2), ForStatement.Statement, StatementType, References);
					}
					Set(StatementObject, 0, guid);
				}
				// CompleteStatement
				if (ForStatement.CompleteStatement == null)
					Set(Object, 1, null);
				else
				{
					ISerializeObject CompleteStatementObject = AddObject(Object, 1); 
					string guid = string.Empty;
					if (References.ContainsKey(ForStatement.CompleteStatement))
						guid = References[ForStatement.CompleteStatement];
					else
					{
						guid = System.Guid.NewGuid().ToString();
						References[ForStatement.CompleteStatement] = guid;
						System.Type CompleteStatementType = ForStatement.CompleteStatement.GetType();
						Set(CompleteStatementObject, 1, CompleteStatementType.AssemblyQualifiedName);
						GetSerializer(CompleteStatementType).SerializeInternal(AddObject(CompleteStatementObject, 2), ForStatement.CompleteStatement, CompleteStatementType, References);
					}
					Set(CompleteStatementObject, 0, guid);
				}
			}
		}

		public override T DeserializeInternal<T>(ISerializeData Data, GUIDTable References, ResolverList ResolverList)
		{
			T returnValue = default(T);
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Language.Statements.Control.ForStatement[] ForStatementArray = (VisualScriptTool.Language.Statements.Control.ForStatement[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 1));
					if (arrayObj == null)
					{
						ForStatementArray[i] = null;
						continue;
					}
					ForStatementArray[i] = GetSerializer(targetType).DeserializeInternal<VisualScriptTool.Language.Statements.Control.ForStatement>(Get<ISerializeObject>(arrayObj, 2), References, ResolverList); 
				}
				returnValue = (T)(object)ForStatementArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Language.Statements.Control.ForStatement ForStatement = (VisualScriptTool.Language.Statements.Control.ForStatement)CreateInstance();
				// MinimumValue
				ISerializeObject MinimumValueObject = Get<ISerializeObject>(Object, 2, null);
				if (MinimumValueObject != null)
				{
					ISerializeObject MinimumValueObjectValue = Get<ISerializeObject>(Object, 2); 
					string guid = Get<string>(MinimumValueObjectValue, 0);
					if (Contains(MinimumValueObjectValue, 1))
					{
						Serializer MinimumValueSerializer = GetSerializer(System.Type.GetType(Get<string>(MinimumValueObjectValue, 1)));
						ForStatement.MinimumValue = MinimumValueSerializer.DeserializeInternal<VisualScriptTool.Language.Statements.Declaration.Variables.IntegerVariable>(Get<ISerializeObject>(MinimumValueObjectValue, 2), References, ResolverList);
						References[guid] = ForStatement.MinimumValue;
					}
					else
						ResolverList.Add(new ReferenceResolver(guid, ForStatement, ForStatement.GetType().GetProperty("MinimumValue", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)));
				}
				else
					ForStatement.MinimumValue = null;
				// MaximumValue
				ISerializeObject MaximumValueObject = Get<ISerializeObject>(Object, 3, null);
				if (MaximumValueObject != null)
				{
					ISerializeObject MaximumValueObjectValue = Get<ISerializeObject>(Object, 3); 
					string guid = Get<string>(MaximumValueObjectValue, 0);
					if (Contains(MaximumValueObjectValue, 1))
					{
						Serializer MaximumValueSerializer = GetSerializer(System.Type.GetType(Get<string>(MaximumValueObjectValue, 1)));
						ForStatement.MaximumValue = MaximumValueSerializer.DeserializeInternal<VisualScriptTool.Language.Statements.Declaration.Variables.IntegerVariable>(Get<ISerializeObject>(MaximumValueObjectValue, 2), References, ResolverList);
						References[guid] = ForStatement.MaximumValue;
					}
					else
						ResolverList.Add(new ReferenceResolver(guid, ForStatement, ForStatement.GetType().GetProperty("MaximumValue", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)));
				}
				else
					ForStatement.MaximumValue = null;
				// StepValue
				ISerializeObject StepValueObject = Get<ISerializeObject>(Object, 4, null);
				if (StepValueObject != null)
				{
					ISerializeObject StepValueObjectValue = Get<ISerializeObject>(Object, 4); 
					string guid = Get<string>(StepValueObjectValue, 0);
					if (Contains(StepValueObjectValue, 1))
					{
						Serializer StepValueSerializer = GetSerializer(System.Type.GetType(Get<string>(StepValueObjectValue, 1)));
						ForStatement.StepValue = StepValueSerializer.DeserializeInternal<VisualScriptTool.Language.Statements.Declaration.Variables.IntegerVariable>(Get<ISerializeObject>(StepValueObjectValue, 2), References, ResolverList);
						References[guid] = ForStatement.StepValue;
					}
					else
						ResolverList.Add(new ReferenceResolver(guid, ForStatement, ForStatement.GetType().GetProperty("StepValue", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)));
				}
				else
					ForStatement.StepValue = null;
				// Statement
				ISerializeObject StatementObject = Get<ISerializeObject>(Object, 5, null);
				if (StatementObject != null)
				{
					ISerializeObject StatementObjectValue = Get<ISerializeObject>(Object, 5); 
					string guid = Get<string>(StatementObjectValue, 0);
					if (Contains(StatementObjectValue, 1))
					{
						Serializer StatementSerializer = GetSerializer(System.Type.GetType(Get<string>(StatementObjectValue, 1)));
						ForStatement.Statement = StatementSerializer.DeserializeInternal<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(StatementObjectValue, 2), References, ResolverList);
						References[guid] = ForStatement.Statement;
					}
					else
						ResolverList.Add(new ReferenceResolver(guid, ForStatement, ForStatement.GetType().GetProperty("Statement", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)));
				}
				else
					ForStatement.Statement = null;
				// CompleteStatement
				ISerializeObject CompleteStatementObject = Get<ISerializeObject>(Object, 1, null);
				if (CompleteStatementObject != null)
				{
					ISerializeObject CompleteStatementObjectValue = Get<ISerializeObject>(Object, 1); 
					string guid = Get<string>(CompleteStatementObjectValue, 0);
					if (Contains(CompleteStatementObjectValue, 1))
					{
						Serializer CompleteStatementSerializer = GetSerializer(System.Type.GetType(Get<string>(CompleteStatementObjectValue, 1)));
						ForStatement.CompleteStatement = CompleteStatementSerializer.DeserializeInternal<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(CompleteStatementObjectValue, 2), References, ResolverList);
						References[guid] = ForStatement.CompleteStatement;
					}
					else
						ResolverList.Add(new ReferenceResolver(guid, ForStatement, ForStatement.GetType().GetProperty("CompleteStatement", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)));
				}
				else
					ForStatement.CompleteStatement = null;
				returnValue = (T)(object)ForStatement;
			}
			return returnValue;
		}

	}
}