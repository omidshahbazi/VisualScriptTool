// Copyright 2016-2017 ?????????????. All Rights Reserved.
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements.Declaration
{
	public abstract class VariableStatement : UserDefinedStatement
	{
		[SerializableElement(1)]
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
