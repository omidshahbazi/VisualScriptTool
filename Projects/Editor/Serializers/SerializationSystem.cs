// This file has generated by Compiler, do not change it manually
namespace VisualScriptTool.Editor.Serializers
{
	static class SerializationSystem
	{
		public static void Initialize()
		{
			Serialization.Creator.AddSerializer(new Statement_Serializer());
			Serialization.Creator.AddSerializer(new ExecuterStatement_Serializer());
			Serialization.Creator.AddSerializer(new IfStatement_Serializer());
			Serialization.Creator.AddSerializer(new WhileStatement_Serializer());
			Serialization.Creator.AddSerializer(new ForStatement_Serializer());
			Serialization.Creator.AddSerializer(new BooleanVariable_Serializer());
			Serialization.Creator.AddSerializer(new IntegerVariable_Serializer());
			Serialization.Creator.AddSerializer(new FloatVariable_Serializer());
			Serialization.Creator.AddSerializer(new StringVariable_Serializer());
			Serialization.Creator.AddSerializer(new VariableSetterStatement_Serializer());
			Serialization.Creator.AddSerializer(new ExecuterStatementInstance_Serializer());
			Serialization.Creator.AddSerializer(new StatementInstance_Serializer());
			Serialization.Creator.AddSerializer(new IfStatementInstance_Serializer());
			Serialization.Creator.AddSerializer(new WhileStatementInstance_Serializer());
			Serialization.Creator.AddSerializer(new ForStatementInstance_Serializer());
			Serialization.Creator.AddSerializer(new VariableGetterStatementInstance_Serializer());
			Serialization.Creator.AddSerializer(new PointF_Serializer());
			Serialization.Creator.AddSerializer(new SizeF_Serializer());
		}
	}
}
