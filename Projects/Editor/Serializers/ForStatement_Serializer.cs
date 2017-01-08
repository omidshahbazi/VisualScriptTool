// Generaterd file
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
			if (instanceType.IsArrayOrList())
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Language.Statements.Control.ForStatement[] ForStatementArray = null;
				if (instanceType.IsArray())
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
						Set(elementObject, 0, elementType.AssemblyQualifiedName);
						GetSerializer(elementType).Serialize(AddObject(elementObject, 1), element); 
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
					System.Type MinimumValueType = ForStatement.MinimumValue.GetType();
					Set(MinimumValueObject, 0, MinimumValueType.AssemblyQualifiedName);
					GetSerializer(MinimumValueType).Serialize(AddObject(MinimumValueObject, 1), ForStatement.MinimumValue);
				}
				// MaximumValue
				if (ForStatement.MaximumValue == null)
					Set(Object, 3, null);
				else
				{
					ISerializeObject MaximumValueObject = AddObject(Object, 3); 
					System.Type MaximumValueType = ForStatement.MaximumValue.GetType();
					Set(MaximumValueObject, 0, MaximumValueType.AssemblyQualifiedName);
					GetSerializer(MaximumValueType).Serialize(AddObject(MaximumValueObject, 1), ForStatement.MaximumValue);
				}
				// StepValue
				if (ForStatement.StepValue == null)
					Set(Object, 4, null);
				else
				{
					ISerializeObject StepValueObject = AddObject(Object, 4); 
					System.Type StepValueType = ForStatement.StepValue.GetType();
					Set(StepValueObject, 0, StepValueType.AssemblyQualifiedName);
					GetSerializer(StepValueType).Serialize(AddObject(StepValueObject, 1), ForStatement.StepValue);
				}
				// Statement
				if (ForStatement.Statement == null)
					Set(Object, 5, null);
				else
				{
					ISerializeObject StatementObject = AddObject(Object, 5); 
					System.Type StatementType = ForStatement.Statement.GetType();
					Set(StatementObject, 0, StatementType.AssemblyQualifiedName);
					GetSerializer(StatementType).Serialize(AddObject(StatementObject, 1), ForStatement.Statement);
				}
				// CompleteStatement
				if (ForStatement.CompleteStatement == null)
					Set(Object, 1, null);
				else
				{
					ISerializeObject CompleteStatementObject = AddObject(Object, 1); 
					System.Type CompleteStatementType = ForStatement.CompleteStatement.GetType();
					Set(CompleteStatementObject, 0, CompleteStatementType.AssemblyQualifiedName);
					GetSerializer(CompleteStatementType).Serialize(AddObject(CompleteStatementObject, 1), ForStatement.CompleteStatement);
				}
			}
		}

		public override T Deserialize<T>(ISerializeData Data)
		{
			if (Data == null)
				throw new System.ArgumentNullException("Data cannot be null");
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Language.Statements.Control.ForStatement[] ForStatementArray = (VisualScriptTool.Language.Statements.Control.ForStatement[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					if (arrayObj == null)
					{
						ForStatementArray[i] = null;
						continue;
					}
					ForStatementArray[i] = GetSerializer(targetType).Deserialize<VisualScriptTool.Language.Statements.Control.ForStatement>(Get<ISerializeObject>(arrayObj, 1)); 
				}
				return (T)(object)ForStatementArray;
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
					Serializer MinimumValueSerializer = GetSerializer(System.Type.GetType(Get<string>(MinimumValueObjectValue, 0)));
					ForStatement.MinimumValue = MinimumValueSerializer.Deserialize<VisualScriptTool.Language.Statements.Declaration.Variables.IntegerVariable>(Get<ISerializeObject>(MinimumValueObjectValue, 1));
				}
				else
					ForStatement.MinimumValue = null;
				// MaximumValue
				ISerializeObject MaximumValueObject = Get<ISerializeObject>(Object, 3, null);
				if (MaximumValueObject != null)
				{
					ISerializeObject MaximumValueObjectValue = Get<ISerializeObject>(Object, 3); 
					Serializer MaximumValueSerializer = GetSerializer(System.Type.GetType(Get<string>(MaximumValueObjectValue, 0)));
					ForStatement.MaximumValue = MaximumValueSerializer.Deserialize<VisualScriptTool.Language.Statements.Declaration.Variables.IntegerVariable>(Get<ISerializeObject>(MaximumValueObjectValue, 1));
				}
				else
					ForStatement.MaximumValue = null;
				// StepValue
				ISerializeObject StepValueObject = Get<ISerializeObject>(Object, 4, null);
				if (StepValueObject != null)
				{
					ISerializeObject StepValueObjectValue = Get<ISerializeObject>(Object, 4); 
					Serializer StepValueSerializer = GetSerializer(System.Type.GetType(Get<string>(StepValueObjectValue, 0)));
					ForStatement.StepValue = StepValueSerializer.Deserialize<VisualScriptTool.Language.Statements.Declaration.Variables.IntegerVariable>(Get<ISerializeObject>(StepValueObjectValue, 1));
				}
				else
					ForStatement.StepValue = null;
				// Statement
				ISerializeObject StatementObject = Get<ISerializeObject>(Object, 5, null);
				if (StatementObject != null)
				{
					ISerializeObject StatementObjectValue = Get<ISerializeObject>(Object, 5); 
					Serializer StatementSerializer = GetSerializer(System.Type.GetType(Get<string>(StatementObjectValue, 0)));
					ForStatement.Statement = StatementSerializer.Deserialize<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(StatementObjectValue, 1));
				}
				else
					ForStatement.Statement = null;
				// CompleteStatement
				ISerializeObject CompleteStatementObject = Get<ISerializeObject>(Object, 1, null);
				if (CompleteStatementObject != null)
				{
					ISerializeObject CompleteStatementObjectValue = Get<ISerializeObject>(Object, 1); 
					Serializer CompleteStatementSerializer = GetSerializer(System.Type.GetType(Get<string>(CompleteStatementObjectValue, 0)));
					ForStatement.CompleteStatement = CompleteStatementSerializer.Deserialize<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(CompleteStatementObjectValue, 1));
				}
				else
					ForStatement.CompleteStatement = null;
				return (T)(object)ForStatement;
			}
		}

	}
}