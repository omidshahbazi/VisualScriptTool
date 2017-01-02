// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Text;

namespace VisualScriptTool.Serialization
{
	class CodeBuilder
	{
		private StringBuilder content = null;

		public CodeBuilder()
		{
			content = new StringBuilder();
		}

		public CodeBuilder Clear()
		{
			content.Clear();
			return this;
		}

		public CodeBuilder Append(object Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(char[] Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(ulong Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(uint Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(ushort Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(decimal Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(double Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(float Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(int Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(short Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(char Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(long Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(sbyte Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(bool Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(string Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(byte Value)
		{
			content.Append(Value);
			return this;
		}

		public CodeBuilder Append(char Value, int RepeatCount)
		{
			content.Append(Value, RepeatCount);
			return this;
		}

		public CodeBuilder Append(string Value, int StartIndex, int Count)
		{
			content.Append(Value, StartIndex, Count);
			return this;
		}

		public CodeBuilder Append(char[] Value, int StartIndex, int CharCount)
		{
			content.Append(Value, StartIndex, CharCount);
			return this;
		}

		public CodeBuilder AppendFormat(string Format, params object[] Args)
		{
			content.AppendFormat(Format, Args);
			return this;
		}

		public CodeBuilder AppendLine(uint Indent = 0)
		{
			content.AppendLine();

			for (uint i = 0; i < Indent; ++i)
				content.Append('\t');

			return this;
		}
		public CodeBuilder AppendLine(string Value, uint Indent = 0)
		{
			AppendLine(Indent);

			content.Append(Value);

			return this;
		}

		public CodeBuilder Replace(string OldValue, string NewValue)
		{
			content.Replace(OldValue, NewValue);

			return this;
		}

		public CodeBuilder Replace(char OldValue, char NewValue)
		{
			content.Replace(OldValue, NewValue);

			return this;
		}

		public override string ToString()
		{
			return content.ToString();
		}

		public string ToString(int StartIndex, int Length)
		{
			return content.ToString(StartIndex, Length);
		}
	}
}
