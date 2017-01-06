// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;
using VisualScriptTool.Reflection;

namespace VisualScriptTool.Serialization
{
	public sealed class SerializerCompiler
	{
		private const string CREATE_INSTANCE_METHOD_NAME = "CreateInstance";
		private const string SERIALIZE_METHOD_NAME = "Serialize";
		private const string DESERIALIZE_METHOD_NAME = "Deserialize";
		private const int DATA_STRUCTURE_TYPE_ID = 0;
		private const int DATA_STRUCTURE_VALUE_ID = 1;

		private enum ValueType
		{
			Primitive = 0,
			Array,
			List,
			Map,
			DataStructure
		}

		private uint indent;

		public ICompileStrategy Strategy
		{
			get;
			set;
		}

		public SerializerCompiler()
		{
			Strategy = new DefaultCompileStrategy();
		}

		public string Compile(Type Type, string Name = "")
		{
			CodeBuilder header = new CodeBuilder();
			CodeBuilder footer = new CodeBuilder();
			CodeBuilder instatiator = new CodeBuilder();
			CodeBuilder serialize = new CodeBuilder();
			CodeBuilder deserialize = new CodeBuilder();

			string typeName = Type.FullName;
			bool hasNamespace = !string.IsNullOrEmpty(Type.Namespace);
			indent = 0;

			if (hasNamespace)
			{
				header.Append("// Generaterd file");
				header.AppendLine("using VisualScriptTool.Serialization;", indent);
				header.AppendLine("using VisualScriptTool.Reflection;", indent);
				header.AppendLine("namespace " + Type.Namespace, indent);
				header.AppendLine("{", indent);

				++indent;
			}

			header.AppendLine(indent);

			header.Append("class " + (string.IsNullOrEmpty(Name) ? Type.Name + "_Serializer" : Name) + " : Serializer");
			header.AppendLine("{", indent);
			++indent;

			header.AppendLine("public override System.Type Type", indent);
			header.AppendLine("{", indent);
			header.AppendLine("get { return typeof(" + typeName + "); }", ++indent);
			header.AppendLine("}", --indent);

			instatiator.AppendLine("public override object " + CREATE_INSTANCE_METHOD_NAME + "()", indent);
			instatiator.AppendLine("{", indent);

			MethodBase instantiatorMethod = Strategy.GetInstantiator(Type);

			++indent;
			if (!(instantiatorMethod is ConstructorInfo) && !(instantiatorMethod is MethodInfo))
			{
				instatiator.AppendLine("return null;", indent);
			}
			else
			{
				ParameterInfo[] parameters = instantiatorMethod.GetParameters();
				uint constantParametersCount = 0;

				instatiator.AppendLine("return ", indent);

				if (instantiatorMethod.IsPublic)
				{
					if (instantiatorMethod is ConstructorInfo)
						instatiator.Append("new " + instantiatorMethod.DeclaringType.FullName + "(");
					else if (instantiatorMethod is MethodInfo)
					{
						constantParametersCount = 1;
						instatiator.Append(instantiatorMethod.DeclaringType.FullName + "." + instantiatorMethod.Name + "(Type");
					}
				}
				else
				{
					if (instantiatorMethod is ConstructorInfo)
					{
						instatiator.Append("Type.GetConstructor(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags." + (instantiatorMethod.IsPublic ? "Public" : "NonPublic") + ", null, new System.Type[] { ");

						for (int i = 0; i < parameters.Length; ++i)
						{
							if (i != 0)
								instatiator.Append(", ");
							instatiator.Append("System.Type.GetType(\"" + parameters[i].ParameterType.FullName + "\")");
						}

						instatiator.Append(" }, null).Invoke(new object[] { ");
					}
					else if (instantiatorMethod is MethodInfo)
					{
						constantParametersCount = 1;

						if (instantiatorMethod.DeclaringType == Type)
							instatiator.Append("Type");
						else
							instatiator.Append("System.Type.GetType(\"" + instantiatorMethod.DeclaringType.FullName + "\")");

						instatiator.Append(".GetMethod(\"" + instantiatorMethod.Name + "\", ");
						instatiator.Append("System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags." + (instantiatorMethod.IsPublic ? "Public" : "NonPublic"));
						instatiator.Append(").Invoke(null, new object[] { Type");
					}
				}

				if (parameters.Length != 0)
				{
					for (uint i = constantParametersCount; i < parameters.Length; ++i)
					{
						if (i != 0)
							instatiator.Append(", ");

						instatiator.Append(Strategy.GetInstantiatorParameterDefaultValue(instantiatorMethod, i - constantParametersCount));
					}
				}

				if (!instantiatorMethod.IsPublic)
					instatiator.Append(" }");

				instatiator.Append(");");
			}

			instatiator.AppendLine("}", --indent);

			string typeArrayName = GetTypeVariableName(Type) + "Array";

			serialize.AppendLine("public override void " + SERIALIZE_METHOD_NAME + "(ISerializeData Data, object Instance)", indent);
			serialize.AppendLine("{", indent);
			++indent;
			AppendGaurd(Type, serialize);

			serialize.AppendLine("if (instanceType.IsArrayOrList())", indent);
			serialize.AppendLine("{", indent);

			serialize.AppendLine("ISerializeArray Array = (ISerializeArray)Data; ", ++indent);
			serialize.AppendLine(Type.FullName + "[] " + typeArrayName + " = null;", indent);

			serialize.AppendLine("if (instanceType.IsArray())", indent);
			serialize.AppendLine(typeArrayName + " = (" + Type.FullName + "[])Instance;", ++indent);
			--indent;
			serialize.AppendLine("else", indent);
			serialize.AppendLine(typeArrayName + " = ((System.Collections.Generic.List<" + Type.FullName + ">)Instance).ToArray();", ++indent);
			--indent;

			serialize.AppendLine("for (int i = 0; i < " + typeArrayName + ".Length; ++i)", indent);
			serialize.AppendLine("{", indent);

			serialize.AppendLine(Type.FullName + " element = " + typeArrayName + "[i];", ++indent);

			if (!Type.IsValueType)
			{
				serialize.AppendLine("if (element == null)", indent);
				serialize.AppendLine("Add(Array, null);", ++indent);
				--indent;
				serialize.AppendLine("else", indent);
				serialize.AppendLine("{", indent);
				++indent;
			}

			serialize.AppendLine("ISerializeObject elementObject = AddObject(Array); ", indent);
			serialize.AppendLine("System.Type elementType = element.GetType();", indent);
			serialize.AppendLine("Set(elementObject, " + DATA_STRUCTURE_TYPE_ID + ", elementType.FullName);", indent);
			serialize.AppendLine("GetSerializer(elementType)." + SERIALIZE_METHOD_NAME + "(AddObject(elementObject, " + DATA_STRUCTURE_VALUE_ID + "), element); ", indent);

			if (!Type.IsValueType)
				serialize.AppendLine("}", --indent);

			serialize.AppendLine("}", --indent);

			--indent;

			serialize.AppendLine("}", indent);
			serialize.AppendLine("else", indent);
			serialize.AppendLine("{", indent);

			serialize.AppendLine("ISerializeObject Object = (ISerializeObject)Data; ", ++indent);
			serialize.AppendLine(Type.FullName + " " + GetTypeVariableName(Type) + " = (" + Type.FullName + ")Instance;", indent);
			--indent;
			--indent;

			deserialize.AppendLine("public override void " + DESERIALIZE_METHOD_NAME + "(ISerializeData Data, object Instance)", indent);
			deserialize.AppendLine("{", indent);
			++indent;
			AppendGaurd(Type, deserialize);

			deserialize.AppendLine("if (instanceType.IsArrayOrList())", indent);
			deserialize.AppendLine("{", indent);

			deserialize.AppendLine("System.Type elementType = (instanceType.IsArray() ? instanceType.GetArrayElementType() : instanceType.GetListElementType());", ++indent);

			deserialize.AppendLine("ISerializeArray Array = (ISerializeArray)Data; ", indent);
			deserialize.AppendLine(Type.FullName + "[] " + typeArrayName + " = null;", indent);

			deserialize.AppendLine("if (instanceType.IsArray())", indent);
			deserialize.AppendLine(typeArrayName + " = (" + Type.FullName + "[])Instance;", ++indent);
			--indent;
			deserialize.AppendLine("else", indent);
			deserialize.AppendLine(typeArrayName + " = (" + Type.FullName + "[])System.Array.CreateInstance(instanceType, Array.Count);", ++indent);
			--indent;

			deserialize.AppendLine("for (uint i = 0; i < Array.Count; ++i)", indent);
			deserialize.AppendLine("{", indent);
			deserialize.AppendLine("ISerializeObject arrayObj = Get<ISerializeObject>(Array, i);", ++indent);
			deserialize.AppendLine("System.Type targetType = System.Type.GetType(Get<string>(arrayObj, " + DATA_STRUCTURE_TYPE_ID + "));", indent);
			if (!Type.IsValueType)
			{
				deserialize.AppendLine("if (arrayObj == null)", indent);
				deserialize.AppendLine("{", indent);
				deserialize.AppendLine(typeArrayName + "[i] = null;", ++indent);
				deserialize.AppendLine("continue;", indent);
				deserialize.AppendLine("}", --indent);
				deserialize.AppendLine("if (" + typeArrayName + "[i] == null)", indent);
				deserialize.AppendLine(typeArrayName + "[i] = (" + Type.FullName + ")GetSerializer(targetType)." + CREATE_INSTANCE_METHOD_NAME + "();", ++indent);
				--indent;
			}


			deserialize.AppendLine("GetSerializer(targetType)." + DESERIALIZE_METHOD_NAME + "(Get<ISerializeObject>(arrayObj, " + DATA_STRUCTURE_VALUE_ID + "), " + typeArrayName + "[i]); ", indent);

			--indent;
			deserialize.AppendLine("}", indent);

			--indent;

			deserialize.AppendLine("}", indent);
			deserialize.AppendLine("else", indent);
			deserialize.AppendLine("{", indent);

			deserialize.AppendLine("ISerializeObject Object = (ISerializeObject)Data; ", ++indent);
			deserialize.AppendLine(Type.FullName + " " + GetTypeVariableName(Type) + " = (" + Type.FullName + ")Instance;", indent);
			--indent;

			CompileMembers(Type, serialize, deserialize);

			serialize.AppendLine("}", indent);
			deserialize.AppendLine("}", indent);

			--indent;
			serialize.AppendLine("}", indent);
			deserialize.AppendLine("}", indent);

			--indent;
			footer.AppendLine("}", indent);

			if (hasNamespace)
			{
				--indent;
				footer.AppendLine("}", indent);
			}

			CodeBuilder code = new CodeBuilder();
			code.Append(header.ToString());
			code.AppendLine(instatiator.ToString());
			code.AppendLine(serialize.ToString());
			code.AppendLine(deserialize.ToString());
			code.AppendLine(footer.ToString());

			return code.ToString();
		}

		private void CompileMembers(Type Type, CodeBuilder SerializeMethod, CodeBuilder DeserializeMethod)
		{
			MemberInfo[] members = Strategy.GetMembers(Type);
			List<int> ids = new List<int>();

			++indent;
			for (int i = 0; i < members.Length; ++i)
			{
				MemberInfo member = members[i];

				SerializeMethod.AppendLine("// " + member.Name, indent);
				DeserializeMethod.AppendLine("// " + member.Name, indent);

				int id = Strategy.GetMemberID(member, i);

				if (ids.Contains(id))
					throw new ArgumentException("ID [" + id + "] used more than once");

				ids.Add(id);

				HandleMember(member, id, SerializeMethod, DeserializeMethod, "Object");
			}

			--indent;
		}

		private void HandleMember(MemberInfo Member, int ID, CodeBuilder SerializeMethod, CodeBuilder DeserializeMethod, string ObjectName)
		{
			Type memberType = (Member is FieldInfo ? ((FieldInfo)Member).FieldType : ((PropertyInfo)Member).PropertyType);
			string memberAccessName = GetTypeVariableName(Member.ReflectedType) + "." + Member.Name;

			ValueType valueType = GetValueType(memberType);

			if (valueType == ValueType.Primitive)
				AppendPrimitive(Member, memberType, ID, memberAccessName, SerializeMethod, DeserializeMethod, ObjectName);
			else if (valueType == ValueType.Array)
				AppendArray(Member, memberType, ID, memberAccessName, SerializeMethod, DeserializeMethod, ObjectName, valueType);
			//else if (valueType == ValueType.Array || valueType == ValueType.List)
			//	AppendArray(Member, memberType, ID, memberAccessName, SerializeMethod, DeserializeMethod, ObjectName, valueType);
			//else if (valueType == ValueType.Map)
			//{

			//}
			else if (valueType == ValueType.DataStructure)
				AppendDataStructure(Member, memberType, ID, memberAccessName, SerializeMethod, DeserializeMethod, ObjectName);
		}

		private void AppendPrimitive(MemberInfo Member, Type MemberType, int ID, string MemberAccessName, CodeBuilder SerializeMethod, CodeBuilder DeserializeMethod, string ObjectName)
		{
			SerializeMethod.AppendLine("Set(" + ObjectName + ", " + ID + ", " + MemberAccessName + ");", indent);

			DeserializeMethod.AppendLine(MemberAccessName + " = Get<" + MemberType.FullName + ">(" + ObjectName + ", " + ID + ", " + Strategy.GetMemberDefaultValue(Member) + ");", indent);
		}

		private void AppendArray(MemberInfo Member, Type MemberType, int ID, string MemberAccessName, CodeBuilder SerializeMethod, CodeBuilder DeserializeMethod, string ObjectName, ValueType ValueType)
		{
			Type elementType = (ValueType == ValueType.Array ? MemberType.GetArrayElementType() : MemberType.GetListElementType());

			SerializeMethod.AppendLine("if (" + MemberAccessName + " == null)", indent);
			SerializeMethod.AppendLine("Set(" + ObjectName + ", " + ID + ", null);", ++indent);
			SerializeMethod.AppendLine("else", --indent);
			SerializeMethod.AppendLine("{", indent);

			string arrayName = Member.Name + "Array";

			SerializeMethod.AppendLine("ISerializeArray " + arrayName + " = AddArray(" + ObjectName + ", " + ID + ");", ++indent);
			SerializeMethod.AppendLine("for (int i = 0; i < " + MemberAccessName + "." + (ValueType == ValueType.Array ? "Length" : "Count") + "; ++i)", indent);
			SerializeMethod.AppendLine("{", indent);

			ValueType elementValueType = GetValueType(elementType);

			++indent;

			SerializeMethod.AppendLine(elementType.FullName + " element = " + MemberAccessName + "[i];", indent);

			SerializeMethod.AppendLine("if (element == null)", indent);
			SerializeMethod.AppendLine("{", indent);
			SerializeMethod.AppendLine("Add(" + arrayName + ", null);", ++indent);
			SerializeMethod.AppendLine("continue;", indent);
			SerializeMethod.AppendLine("}", --indent);

			if (elementValueType == ValueType.Primitive)
				SerializeMethod.AppendLine("Add(" + arrayName + ", element);", indent);
			//else if (elementValueType == ValueType.Array || elementValueType == ValueType.List)
			//{

			//}
			//else if (elementValueType == ValueType.Map)
			//{

			//}
			else if (elementValueType == ValueType.DataStructure)
			{
				string arrayObjectName = Member.Name + "ArrayObject";
				SerializeMethod.AppendLine("ISerializeObject " + arrayObjectName + " = AddObject(" + arrayName + "); ", indent);
				SerializeMethod.AppendLine("System.Type elementType = element.GetType();", indent);
				SerializeMethod.AppendLine("Set(" + arrayObjectName + ", " + DATA_STRUCTURE_TYPE_ID + ", elementType.FullName);", indent);
				SerializeMethod.AppendLine("GetSerializer(elementType)." + SERIALIZE_METHOD_NAME + "(AddObject(" + arrayObjectName + ", " + DATA_STRUCTURE_VALUE_ID + "), element); ", indent);
			}

			SerializeMethod.AppendLine("}", --indent);

			SerializeMethod.AppendLine("}", --indent);

			DeserializeMethod.AppendLine("ISerializeArray " + arrayName + " = Get<ISerializeArray>(" + ObjectName + ", " + ID + ", null);", indent);
			DeserializeMethod.AppendLine("if (" + arrayName + " == null)", indent);
			DeserializeMethod.AppendLine(MemberAccessName + " = null;", ++indent);
			DeserializeMethod.AppendLine("else", --indent);
			DeserializeMethod.AppendLine("{", indent);

			++indent;
			if (ValueType == ValueType.Array)
				DeserializeMethod.AppendLine(MemberAccessName + " = (" + MemberType.FullName + ")System.Array.CreateInstance(typeof(" + elementType.FullName + "), " + arrayName + ".Count);", indent);
			else
				DeserializeMethod.AppendLine("// " + MemberAccessName + " Allocation", indent);

			DeserializeMethod.AppendLine("for (uint i = 0; i < " + arrayName + ".Count" + "; ++i)", indent);
			DeserializeMethod.AppendLine("{", indent);

			++indent;
			if (elementValueType == ValueType.Primitive)
			{
				if (ValueType == ValueType.Array)
					DeserializeMethod.AppendLine(MemberAccessName + "[(int)i] = " + arrayName + ".Get<" + elementType.FullName + ">(i);", indent);
				else
					DeserializeMethod.AppendLine(MemberAccessName + ".Add(" + arrayName + ".Get<" + elementType.FullName + ">(i));", indent);
			}
			//else if (elementValueType == ValueType.Array || elementValueType == ValueType.List)
			//{

			//}
			//else if (elementValueType == ValueType.Map)
			//{

			//}
			else if (elementValueType == ValueType.DataStructure)
			{
				if (ValueType == ValueType.Array)
					DeserializeMethod.AppendLine(MemberAccessName + "[(int)i] = (" + elementType.FullName + ")GetSerializer(" + MemberAccessName + ".GetType())." + CREATE_INSTANCE_METHOD_NAME + "();", indent);
				else
					DeserializeMethod.AppendLine("// " + MemberAccessName + ".Add(Allocated);", indent);

				DeserializeMethod.AppendLine("GetSerializer(" + MemberAccessName + ".GetType())." + DESERIALIZE_METHOD_NAME + "(Get<ISerializeObject>(" + arrayName + ", i), " + MemberAccessName + "[(int)i]);", indent);
			}

			DeserializeMethod.AppendLine("}", --indent);

			DeserializeMethod.AppendLine("}", --indent);
		}

		private void AppendDataStructure(MemberInfo Member, Type MemberType, int ID, string MemberAccessName, CodeBuilder SerializeMethod, CodeBuilder DeserializeMethod, string ObjectName)
		{
			string objectName = Member.Name + "Object";

			if (!MemberType.IsValueType)
			{
				SerializeMethod.AppendLine("if (" + MemberAccessName + " == null)", indent);
				SerializeMethod.AppendLine("Set(" + ObjectName + ", " + ID + ", null);", ++indent);
				SerializeMethod.AppendLine("else", --indent);
				SerializeMethod.AppendLine("{", indent);
				++indent;
			}

			SerializeMethod.AppendLine("ISerializeObject " + objectName + " = AddObject(" + ObjectName + ", " + ID + "); ", indent);
			SerializeMethod.AppendLine("System.Type " + Member.Name + "Type = " + MemberAccessName + ".GetType();", indent);
			SerializeMethod.AppendLine("Set(" + objectName + ", " + DATA_STRUCTURE_TYPE_ID + ", " + Member.Name + "Type.FullName);", indent);
			SerializeMethod.AppendLine("GetSerializer(" + Member.Name + "Type)." + SERIALIZE_METHOD_NAME + "(AddObject(" + objectName + ", " + DATA_STRUCTURE_VALUE_ID + "), " + MemberAccessName + ");", indent);

			if (!MemberType.IsValueType)
				SerializeMethod.AppendLine("}", --indent);

			DeserializeMethod.AppendLine("ISerializeObject " + objectName + " = Get<ISerializeObject>(" + ObjectName + ", " + ID + ", null);", indent);

			DeserializeMethod.AppendLine("if (" + objectName + " != null)", indent);
			DeserializeMethod.AppendLine("{", indent);

			string serializerName = Member.Name + "Serializer";
			objectName += "Value";
			DeserializeMethod.AppendLine("ISerializeObject " + objectName + " = Get<ISerializeObject>(" + ObjectName + ", " + ID + "); ", ++indent);
			DeserializeMethod.AppendLine("Serializer " + serializerName + " = GetSerializer(System.Type.GetType(Get<string>(" + objectName + ", " + DATA_STRUCTURE_TYPE_ID + ")));", indent);
			DeserializeMethod.AppendLine(MemberAccessName + " = (" + MemberType.FullName + ")" + serializerName + "." + CREATE_INSTANCE_METHOD_NAME + "();", indent);
			DeserializeMethod.AppendLine(serializerName + "." + DESERIALIZE_METHOD_NAME + "(Get<ISerializeObject>(" + objectName + ", " + DATA_STRUCTURE_VALUE_ID + "), " + MemberAccessName + ");", indent);

			DeserializeMethod.AppendLine("}", --indent);
			if (!MemberType.IsValueType)
			{
				DeserializeMethod.AppendLine("else", indent);
				DeserializeMethod.AppendLine(MemberAccessName + " = null;", ++indent);
				--indent;
			}
		}

		private void AppendGaurd(Type Type, CodeBuilder Builder)
		{
			Builder.AppendLine("if (Data == null || Instance == null)", indent);
			Builder.AppendLine("throw new System.ArgumentNullException(\"Data and Instance cannot be null\");", ++indent);
			--indent;

			Builder.AppendLine("System.Type instanceType = Instance.GetType();", indent);
			Builder.AppendLine("if (instanceType.IsArray())", indent);
			Builder.AppendLine("instanceType = instanceType.GetArrayElementType();", ++indent);
			--indent;
			Builder.AppendLine("else if (instanceType.IsList())", indent);
			Builder.AppendLine("instanceType = instanceType.GetListElementType();", ++indent);
			--indent;

			Builder.AppendLine("if (Type != instanceType)", indent);
			Builder.AppendLine("throw new System.InvalidCastException(\"Expected [\" + Type.FullName + \"]\");", ++indent);
			--indent;

			Builder.AppendLine("instanceType = Instance.GetType();", indent);

			Builder.AppendLine("if ((Data is ISerializeObject && instanceType.IsArrayOrList()) || (Data is ISerializeArray && !instanceType.IsArrayOrList()))", indent);
			Builder.AppendLine("throw new System.ArgumentException(\"Data and Instance mismatch [\" + Type.FullName + \"]\");", ++indent);
			--indent;
		}

		private ValueType GetValueType(Type Type)
		{
			if (Strategy.IsPrimitive(Type))
				return ValueType.Primitive;
			else if (Strategy.IsArray(Type))
				return ValueType.Array;
			else if (Strategy.IsList(Type))
				return ValueType.List;
			else if (Strategy.IsMap(Type))
				return ValueType.Map;

			return ValueType.DataStructure;
		}

		private static string GetTypeVariableName(Type Type)
		{
			return Type.Name;
		}
	}
}