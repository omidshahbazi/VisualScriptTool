// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;

namespace VisualScriptTool.Serialization
{
	public interface IObjectFactory
	{
		object Instantiate(Type Type);

		bool CanInstantiate(Type Type);
	}
}