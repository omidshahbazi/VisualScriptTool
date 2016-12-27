// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	class MemberData
	{
		private object instance = null;
		private MemberInfo memberInfo = null;
		private object cachedValue = null;

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

		public object Value
		{
			get { return cachedValue; }
			set
			{
				cachedValue = value;

				if (memberInfo is FieldInfo)
					((FieldInfo)memberInfo).SetValue(instance, Value);

				((PropertyInfo)memberInfo).SetValue(instance, Value, null);
			}
		}

		public MemberData(object Instance, MemberInfo MemberInfo, SerializableAttribute Attribute)
		{
			instance = Instance;
			memberInfo = MemberInfo;
			Identifier = Attribute.Identifier;

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
			}
		}
	}
}