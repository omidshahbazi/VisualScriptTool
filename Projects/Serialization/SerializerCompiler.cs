// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;

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

		public string Compile(Type Type)
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
				header.AppendLine("namespace " + Type.Namespace, indent);
				header.AppendLine("{", indent);

				++indent;
			}

			header.AppendLine(indent);

			header.Append("class " + GetSerializerName(Type) + " : Serializer");
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

			serialize.AppendLine("public override void " + SERIALIZE_METHOD_NAME + "(ISerializeObject Object, object Instance)", indent);
			serialize.AppendLine("{", indent);
			AppendTypeCast(Type, serialize);

			deserialize.AppendLine("public override void " + DESERIALIZE_METHOD_NAME + "(ISerializeObject Object, object Instance)", indent);
			deserialize.AppendLine("{", indent);
			AppendTypeCast(Type, deserialize);

			CompileMembers(Type, serialize, deserialize);

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
			string memberAccessName = GetTypeVariableName(Member.DeclaringType) + "." + Member.Name;

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
			Type elementType = (ValueType == ValueType.Array ? GetArrayElementType(MemberType) : GetListElementType(MemberType));

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
				SerializeMethod.AppendLine("Set(" + arrayObjectName + ", " + DATA_STRUCTURE_TYPE_ID + ", elementType.GUID.ToString());", indent);
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

			SerializeMethod.AppendLine("if (" + MemberAccessName + " == null)", indent);
			SerializeMethod.AppendLine("Set(" + ObjectName + ", " + ID + ", null);", ++indent);
			SerializeMethod.AppendLine("else", --indent);
			SerializeMethod.AppendLine("{", indent);
			SerializeMethod.AppendLine("ISerializeObject " + objectName + " = AddObject(" + ObjectName + ", " + ID + "); ", ++indent);
			SerializeMethod.AppendLine("System.Type type = " + MemberAccessName + ".GetType();", indent);
			SerializeMethod.AppendLine("Set(" + objectName + ", " + DATA_STRUCTURE_TYPE_ID + ", type.FullName);", indent);
			SerializeMethod.AppendLine("GetSerializer(type)." + SERIALIZE_METHOD_NAME + "(AddObject(" + objectName + ", " + DATA_STRUCTURE_VALUE_ID + "), " + MemberAccessName + ");", indent);
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

		private void AppendTypeCast(Type Type, CodeBuilder Builder)
		{
			Builder.AppendLine("if (Type != Instance.GetType())", ++indent);
			Builder.AppendLine("throw new System.InvalidCastException(\"Expected [\" + Type.FullName + \"]\");", ++indent);
			--indent;

			Builder.AppendLine(Type.FullName + " " + GetTypeVariableName(Type) + " = (" + Type.FullName + ")Instance;", indent);
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

		private static string GetSerializerName(Type Type)
		{
			return Type.Name + "_Serializer";
		}

		private static string GetTypeVariableName(Type Type)
		{
			return Type.Name;
		}

		private static Type GetArrayElementType(Type Type)
		{
			return (Type.HasElementType ? Type.GetElementType() : null);
		}

		private static Type GetListElementType(Type Type)
		{
			return Type.GetGenericArguments()[0];
		}
	}
}