// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor.Serializers
{
	class IfStatement_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Language.Statements.Control.IfStatement); }
		}

		public override object CreateInstance()
		{
			return new VisualScriptTool.Language.Statements.Control.IfStatement();
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
				VisualScriptTool.Language.Statements.Control.IfStatement[] IfStatementArray = null;
				if (instanceType.IsArray())
					IfStatementArray = (VisualScriptTool.Language.Statements.Control.IfStatement[])Instance;
				else
					IfStatementArray = ((System.Collections.Generic.List<VisualScriptTool.Language.Statements.Control.IfStatement>)Instance).ToArray();
				for (int i = 0; i < IfStatementArray.Length; ++i)
				{
					VisualScriptTool.Language.Statements.Control.IfStatement element = IfStatementArray[i];
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
				VisualScriptTool.Language.Statements.Control.IfStatement IfStatement = (VisualScriptTool.Language.Statements.Control.IfStatement)Instance;
				// Condition
				if (IfStatement.Condition == null)
					Set(Object, 2, null);
				else
				{
					ISerializeObject ConditionObject = AddObject(Object, 2); 
					System.Type ConditionType = IfStatement.Condition.GetType();
					Set(ConditionObject, 0, ConditionType.AssemblyQualifiedName);
					GetSerializer(ConditionType).Serialize(AddObject(ConditionObject, 1), IfStatement.Condition);
				}
				// Statement
				if (IfStatement.Statement == null)
					Set(Object, 3, null);
				else
				{
					ISerializeObject StatementObject = AddObject(Object, 3); 
					System.Type StatementType = IfStatement.Statement.GetType();
					Set(StatementObject, 0, StatementType.AssemblyQualifiedName);
					GetSerializer(StatementType).Serialize(AddObject(StatementObject, 1), IfStatement.Statement);
				}
				// ElseStatment
				if (IfStatement.ElseStatment == null)
					Set(Object, 4, null);
				else
				{
					ISerializeObject ElseStatmentObject = AddObject(Object, 4); 
					System.Type ElseStatmentType = IfStatement.ElseStatment.GetType();
					Set(ElseStatmentObject, 0, ElseStatmentType.AssemblyQualifiedName);
					GetSerializer(ElseStatmentType).Serialize(AddObject(ElseStatmentObject, 1), IfStatement.ElseStatment);
				}
				// CompleteStatement
				if (IfStatement.CompleteStatement == null)
					Set(Object, 1, null);
				else
				{
					ISerializeObject CompleteStatementObject = AddObject(Object, 1); 
					System.Type CompleteStatementType = IfStatement.CompleteStatement.GetType();
					Set(CompleteStatementObject, 0, CompleteStatementType.AssemblyQualifiedName);
					GetSerializer(CompleteStatementType).Serialize(AddObject(CompleteStatementObject, 1), IfStatement.CompleteStatement);
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
				VisualScriptTool.Language.Statements.Control.IfStatement[] IfStatementArray = (VisualScriptTool.Language.Statements.Control.IfStatement[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					if (arrayObj == null)
					{
						IfStatementArray[i] = null;
						continue;
					}
					IfStatementArray[i] = GetSerializer(targetType).Deserialize<VisualScriptTool.Language.Statements.Control.IfStatement>(Get<ISerializeObject>(arrayObj, 1)); 
				}
				return (T)(object)IfStatementArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Language.Statements.Control.IfStatement IfStatement = (VisualScriptTool.Language.Statements.Control.IfStatement)CreateInstance();
				// Condition
				ISerializeObject ConditionObject = Get<ISerializeObject>(Object, 2, null);
				if (ConditionObject != null)
				{
					ISerializeObject ConditionObjectValue = Get<ISerializeObject>(Object, 2); 
					Serializer ConditionSerializer = GetSerializer(System.Type.GetType(Get<string>(ConditionObjectValue, 0)));
					IfStatement.Condition = ConditionSerializer.Deserialize<VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable>(Get<ISerializeObject>(ConditionObjectValue, 1));
				}
				else
					IfStatement.Condition = null;
				// Statement
				ISerializeObject StatementObject = Get<ISerializeObject>(Object, 3, null);
				if (StatementObject != null)
				{
					ISerializeObject StatementObjectValue = Get<ISerializeObject>(Object, 3); 
					Serializer StatementSerializer = GetSerializer(System.Type.GetType(Get<string>(StatementObjectValue, 0)));
					IfStatement.Statement = StatementSerializer.Deserialize<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(StatementObjectValue, 1));
				}
				else
					IfStatement.Statement = null;
				// ElseStatment
				ISerializeObject ElseStatmentObject = Get<ISerializeObject>(Object, 4, null);
				if (ElseStatmentObject != null)
				{
					ISerializeObject ElseStatmentObjectValue = Get<ISerializeObject>(Object, 4); 
					Serializer ElseStatmentSerializer = GetSerializer(System.Type.GetType(Get<string>(ElseStatmentObjectValue, 0)));
					IfStatement.ElseStatment = ElseStatmentSerializer.Deserialize<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(ElseStatmentObjectValue, 1));
				}
				else
					IfStatement.ElseStatment = null;
				// CompleteStatement
				ISerializeObject CompleteStatementObject = Get<ISerializeObject>(Object, 1, null);
				if (CompleteStatementObject != null)
				{
					ISerializeObject CompleteStatementObjectValue = Get<ISerializeObject>(Object, 1); 
					Serializer CompleteStatementSerializer = GetSerializer(System.Type.GetType(Get<string>(CompleteStatementObjectValue, 0)));
					IfStatement.CompleteStatement = CompleteStatementSerializer.Deserialize<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(CompleteStatementObjectValue, 1));
				}
				else
					IfStatement.CompleteStatement = null;
				return (T)(object)IfStatement;
			}
		}

	}
}