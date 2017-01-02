// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public interface ICompileStrategy
	{
		MemberInfo[] GetMembers(Type Type);
	}
}
