// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public class SystemObjectFactory : IObjectFactory
	{
		bool IObjectFactory.CanInstantiate(Type Type)
		{
			ConstructorInfo[] constructors = Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			for (int i = 0; i < constructors.Length; ++i)
			{
				ParameterInfo[] parameters = constructors[i].GetParameters();

				if (parameters.Length == 0)
					return true;

				bool canProceed = true;
				for (int j = 0; j < parameters.Length; ++j)
					if (!parameters[i].ParameterType.IsPrimitive)
					{
						canProceed = false;
						break;
					}

				if (canProceed)
					return true;
			}

			return false;
		}

		object IObjectFactory.Instantiate(Type Type)
		{
			return Activator.CreateInstance(Type, true);
		}
	}
}