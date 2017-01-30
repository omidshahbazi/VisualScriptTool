// Copyright 2016-2017 ?????????????. All Rights Reserved.
using System;
using System.Windows.Forms;
using VisualScriptTool.Editor.Serializers;

namespace VisualScriptTool.Editor
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Compiler.Compile();

			SerializationSystem.Initialize();

			//Application.Run(new MainForm());


			byte[] encoded = RunLenghtEncoder.Encode2(System.IO.File.ReadAllBytes(Application.StartupPath + "/Reflection.pdb"));

			 encoded = RunLenghtEncoder.Encode2(encoded);

			System.IO.File.WriteAllBytes(Application.StartupPath + "/1.bin", encoded);
		}


		private class RunLenghtEncoder
		{
			public static byte[] Encode2(byte[] Data)
			{
				byte[] encodedHeader = new byte[Data.Length];
				byte[] encodedData = new byte[Data.Length];

				uint dataIndex = 0;
				short encodedHeaderIndex = 0;
				short encodedDataIndex = 0;
				byte sameBytesCount = 1;

				byte previousByte = Data[dataIndex++];

				while (true)
				{
					byte b = Data[dataIndex];

					if (b == previousByte)
						sameBytesCount++;
					else
					{
						if (sameBytesCount != 1)
						{
							encodedHeader[encodedHeaderIndex++] = (byte)(encodedDataIndex >> 8);
							encodedHeader[encodedHeaderIndex++] = (byte)encodedDataIndex;

							encodedHeader[encodedHeaderIndex++] = sameBytesCount;
						}

						encodedData[encodedDataIndex++] = b;

						sameBytesCount = 1;
					}

					previousByte = b;
					if (++dataIndex >= Data.Length)
						break;
				}

				byte[] finalData = new byte[2 + encodedHeaderIndex + 1 + encodedDataIndex + 1];

				short startBlockIndex = (short)(2 + encodedHeaderIndex);
				finalData[0] = (byte)(startBlockIndex >> 8);
				finalData[1] = (byte)startBlockIndex;

				System.Array.Copy(encodedHeader, 0, finalData, 2, encodedHeaderIndex + 1);
				System.Array.Copy(encodedData, 0, finalData, 2 + encodedHeaderIndex, encodedDataIndex + 1);

				return finalData;
			}
		}

		public static byte[] Encode1(byte[] Data)
		{
			byte[] encodedData = new byte[Data.Length * 2];

			uint dataIndex = 0;
			uint encodedDataIndex = 0;
			byte sameBytesCount = 1;

			byte previousByte = Data[dataIndex++];

			while (true)
			{
				byte b = Data[dataIndex];

				if (b == previousByte)
					sameBytesCount++;
				else
				{
					if (sameBytesCount != 1)
						encodedData[encodedDataIndex++] = sameBytesCount;

					encodedData[encodedDataIndex++] = b;

					sameBytesCount = 1;
				}

				previousByte = b;
				if (++dataIndex >= Data.Length)
					break;
			}

			byte[] finalData = new byte[encodedDataIndex + 1];
			System.Array.Copy(encodedData, finalData, finalData.Length);

			return finalData;
		}
	}
}