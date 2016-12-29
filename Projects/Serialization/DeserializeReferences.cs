// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;

namespace VisualScriptTool.Serialization
{
	class DeserializeReferences
	{
		public class InstanceData
		{
			public string ID
			{
				get;
				private set;
			}

			public object Instance
			{
				get;
				set;
			}

			public ISerializeObject SerializeObject
			{
				get;
				set;
			}

			public bool HasReference
			{
				get { return Instance != null; }
			}

			public InstanceData(string ID, ISerializeObject SerializeObject)
			{
				this.ID = ID;
				this.SerializeObject = SerializeObject;
			}

			public static bool IsReferenceIDFormat(string ID)
			{
				return ID.StartsWith("O#");
			}
		}

		public class InstanceDataList : List<InstanceData>
		{ }

		private InstanceDataList instances = new InstanceDataList();

		public InstanceData[] Instances
		{
			get { return instances.ToArray(); }
		}

		public InstanceData GetInstance(string ID)
		{
			for (int i = 0; i < instances.Count; ++i)
				if (instances[i].ID == ID)
					return instances[i];

			return null;
		}

		public InstanceData AddInstance(string ID, ISerializeObject SerializeObject)
		{
			InstanceData instance = new InstanceData(ID, SerializeObject);
			instances.Add(instance);
			return instance;
		}

		//public InstanceData GetOrAddInstance(string ID, ISerializeObject SerializeObject)
		//{
		//	InstanceData instance = GetInstance(ID);

		//	if (instance != null)
		//		return instance;

		//	return AddInstance(ID, SerializeObject);
		//}
	}
}