using System;
using System.IO;
using Xunit;
using Xunit.Extensions;
using Zoltu.IO;

namespace Zoltu.Tests
{
	public class BigEndianBinaryWriterTests
	{
		[Fact]
		public void make_ncrunch_happy()
		{ }

		// validates my assumptions about the default implementation doing the opposite of this implementation
		[Theory]
		[InlineData((Int32)0, new Byte[] { 0x00, 0x00, 0x00, 0x00 })]
		[InlineData((Int32)1, new Byte[] { 0x01, 0x00, 0x00, 0x00 })]
		[InlineData((Int32)(-1), new Byte[] { 0xFF, 0xFF, 0xFF, 0xFF })]
		[InlineData(Int32.MinValue, new Byte[] { 0x00, 0x00, 0x00, 0x80 })]
		[InlineData(Int32.MaxValue, new Byte[] { 0xFF, 0xFF, 0xFF, 0x7F })]
		public void NativeBinaryWriterTests(Int32 number, Byte[] expectedBytes)
		{
			// arrange
			var memoryStream = new MemoryStream();
			var binaryWriter = new BinaryWriter(memoryStream);

			// act
			binaryWriter.Write(number);

			// assert
			var actualBytes = memoryStream.ToArray();
			Assert.Equal(expectedBytes, actualBytes);
		}

		[Theory]
		[InlineData((Int32)0, new Byte[] { 0x00, 0x00, 0x00, 0x00 })]
		[InlineData((Int32)1, new Byte[] { 0x00, 0x00, 0x00, 0x01 })]
		[InlineData((Int32)(-1), new Byte[] { 0xFF, 0xFF, 0xFF, 0xFF })]
		[InlineData(Int32.MinValue, new Byte[] { 0x80, 0x00, 0x00, 0x00 })]
		[InlineData(Int32.MaxValue, new Byte[] { 0x7F, 0xFF, 0xFF, 0xFF })]
		public void Int32Tests(Int32 number, Byte[] expectedBytes)
		{
			// arrange
			var memoryStream = new MemoryStream();
			var binaryWriter = new BigEndianBinaryWriter(memoryStream);

			// act
			binaryWriter.Write(number);

			// assert
			var actualBytes = memoryStream.ToArray();
			Assert.Equal(expectedBytes, actualBytes);
		}

		[Theory]
		[InlineData((UInt32)0, new Byte[] { 0x00, 0x00, 0x00, 0x00 })]
		[InlineData((UInt32)1, new Byte[] { 0x00, 0x00, 0x00, 0x01 })]
		[InlineData((UInt32)123456789, new Byte[] { 0x07, 0x5B, 0xCD, 0x15 })]
		[InlineData(UInt32.MinValue, new Byte[] { 0x00, 0x00, 0x00, 0x00 })]
		[InlineData(UInt32.MaxValue, new Byte[] { 0xFF, 0xFF, 0xFF, 0xFF })]
		public void UInt32Tests(UInt32 number, Byte[] expectedBytes)
		{
			// arrange
			var memoryStream = new MemoryStream();
			var binaryWriter = new BigEndianBinaryWriter(memoryStream);

			// act
			binaryWriter.Write(number);

			// assert
			var actualBytes = memoryStream.ToArray();
			Assert.Equal(expectedBytes, actualBytes);
		}

		[Theory]
		[InlineData((Single)(0), new Byte[] { 0x00, 0x00, 0x00, 0x00 })]
		[InlineData((Single)(1), new Byte[] { 0x3F, 0x80, 0x00, 0x00 })]
		[InlineData((Single)(-1), new Byte[] { 0xBF, 0x80, 0x00, 0x00 })]
		[InlineData(Single.MinValue, new Byte[] { 0xFF, 0x7F, 0xFF, 0xFF })]
		[InlineData(Single.MaxValue, new Byte[] { 0x7F, 0x7F, 0xFF, 0xFF })]
		[InlineData(Single.PositiveInfinity, new Byte[] { 0x7F, 0x80, 0x00, 0x00 })]
		[InlineData(Single.NegativeInfinity, new Byte[] { 0xFF, 0x80, 0x00, 0x00 })]
		[InlineData(Single.NaN, new Byte[] { 0xFF, 0xC0, 0x00, 0x00 })]
		public void SingleTests(Single number, Byte[] expectedBytes)
		{
			// arrange
			var memoryStream = new MemoryStream();
			var binaryWriter = new BigEndianBinaryWriter(memoryStream);

			// act
			binaryWriter.Write(number);

			// assert
			var actualBytes = memoryStream.ToArray();
			Assert.Equal(expectedBytes, actualBytes);
		}

		[Theory]
		[InlineData((Double)(0), new Byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
		[InlineData((Double)(1), new Byte[] { 0x3F, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
		[InlineData((Double)(-1), new Byte[] { 0xBF, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
		[InlineData(Double.MinValue, new Byte[] { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF })]
		[InlineData(Double.MaxValue, new Byte[] { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF })]
		[InlineData(Double.PositiveInfinity, new Byte[] { 0x7F, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
		[InlineData(Double.NegativeInfinity, new Byte[] { 0xFF, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
		[InlineData(Double.NaN, new Byte[] { 0xFF, 0xF8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
		public void DoubleTests(Double number, Byte[] expectedBytes)
		{
			// arrange
			var memoryStream = new MemoryStream();
			var binaryWriter = new BigEndianBinaryWriter(memoryStream);

			// act
			binaryWriter.Write(number);

			// assert
			var actualBytes = memoryStream.ToArray();
			Assert.Equal(expectedBytes, actualBytes);
		}

		[Theory]
		[InlineData("0000", new Byte[] { 0x04, 0x30, 0x30, 0x30, 0x30 })]
		[InlineData("€€€€", new Byte[] { 0x0C, 0xE2, 0x82, 0xAC, 0xE2, 0x82, 0xAC, 0xE2, 0x82, 0xAC, 0xE2, 0x82, 0xAC })]
		public void StringTests(String value, Byte[] expectedBytes)
		{
			// arrange
			var memoryStream = new MemoryStream();
			var binaryWriter = new BigEndianBinaryWriter(memoryStream);

			// act
			binaryWriter.Write(value);

			// assert
			var actualBytes = memoryStream.ToArray();
			Assert.Equal(expectedBytes, actualBytes);
		}

		[Theory]
		[InlineData('0', new Byte[] { 0x30 })]
		[InlineData('€', new Byte[] { 0xE2, 0x82, 0xAC })]
		public void CharTests(Char value, Byte[] expectedBytes)
		{
			// arrange
			var memoryStream = new MemoryStream();
			var binaryWriter = new BigEndianBinaryWriter(memoryStream);

			// act
			binaryWriter.Write(value);

			// assert
			var actualBytes = memoryStream.ToArray();
			Assert.Equal(expectedBytes, actualBytes);
		}

		[Theory]
		[InlineData(new Char[] { '0', '0', '0', '0' }, new Byte[] { 0x30, 0x30, 0x30, 0x30 })]
		[InlineData(new Char[] { '€', '€', '€', '€' }, new Byte[] { 0xE2, 0x82, 0xAC, 0xE2, 0x82, 0xAC, 0xE2, 0x82, 0xAC, 0xE2, 0x82, 0xAC })]
		public void CharArrayTests(Char[] value, Byte[] expectedBytes)
		{
			// arrange
			var memoryStream = new MemoryStream();
			var binaryWriter = new BigEndianBinaryWriter(memoryStream);

			// act
			binaryWriter.Write(value);

			// assert
			var actualBytes = memoryStream.ToArray();
			Assert.Equal(expectedBytes, actualBytes);
		}

		[Theory]
		[InlineData(new Char[] { '0', '1', '2', '3' }, 1, 2, new Byte[] { 0x31, 0x32 })]
		[InlineData(new Char[] { '€', '2', '€', '€' }, 1, 2, new Byte[] { 0x32, 0xE2, 0x82, 0xAC })]
		public void CharSubArrayTests(Char[] value, Int32 index, Int32 count, Byte[] expectedBytes)
		{
			// arrange
			var memoryStream = new MemoryStream();
			var binaryWriter = new BigEndianBinaryWriter(memoryStream);

			// act
			binaryWriter.Write(value, index, count);

			// assert
			var actualBytes = memoryStream.ToArray();
			Assert.Equal(expectedBytes, actualBytes);
		}

	}
}
