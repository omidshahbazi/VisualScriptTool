// This File Has Generated By Compiler, Do Not Change It Manually
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
				VisualScriptTool.Language.Statements.Statement[] StatementArray = null;
				if (InstanceType.IsArray())
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
						Set(elementObject, 1, elementType.AssemblyQualifiedName);
						GetSerializer(elementType).SerializeInternal(AddObject(elementObject, 2), element, elementType, References); 
					}
				}
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Language.Statements.Statement Statement = (VisualScriptTool.Language.Statements.Statement)Instance;
			}
		}

		public override T DeserializeInternal<T>(ISerializeData Data, GUIDTable References, ResolverList ResolverList)
		{
			T returnValue = default(T);
			if (Data is ISerializeArray)
			{
				ISerializeArray Array = (ISerializeArray)Data; 
				VisualScriptTool.Language.Statements.Statement[] StatementArray = (VisualScriptTool.Language.Statements.Statement[])System.Array.CreateInstance(Type, Array.Count);
				for (uint i = 0; i < Array.Count; ++i)
				{
					ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);
					System.Type targetType = System.Type.GetType(Get<string>(arrayObj, 1));
					if (arrayObj == null)
					{
						StatementArray[i] = null;
						continue;
					}
					StatementArray[i] = GetSerializer(targetType).DeserializeInternal<VisualScriptTool.Language.Statements.Statement>(Get<ISerializeObject>(arrayObj, 2), References, ResolverList); 
				}
				returnValue = (T)(object)StatementArray;
			}
			else
			{
				ISerializeObject Object = (ISerializeObject)Data; 
				VisualScriptTool.Language.Statements.Statement Statement = (VisualScriptTool.Language.Statements.Statement)CreateInstance();
				returnValue = (T)(object)Statement;
			}
			return returnValue;
		}

	}
}