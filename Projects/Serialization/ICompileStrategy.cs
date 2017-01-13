// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public interface ICompileStrategy
	{
		MethodBase GetInstantiator(Type Type);
		MethodInfo GetPreSerialize(Type Type);
		MethodInfo GetPostSerialize(Type Type);
		MethodInfo GetPreDeserialize(Type Type);
		MethodInfo GetPostDeserialize(Type Type);

		MemberInfo[] GetMembers(Type Type);

		int GetMemberID(MemberInfo Member, int DefaultID);
		string GetMemberDefaultValue(MemberInfo Member);
		string GetInstantiatorParameterDefaultValue(MethodBase Method, uint Index);

		bool IsPrimitive(Type Type);
		bool IsArray(Type Type);
		bool IsList(Type Type);
		bool IsMap(Type Type);
	}
}
