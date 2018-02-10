// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration.Variables;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Control
{
	public class ForStatement : ControlStatement
	{
		public override string Name
		{
			get { return "for"; }
			set { }
		}

		[SerializableElement(2)]
		public IntegerVariable MinimumValue
		{
			get;
			set;
		}

		[SerializableElement(6)]
		public int MinimumDefaultValue
		{
			get;
			set;
		}

		[SerializableElement(3)]
		public IntegerVariable MaximumValue
		{
			get;
			set;
		}

		[SerializableElement(7)]
		public int MaximumDefaultValue
		{
			get;
			set;
		}

		[SerializableElement(4)]
		public IntegerVariable StepValue
		{
			get;
			set;
		}

		[SerializableElement(8)]
		public int StepDefaultValue
		{
			get;
			set;
		}

		[SerializableElement(5)]
		public Statement Statement
		{
			get;
			set;
		}
	}
}