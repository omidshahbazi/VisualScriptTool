// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	public sealed class MemberData
	{
		private object instance = null;
		private MemberInfo memberInfo = null;
		private object cachedValue = null;
		private bool isProperty = false;
		private MethodInfo setAccessor = null;

		public int Identifier
		{
			get;
			private set;
		}

		public System.Type Type
		{
			get;
			private set;
		}

		public bool IsPrimitive
		{
			get;
			private set;
		}

		public object Value
		{
			get { return cachedValue; }
			set
			{
				cachedValue = value;

				if (isProperty)
				{
					setAccessor.Invoke(instance, new object[] { Value });
					return;
				}

				((FieldInfo)memberInfo).SetValue(instance, Value);
			}
		}

		public MemberData(object Instance, MemberInfo MemberInfo, int Identifier)
		{
			instance = Instance;
			memberInfo = MemberInfo;
			this.Identifier = Identifier;

			if (memberInfo is FieldInfo)
			{
				FieldInfo field = (FieldInfo)memberInfo;

				cachedValue = field.GetValue(instance);
				Type = field.FieldType;
			}
			else
			{
				PropertyInfo property = (PropertyInfo)memberInfo;

				cachedValue = property.GetValue(instance, null);
				Type = property.PropertyType;
				isProperty = true;
				setAccessor = property.GetSetMethod(true);
			}

			IsPrimitive = IsTypePrimitive(Type);
        }

		public static bool IsTypePrimitive(System.Type Type)
		{
			return (Type.IsPrimitive || Type == typeof(string));
		}
	}
}