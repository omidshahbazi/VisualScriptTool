// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public class DefaultObjectFactory : IObjectFactory
	{
		public Array InstantiateArray(Type Type, uint Length)
		{
			return Array.CreateInstance(Type.GetElementType(), Length);
		}

		object IObjectFactory.Instantiate(Type Type)
		{
			return Activator.CreateInstance(Type, true);
		}

		public bool CanInstantiateArray(Type Type)
		{
			return true;
		}

		bool IObjectFactory.CanInstantiate(Type Type)
		{
			ConstructorInfo[] constructors = Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			for (int i = 0; i < constructors.Length; ++i)
				if (constructors[i].GetParameters().Length == 0)
					return true;

			return false;
		}
	}
}