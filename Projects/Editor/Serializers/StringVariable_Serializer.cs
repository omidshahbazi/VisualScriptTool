// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor.Serializers
{
	class StringVariable_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable); }
		}

		public override object CreateInstance()
		{
			return new VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable();
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
				VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable[] StringVariableArray = null;
				if (instanceType.IsArray())
					StringVariableArray = (VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable[])Instance;
				else
					StringVariableArray = ((System.Collections.Generic.List<VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable>)Instance).ToArray();
				for (int i = 0; i < StringVariableArray.Length; ++i)
				{
					VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable element = StringVariableArray[i];
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
				VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable StringVariable = (VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable)Instance;
				// Value
				Set(Object, 2, StringVariable.Value);
			}
		}

		public override T Deserialize<T>(ISerializeData Data)
		{
			if (Data == null)
				throw new System.ArgumentNullException("Data cannot be null");
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable[] StringVariableArray = (VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					if (arrayObj == null)
					{
						StringVariableArray[i] = null;
						continue;
					}
					StringVariableArray[i] = GetSerializer(targetType).Deserialize<VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable>(Get<ISerializeObject>(arrayObj, 1)); 
				}
				return (T)(object)StringVariableArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable StringVariable = (VisualScriptTool.Language.Statements.Declaration.Variables.StringVariable)CreateInstance();
				// Value
				StringVariable.Value = Get<System.String>(Object, 2, "");
				return (T)(object)StringVariable;
			}
		}

	}
}