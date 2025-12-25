// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using VisualScriptTool.Serialization;

namespace VisualScriptTool.Language
{
	public struct AnyDataType
	{
		public static readonly Type[] AVAILABLE_TYPES = new Type[]
		{
			typeof(bool),
			typeof(byte),
			typeof(sbyte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(char),
			typeof(string)
		};

		[SerializableElement(0)]
		public object Value
		{
			get;
			set;
		}

		public Type Type
		{
			get { return (Value == null ? null : Value.GetType()); }
		}

		public AnyDataType(object Value)
		{
			if (Value == null)
			{
				this.Value = null;

				return;
			}

			this.Value = Value;

			CheckPrimitiveDataTypes(Value.GetType());
		}

		private void CheckPrimitiveDataTypes(params Type[] Types)
		{
			for (int i = 0; i < Types.Length; i++)
				if (Array.IndexOf(AVAILABLE_TYPES, Types[i]) == -1)
					throw new InvalidCastException();
		}

		private void CheckValidGet<T>()
		{
			if (Type == typeof(T))
				return;

			if (typeof(T).IsAssignableFrom(Type))
				return;

			throw new InvalidCastException();
		}

		public override bool Equals(object obj)
		{
			if (obj is null)
				return false;

			if (!(obj is AnyDataType))
				return false;

			return Value.Equals(((AnyDataType)obj).Value);
		}

		public override string ToString()
		{
			return (Value == null ? "null" : Value.ToString());
		}

		public static implicit operator AnyDataType(bool Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(byte Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(sbyte Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(short Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(ushort Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(int Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(uint Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(long Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(ulong Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(float Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(double Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(decimal Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(char Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator AnyDataType(string Value)
		{
			return new AnyDataType(Value);
		}

		public static implicit operator bool(AnyDataType Value)
		{
			Value.CheckValidGet<bool>();

			return (bool)Value.Value;
		}

		public static implicit operator byte(AnyDataType Value)
		{
			Value.CheckValidGet<byte>();

			return (byte)Value.Value;
		}

		public static implicit operator sbyte(AnyDataType Value)
		{
			Value.CheckValidGet<sbyte>();

			return (sbyte)Value.Value;
		}

		public static implicit operator short(AnyDataType Value)
		{
			Value.CheckValidGet<short>();

			return (short)Value.Value;
		}

		public static implicit operator ushort(AnyDataType Value)
		{
			Value.CheckValidGet<ushort>();

			return (ushort)Value.Value;
		}

		public static implicit operator int(AnyDataType Value)
		{
			Value.CheckValidGet<int>();

			return (int)Value.Value;
		}

		public static implicit operator uint(AnyDataType Value)
		{
			Value.CheckValidGet<uint>();

			return (uint)Value.Value;
		}

		public static implicit operator long(AnyDataType Value)
		{
			Value.CheckValidGet<long>();

			return (long)Value.Value;
		}

		public static implicit operator ulong(AnyDataType Value)
		{
			Value.CheckValidGet<ulong>();

			return (ulong)Value.Value;
		}

		public static implicit operator float(AnyDataType Value)
		{
			Value.CheckValidGet<float>();

			return (float)Value.Value;
		}

		public static implicit operator double(AnyDataType Value)
		{
			Value.CheckValidGet<double>();

			return (double)Value.Value;
		}

		public static implicit operator decimal(AnyDataType Value)
		{
			Value.CheckValidGet<decimal>();

			return (decimal)Value.Value;
		}

		public static implicit operator char(AnyDataType Value)
		{
			Value.CheckValidGet<char>();

			return (char)Value.Value;
		}

		public static implicit operator string(AnyDataType Value)
		{
			Value.CheckValidGet<string>();

			return (string)Value.Value;
		}

		public static bool operator ==(AnyDataType A, AnyDataType B)
		{
			return A.Value.Equals(B.Value);
		}

		public static bool operator !=(AnyDataType A, AnyDataType B)
		{
			return !A.Value.Equals(B.Value);
		}
	}
}
