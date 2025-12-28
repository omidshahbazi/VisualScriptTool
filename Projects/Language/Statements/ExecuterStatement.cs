// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Language.Statements.Control;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements
{
	public class EntrypointStatement : FlowStatement
	{
		public override string Name
		{
			get { return "Entrypoint"; }
			set { }
		}

		[SerializableElement(2)]
		public Statement Statement
		{
			get;
			set;
		}
	}
}