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
		public void Compile()
		{

		}

		public string Compile(Type Type)
		{
			CodeBuilder code = new CodeBuilder();

			bool hasNamespace = !string.IsNullOrEmpty(Type.Namespace);
			uint indent = 0;

			if (hasNamespace)
			{
				code.AppendLine("namespace " + Type.Namespace, indent);
				code.AppendLine("{", indent);

				++indent;
			}

			code.AppendLine(indent);

			if (Type.IsPublic)
				code.Append("public ");
			code.Append("class " + Type.Name + "_Serializer : Serializer");
			code.AppendLine("{", indent);

			//MemberInfo[] members = GetMembers(Type);

			//for (int i = 0; i < members.Length; ++i)
			//{

			//}

			code.AppendLine("}", indent);

			if (hasNamespace)
			{
				--indent;
				code.AppendLine("}", indent);
			}

			return code.ToString();
		}

		private static MemberInfo[] GetMembers(Type Type)
		{
			List<MemberInfo> list = new List<MemberInfo>();

			list.AddRange(TypeUtils.GetProperties(Type));
			list.AddRange(TypeUtils.GetFields(Type));

			return list.ToArray();
		}
	}
}
