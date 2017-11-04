// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Declaration.Variables
{
	public abstract class VariableStatement : Statement
	{
		[SerializableElement(0)]
		public override string Name
		{
			get;
			set;
		}

        public override string ToString()
        {
            return Name + " (" + GetType().Name.Replace("Variable", "") + ")";
        }
    }
}
