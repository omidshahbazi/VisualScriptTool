// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Collections.Generic;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language.Statements
{
	public abstract class Statement
	{
		[SerializableElement(0)]
		public string ID
		{
			get;
			set;
		}

		public abstract string Name
		{
			get;
			set;
		}
		public Statement()
		{
			ID = Guid.NewGuid().ToString();
		}

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			if (obj is null)
				return false;

			if (obj is Statement)
				return ((Statement)obj).ID == ID;

			return false;
		}

		public static bool operator ==(Statement a, Statement b)
		{
			if (a is null && b is null)
				return true;

			if (a is null || b is null)
				return false;

			if (ReferenceEquals(a, b))
				return true;

			return (a.ID == b.ID);
		}

		public static bool operator !=(Statement a, Statement b)
		{
			if (a is null && b is null)
				return false;

			if (a is null || b is null)
				return true;

			if (ReferenceEquals(a, b))
				return false;

			return (a.ID != b.ID);
		}
	}

	public class StatementList : List<Statement>
	{ }
}
