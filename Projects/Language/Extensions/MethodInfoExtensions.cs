// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Reflection;
using System.Text;

namespace VisualScriptTool.Language.Extensions
{
    public static class MethodInfoExtensions
    {
        private const char SEPARATOR = ':';

        public static string GetFullName(this MethodInfo a)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(a.DeclaringType.FullName);
            builder.Append(SEPARATOR);
            builder.Append(a.GetPrettyName());
            return builder.ToString();
        }

        public static string GetPrettyName(this MethodInfo a)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(a.Name);
            builder.Append('(');
            ParameterInfo[] parameters = a.GetParameters();
            for (int i = 0; i < parameters.Length; ++i)
            {
                if (i != 0)
                    builder.Append(", ");
                builder.Append(parameters[i].ParameterType.FullName);
            }
            builder.Append(')');
            return builder.ToString();
        }

        public static MethodInfo Get(string FullName)
        {
            if (string.IsNullOrEmpty(FullName))
                return null;

            string[] parts = FullName.Split(new char[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

            Type type = Type.GetType(parts[0]);

            if (type == null)
                return null;

            string[] signature = parts[1].Split(new char[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
            Type[] parametersType = new Type[signature.Length - 1];
            for (int i = 0; i < parametersType.Length; ++i)
                parametersType[i] = Type.GetType(signature[i + 1]);

            return type.GetMethod(signature[0], parametersType);
        }
    }
}