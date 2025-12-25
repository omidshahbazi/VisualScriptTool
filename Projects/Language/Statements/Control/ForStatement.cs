// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Declaration;
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

		[Constraints(typeof(byte))]
		[Constraints(typeof(sbyte))]
		[Constraints(typeof(short))]
		[Constraints(typeof(ushort))]
		[Constraints(typeof(int))]
		[Constraints(typeof(uint))]
		[Constraints(typeof(long))]
		[Constraints(typeof(ulong))]
		[Constraints(typeof(float))]
		[Constraints(typeof(double))]
		[Constraints(typeof(decimal))]
		[SerializableElement(2)]
		public VariableStatement First
		{
			get;
			set;
		}

		[SerializableElement(3)]
		public AnyDataType FirstDefaultValue
		{
			get;
			set;
		}

		[Constraints(typeof(byte))]
		[Constraints(typeof(sbyte))]
		[Constraints(typeof(short))]
		[Constraints(typeof(ushort))]
		[Constraints(typeof(int))]
		[Constraints(typeof(uint))]
		[Constraints(typeof(long))]
		[Constraints(typeof(ulong))]
		[Constraints(typeof(float))]
		[Constraints(typeof(double))]
		[Constraints(typeof(decimal))]
		[SerializableElement(4)]
		public VariableStatement Last
		{
			get;
			set;
		}

		[SerializableElement(5)]
		public AnyDataType LastDefaultValue
		{
			get;
			set;
		}

		[Constraints(typeof(byte))]
		[Constraints(typeof(sbyte))]
		[Constraints(typeof(short))]
		[Constraints(typeof(ushort))]
		[Constraints(typeof(int))]
		[Constraints(typeof(uint))]
		[Constraints(typeof(long))]
		[Constraints(typeof(ulong))]
		[Constraints(typeof(float))]
		[Constraints(typeof(double))]
		[Constraints(typeof(decimal))]
		[SerializableElement(6)]
		public VariableStatement Step
		{
			get;
			set;
		}

		[SerializableElement(7)]
		public AnyDataType StepDefaultValue
		{
			get;
			set;
		}

		[SerializableElement(8)]
		public Statement Statement
		{
			get;
			set;
		}

		public ForStatement()
		{
			FirstDefaultValue = 0;
			LastDefaultValue = 1;
			StepDefaultValue = 1;
		}
	}
}