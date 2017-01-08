// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor.Serializers
{
	class BooleanVariable_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable); }
		}

		public override object CreateInstance()
		{
			return new VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable();
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
				VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable[] BooleanVariableArray = null;
				if (instanceType.IsArray())
					BooleanVariableArray = (VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable[])Instance;
				else
					BooleanVariableArray = ((System.Collections.Generic.List<VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable>)Instance).ToArray();
				for (int i = 0; i < BooleanVariableArray.Length; ++i)
				{
					VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable element = BooleanVariableArray[i];
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
				VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable BooleanVariable = (VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable)Instance;
				// Value
				Set(Object, 2, BooleanVariable.Value);
			}
		}

		public override T Deserialize<T>(ISerializeData Data)
		{
			if (Data == null)
				throw new System.ArgumentNullException("Data cannot be null");
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable[] BooleanVariableArray = (VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					if (arrayObj == null)
					{
						BooleanVariableArray[i] = null;
						continue;
					}
					BooleanVariableArray[i] = GetSerializer(targetType).Deserialize<VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable>(Get<ISerializeObject>(arrayObj, 1)); 
				}
				return (T)(object)BooleanVariableArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable BooleanVariable = (VisualScriptTool.Language.Statements.Declaration.Variables.BooleanVariable)CreateInstance();
				// Value
				BooleanVariable.Value = Get<System.Boolean>(Object, 2, false);
				return (T)(object)BooleanVariable;
			}
		}

	}
}