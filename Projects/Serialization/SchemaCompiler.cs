// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public sealed class SchemaCompiler
	{
		private const string SERIALIZE_METHOD_NAME = "Serialize";
		private const string DESERIALIZE_METHOD_NAME = "Deserialize";

		private uint indent;

		public ICompileStrategy Strategy
		{
			get;
			set;
		}

		public SchemaCompiler()
		{
			Strategy = new DefaultCompileStrategy();
		}

		public void Compile()
		{
		}

		public string Compile(Type Type)
		{
			CodeBuilder header = new CodeBuilder();
			CodeBuilder footer = new CodeBuilder();
			CodeBuilder serialize = new CodeBuilder();
			CodeBuilder deserialize = new CodeBuilder();

			string typeName = Type.Name;
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

			header.Append("static class " + GetSchemaName(Type));
			header.AppendLine("{", indent);

			++indent;
			serialize.AppendLine("public static void " + SERIALIZE_METHOD_NAME + "(ISerializeObject Object, " + typeName + " Instance)", indent);
			serialize.AppendLine("{", indent);
			deserialize.AppendLine("public static void " + DESERIALIZE_METHOD_NAME + "(ISerializeObject Object, " + typeName + " Instance)", indent);
			deserialize.AppendLine("{", indent);

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

				if (!Strategy.IsSerializableMember(member))
					continue;

				int id = Strategy.GetMemberID(member, i);

				if (ids.Contains(id))
					throw new ArgumentException("ID [" + id + "] used more than once");

				ids.Add(id);

				Type memberType = (member is FieldInfo ? ((FieldInfo)member).FieldType : ((PropertyInfo)member).PropertyType);
				string memberAccessName = "Instance." + member.Name;

				if (Strategy.IsPrimitive(memberType))
				{
					SerializeMethod.AppendLine("Serializer.Set(Object, " + id + ", " + memberAccessName + ");", indent);

					DeserializeMethod.AppendLine(memberAccessName + " = Serializer.Get<" + memberType.FullName + ">(Object, " + id + ", " + Strategy.GetMemberDefaultValue(member) + ");", indent);
				}
				else if (Strategy.IsArray(memberType))
				{
					string arrayName = member.Name + "Array";

					SerializeMethod.AppendLine("if (" + memberAccessName + " == null)", indent);
					SerializeMethod.AppendLine("Serializer.Set(Object, " + id + ", null);", ++indent);
					SerializeMethod.AppendLine("else", --indent);
					SerializeMethod.AppendLine("{", indent);
					++indent;

					SerializeMethod.AppendLine("System.Array " + arrayName + " = (System.Array)" + memberAccessName + ";", indent);
					SerializeMethod.AppendLine("for (int i = 0; i < " + arrayName + ".Length; ++i)", indent);
					SerializeMethod.AppendLine("{", indent);

					++indent;
				   --indent;

					SerializeMethod.AppendLine("}", indent);

					--indent;
					SerializeMethod.AppendLine("}", indent);

				}
				else if (Strategy.IsList(memberType))
				{

				}
				else if (Strategy.IsMap(memberType))
				{

				}
				else
				{
					SerializeMethod.AppendLine("if (" + memberAccessName + " == null)", indent);
					SerializeMethod.AppendLine("Serializer.Set(Object, " + id + ", null);", ++indent);
					SerializeMethod.AppendLine("else", --indent);
					SerializeMethod.AppendLine(GetSchemaName(memberType) + "." + SERIALIZE_METHOD_NAME + "(Serializer.AddObject(Object, " + id + "), " + memberAccessName + ");", ++indent);
					--indent;

					// deserialize
				}
			}
			--indent;
		}

		private static string GetSchemaName(Type Type)
		{
			return Type.Name + "_Schema";
		}
	}
}
