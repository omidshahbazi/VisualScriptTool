// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Text;

namespace VisualScriptTool.Serialization.JSONSerializer
{
	class JSONParser
	{
		private const char NULL_CHAR = '\0';

		private int index = 0;
		private char[] contents = null;

		public ISerializeData Parse(ref string Contents)
		{
			index = 0;
			contents = Contents.ToCharArray();

			char c = GetChar();
			//MoveToNextChar();

			if (c == '{')
				return ParseObject(null);
			else if (c == '[')
				return ParseArray(null);

			return null;
		}

		private ISerializeObject ParseObject(ISerializeData Parent)
		{
			ISerializeObject obj = new JSONSerializeObject(Parent);

			while (true)
			{
				char c = GetChar();
				if (c == '}')
					break;

				MoveToNextChar();

				c = GetChar();
				if (c != '"')
					break;

				string key = ReadLiteral();

				MoveToNextChar();

				c = GetChar();
				if (c != ':')
					break;

				MoveToNextChar();

				c = GetChar();

				if (c == '{')
					obj.Set(key, ParseObject(obj));
				else if (c == '[')
					obj.Set(key, ParseArray(obj));
				else
				{
					bool isString = (c == '"');

					if (isString)
						obj.Set(key, ReadLiteral());
					else
						obj.Set(key, CastItem(ReadItem()));
				}

				MoveToNextChar();

				c = GetChar();
				if (c != ',')
					break;
			}

			return obj;
		}

		private ISerializeArray ParseArray(ISerializeData Parent)
		{
			ISerializeArray array = new JSONSerializeArray(Parent);

			while (true)
			{
				char c = GetChar();

				if (c == ']')
					break;

				MoveToNextChar();

				c = GetChar();

				if (c == '{')
					array.Add(ParseObject(array));
				else if (c == '[')
					array.Add(ParseArray(array));
				else
				{
					bool isString = (c == '"');

					if (isString)
						array.Add(ReadLiteral());
					else
						array.Add(CastItem(ReadItem()));
				}

				MoveToNextChar();

				c = GetChar();
				if (c != ',')
					break;
			}

			return array;
		}

		private string ReadItem()
		{
			StringBuilder str = new StringBuilder();

			char c = GetChar();
			str.Append(c);

			while (!IsWhitespace())
			{
				MoveNext();

				c = GetChar();

				if (IsWhitespace() || c == ',' || c == ':' || c == '"' || c == '{' || c == '}' || c == '[' || c == ']')
				{
					MoveBack();

					break;
				}

				str.Append(c);
			}

			return str.ToString();
		}

		private string ReadLiteral()
		{
			StringBuilder str = new StringBuilder();

			char c = GetChar();

			if (c != '"')
				return string.Empty;

			MoveNext();

			c = GetChar();

			if (c == '"')
				return string.Empty;

			str.Append(c);

			char prevChar = NULL_CHAR;

			while (true)
			{
				MoveNext();

				c = GetChar();

				if (prevChar != '\\' && c == '"')
					break;

				str.Append(c);

				prevChar = c;
			}

			//MoveNext();

			return str.ToString();
		}

		private void MoveToNextChar()
		{
			MoveNext();
			while (IsWhitespace() && GetChar() != NULL_CHAR)
				MoveNext();
		}

		private void MoveNext()
		{
			++index;
		}

		private void MoveBack()
		{
			--index;
		}

		private bool IsWhitespace()
		{
			char c = GetChar();

			return (c == ' ' || c == '\t' || c == '\n' || c == '\r' || c == NULL_CHAR);
		}

		private char GetChar()
		{
			if (index < contents.Length)
				return contents[index];

			return NULL_CHAR;
		}

		private object CastItem(string Item)
		{
			if (Item == "null")
				return null;
			else if (Item == "true")
				return true;
			else if (Item == "false")
				return false;
			else if (Item.Contains("."))
				return double.Parse(Item);

			return long.Parse(Item);
		}
	}
}
