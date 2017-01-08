// Generaterd file
using VisualScriptTool.Serialization;
using VisualScriptTool.Reflection;
namespace VisualScriptTool.Editor.Serializers
{
	class Statement_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(VisualScriptTool.Language.Statements.Statement); }
		}

		public override object CreateInstance()
		{
			return Type.GetConstructor(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, new System.Type[] {  }, null).Invoke(new object[] {  });
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
				VisualScriptTool.Language.Statements.Statement[] StatementArray = null;
				if (instanceType.IsArray())
					StatementArray = (VisualScriptTool.Language.Statements.Statement[])Instance;
				else
					StatementArray = ((System.Collections.Generic.List<VisualScriptTool.Language.Statements.Statement>)Instance).ToArray();
				for (int i = 0; i < StatementArray.Length; ++i)
				{
					VisualScriptTool.Language.Statements.Statement element = StatementArray[i];
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
				VisualScriptTool.Language.Statements.Statement Statement = (VisualScriptTool.Language.Statements.Statement)Instance;
				// Name
				Set(Object, 0, Statement.Name);
			}
		}

		public override T Deserialize<T>(ISerializeData Data)
		{
			if (Data == null)
				throw new System.ArgumentNullException("Data cannot be null");
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Language.Statements.Statement[] StatementArray = (VisualScriptTool.Language.Statements.Statement[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 0));
					if (arrayObj == null)
					{
						StatementArray[i] = null;
						continue;
					}
					StatementArray[i] = GetSerializer(targetType).Deserialize<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(arrayObj, 1)); 
				}
				return (T)(object)StatementArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Language.Statements.Statement Statement = (VisualScriptTool.Language.Statements.Statement)CreateInstance();
				// Name
				Statement.Name = Get<System.String>(Object, 0, "");
				return (T)(object)Statement;
			}
		}

	}
}