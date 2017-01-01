// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Reflection;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Editor
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
			//return Activator.CreateInstance(Type, true);

			ConstructorInfo[] constructors = Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			ConstructorInfo properConstructor = null;

			ParameterInfo[] parameters = null;

			for (int i = 0; i < constructors.Length; ++i)
			{
				ConstructorInfo ctor = constructors[i];

				parameters = ctor.GetParameters();

				if (parameters.Length == 0)
				{
					properConstructor = ctor;
					break;
				}

				bool canProceed = true;
				for (int j = 0; j < parameters.Length; ++j)
					if (!parameters[i].ParameterType.IsPrimitive)
					{
						canProceed = false;
						break;
					}

				if (canProceed)
				{
					properConstructor = ctor;
					break;
				}
			}

			if (properConstructor == null)
				return null;

			parameters = properConstructor.GetParameters();
			object[] arguments = null;

			if (parameters.Length != 0)
				arguments = new object[parameters.Length];

			for (int i = 0; i < arguments.Length; ++i)
				arguments[i] = Activator.CreateInstance(parameters[i].ParameterType);

			return properConstructor.Invoke(arguments);
		}
	}
}