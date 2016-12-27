// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Reflection;

namespace VisualScriptTool.Serialization
{
	class MemberData
	{
		private object instance = null;
		private MemberInfo memberInfo = null;
		private SerializableAttribute attribute = null;

		public int Identifier
		{
			get { return attribute.Identifier; }
		}

		public object Value
		{
			get
			{
				if (memberInfo is FieldInfo)
					return ((FieldInfo)memberInfo).GetValue(instance);

				return ((PropertyInfo)memberInfo).GetValue(instance, null);
			}
			set
			{
				if (memberInfo is FieldInfo)
					((FieldInfo)memberInfo).SetValue(instance, Value);

				((PropertyInfo)memberInfo).SetValue(instance, Value, null);
			}
		}

		public MemberData(object Instance, MemberInfo MemberInfo, SerializableAttribute Attribute)
		{
			instance = Instance;
			memberInfo = MemberInfo;
			attribute = Attribute;
		}
	}
}