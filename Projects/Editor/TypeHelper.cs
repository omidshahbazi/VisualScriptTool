// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Reflection;

namespace VisualScriptTool.Editor
{
	public static class TypeHelper
	{
		public static Type[] GetDrievedTypesOf<T>() where T : class
		{
			Type baseClassType = typeof(T);

			List<Type> retTypes = new List<Type>();

			Type[] types = Assembly.GetCallingAssembly().GetTypes();

			for (int i = 0; i < types.Length; ++i)
			{
				Type type = types[i];

				if (type.IsSubclassOf(baseClassType))
					retTypes.Add(type);
			}

			if (retTypes.Count == 0)
				return null;

			return retTypes.ToArray();
		}
	}
}