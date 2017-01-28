// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System.Text;

namespace VisualScriptTool.Serialization.JSONSerializer
{
	class JSONParser
	{
		private const char NULL_CHAR = '\0';

		private int index = 0;
		private string contents = null;

		public T Deserialize<T>(ref string Contents) where T : ISerializeData
		{
			index = 0;
			contents = Contents;

			if (IsWhitespace())
				return default(T);

			if (GetChar() == '{')
				return (T)ParseObject(null);
			else if (GetChar() == '[')
				return (T)ParseArray(null);

			return default(T);
		}

		private ISerializeObject ParseObject(ISerializeData Parent)
		{
			ISerializeObject obj = new JSONSerializeObject(Parent);



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
				{
					ISerializeObject obj = ParseObject(array);
					array.Add(obj);
				}
				else if (c == '[')
				{
					ISerializeArray obj = ParseArray(array);
					array.Add(obj);
				}
				else
				{
					bool isString = (c == '"');

					if (isString)
					{
						MoveNext();

						array.Add(ReadLiteral());

						MoveNext();
					}
					else
						array.Add(CastItem(ReadItem()));
				}
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

				if (c == ',' || c == ':' || c == '"' || c == '"' || c == '{' || c == '}' || c == '[' || c == ']')
					break;

				str.Append(c);
			}

			return str.ToString();
		}

		private string ReadLiteral()
		{
			StringBuilder str = new StringBuilder();

			char c = GetChar();
			str.Append(c);

			while (!IsWhitespace())
			{
				MoveNext();

				c = GetChar();

				if (c == ',' || c == ':' || c == '"' || c == '"' || c == '{' || c == '}' || c == '[' || c == ']')
					break;

				str.Append(c);
			}

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
