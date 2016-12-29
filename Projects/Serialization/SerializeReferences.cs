// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Collections.Generic;

namespace VisualScriptTool.Serialization
{
	class SerializeReferences
	{
		public class InstanceData
		{
			private static uint globalID;

			public string ID
			{
				get;
				private set;
			}

			public object Instance
			{
				get;
				private set;
			}

			public InstanceData(object Instance)
			{
				ID = "O#" + (globalID++).ToString();
				this.Instance = Instance;
			}
		}

		public class InstanceDataList : List<InstanceData>
		{ }

		private InstanceDataList instances = new InstanceDataList();

		public InstanceData[] Instances
		{
			get { return instances.ToArray(); }
		}

		public InstanceData GetInstance(object Instance)
		{
			for (int i = 0; i < instances.Count; ++i)
				if (instances[i].Instance == Instance)
					return instances[i];

			return null;
		}

		public InstanceData AddInstance(object Instance)
		{
			InstanceData instance = new InstanceData(Instance);
			instances.Add(instance);
			return instance;
		}

		public InstanceData GetOrAddInstance(object Instance)
		{
			InstanceData instance = GetInstance(Instance);

			if (instance != null)
				return instance;

			return AddInstance(Instance);
		}
	}
}