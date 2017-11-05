// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Control
{
	public abstract class FlowStatement : Statement
	{
		[SerializableElement(1)]
		public Statement CompleteStatement
		{
			get;
			set;
		}
	}
}
