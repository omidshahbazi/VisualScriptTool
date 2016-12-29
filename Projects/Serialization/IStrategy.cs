// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Serialization
{
	public interface IStrategy
	{
		bool CanHandle(Type Type);

		bool IsPrimitiveType(Type Type);

		MemberData[] GetMembers(object Instance);
	}
}