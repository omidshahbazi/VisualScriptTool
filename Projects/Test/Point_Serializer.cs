// Generaterd file
using VisualScriptTool.Serialization;
namespace System.Drawing
{
	class BitmapSuffixInSatelliteAssemblyAttribute_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute); }
		}

		public override object CreateInstance()
		{
			return new System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute();
		}

		public override void Serialize(ISerializeObject Object, object Instance)
		{
			if (Type != Instance.GetType())
				throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
			System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute BitmapSuffixInSatelliteAssemblyAttribute = (System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute)Instance;
		}

		public override void Deserialize(ISerializeObject Object, object Instance)
		{
			if (Type != Instance.GetType())
				throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
			System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute BitmapSuffixInSatelliteAssemblyAttribute = (System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute)Instance;
		}

	}
}