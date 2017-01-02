// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using VisualScriptTool.Reflection;

namespace VisualScriptTool.Serialization
{
	public sealed class SchemaCompiler
	{
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
			uint indent = 0;

			if (hasNamespace)
			{
				header.Append("// Generaterd file");
				header.AppendLine("using VisualScriptTool.Serialization;", indent);
				header.AppendLine("namespace " + Type.Namespace, indent);
				header.AppendLine("{", indent);

				++indent;
			}

			header.AppendLine(indent);

			if (Type.IsPublic)
				header.Append("public ");
			header.Append("class " + Type.Name + "_Serializer : Serializer");
			header.AppendLine("{", indent);

			++indent;
			serialize.AppendLine("public void Serialize(ISerializeObject Object, " + typeName + " Instance)", indent);
			serialize.AppendLine("{", indent);
			deserialize.AppendLine("public void Deserialize(ISerializeObject Object, " + typeName + " Instance)", indent);
			deserialize.AppendLine("{", indent);

			MemberInfo[] members = Strategy.GetMembers(Type);

			++indent;
			for (int i = 0; i < members.Length; ++i)
			{
				MemberInfo member = members[i];
				Type memberType = (member is FieldInfo ? ((FieldInfo)member).FieldType : ((PropertyInfo)member).PropertyType);

				serialize.AppendLine("Object.Set(\"" + i + "\", Instance." + member.Name + ");", indent);

				deserialize.AppendLine("Instance." + member.Name + " = Object.Get<" + memberType.FullName + ">(\"" + i + "\");", indent);
			}
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
			code.AppendLine(serialize.ToString());
			code.AppendLine(deserialize.ToString());
			code.AppendLine(footer.ToString());

			return code.ToString();
		}
	}
}
