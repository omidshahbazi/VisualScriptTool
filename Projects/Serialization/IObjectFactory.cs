// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Serialization
{
	public interface IObjectFactory
	{
		object Instantiate(Type Type);
		Array InstantiateArray(Type Type, uint Length);

		bool CanInstantiate(Type Type);
		bool CanInstantiateArray(Type Type);
	}
}