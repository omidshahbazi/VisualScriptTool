// Generaterd file
using VisualScriptTool.Serialization;
namespace System.Drawing
{
	class Point_Serializer : Serializer
	{
		public override System.Type Type
		{
			get { return typeof(System.Drawing.Point); }
		}

		public override object CreateInstance()
		{
			return null;
		}

		public override void Serialize(ISerializeObject Object, object Instance)
		{
			if (Type != Instance.GetType())
				throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
			System.Drawing.Point Point = (System.Drawing.Point)Instance;
		}

		public override void Deserialize(ISerializeObject Object, object Instance)
		{
			if (Type != Instance.GetType())
				throw new System.InvalidCastException("Expected [" + Type.FullName + "]");
			System.Drawing.Point Point = (System.Drawing.Point)Instance;
		}

	}
}