﻿// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public interface ICompileStrategy
	{
		MemberInfo[] GetMembers(Type Type);

		bool IsSerializableMember(MemberInfo Member);
		int GetMemberID(MemberInfo Member, int DefaultID);
		string GetMemberDefaultValue(MemberInfo Member);

		bool IsPrimitive(Type Type);
		bool IsArray(Type Type);
		bool IsList(Type Type);
		bool IsMap(Type Type);
	}
}
