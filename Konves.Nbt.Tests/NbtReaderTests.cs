using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Konves.Nbt.Tests
{
	[TestClass]
	public class NbtReaderTests
	{
		[TestMethod]
		public void ReadTagInfo_Normal()
		{
			// Arrange
			byte[] data = new byte[] { 0x04, 0x00, 0x0D };
			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = (NbtTagType)0x04;
			int expectedNameLength = 0x0D;

			// Act
			NbtTagInfo tagInfo = reader.ReadTagInfo();

			// Assert
			Assert.AreEqual(expectedTagType, tagInfo.Type);
			Assert.AreEqual(expectedNameLength, tagInfo.NameLength);
		}

		[TestMethod]
		public void ReadTagInfo_EndTag()
		{
			// Arrange
			byte[] data = new byte[] { 0x00};
			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.End;
			int expectedNameLength = 0x00;

			// Act
			NbtTagInfo tagInfo = reader.ReadTagInfo();

			// Assert
			Assert.AreEqual(expectedTagType, tagInfo.Type);
			Assert.AreEqual(expectedNameLength, tagInfo.NameLength);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void ReadTagInfo_ArgumentOutOfRangeException()
		{
			// Arrange
			byte[] data = new byte[] { 0xFF, 0x00, 0x0D };
			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = (NbtTagType)0x04;
			int expectedNameLength = 0x0D;

			// Act
			NbtTagInfo tagInfo = reader.ReadTagInfo();
		}

		[TestMethod]
		public void ReadByte_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x01, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x7B };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.Byte;
			string expectedName = "asdf";
			byte expectedValue = 123;

			// Act
			NbtByte result = reader.ReadByte(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);
			Assert.AreEqual(expectedValue, result.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadByte_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x4F };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtByte result = reader.ReadByte(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadByte_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x01, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x4F };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtByte result = reader.ReadByte(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadByte_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x01, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtByte result = reader.ReadByte(tagInfo);
		}

		[TestMethod]
		public void ReadShort_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x02, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x30, 0x39 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.Short;
			string expectedName = "asdf";
			short expectedValue = 12345;

			// Act
			NbtShort result = reader.ReadShort(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);
			Assert.AreEqual(expectedValue, result.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadShort_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x30, 0x39 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtShort result = reader.ReadShort(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadShort_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x02, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x30, 0x39 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtShort result = reader.ReadShort(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadShort_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x02, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtShort result = reader.ReadShort(tagInfo);
		}

		[TestMethod]
		public void ReadInt_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x03, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x12, 0x34, 0x56, 0x78 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.Int;
			string expectedName = "asdf";
			int expectedValue = 305419896;

			// Act
			NbtInt result = reader.ReadInt(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);
			Assert.AreEqual(expectedValue, result.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadInt_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x12, 0x34, 0x56, 0x78 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtInt result = reader.ReadInt(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadInt_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x03, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x12, 0x34, 0x56, 0x78 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtInt result = reader.ReadInt(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadInt_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x03, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtInt result = reader.ReadInt(tagInfo);
		}

		[TestMethod]
		public void ReadLong_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x04, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.Long;
			string expectedName = "asdf";
			long expectedValue = 81985529216486895;

			// Act
			NbtLong result = reader.ReadLong(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);
			Assert.AreEqual(expectedValue, result.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadLong_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtLong result = reader.ReadLong(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadLong_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x04, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtLong result = reader.ReadLong(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadLong_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x04, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtLong result = reader.ReadLong(tagInfo);
		}

		[TestMethod]
		public void ReadFloat_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x05, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0xC0, 0x49, 0x0F, 0xDB };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.Float;
			string expectedName = "asdf";
			float expectedValue = -3.1415927F;

			// Act
			NbtFloat result = reader.ReadFloat(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);
			Assert.AreEqual(expectedValue, result.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadFloat_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0xC0, 0x49, 0x0F, 0xDB };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtFloat result = reader.ReadFloat(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadFloat_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x05, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0xC0, 0x49, 0x0F, 0xDB };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtFloat result = reader.ReadFloat(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadFloat_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x05, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtFloat result = reader.ReadFloat(tagInfo);
		}

		[TestMethod]
		public void ReadDouble_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x06, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.Double;
			string expectedName = "asdf";
			double expectedValue = 3.14159265358979311599796346854E0;

			// Act
			NbtDouble result = reader.ReadDouble(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);
			Assert.AreEqual(expectedValue, result.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadDouble_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtDouble result = reader.ReadDouble(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadDouble_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x06, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtDouble result = reader.ReadDouble(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadDouble_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x06, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtDouble result = reader.ReadDouble(tagInfo);
		}

		[TestMethod]
		public void ReadByteArray_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x07, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.ByteArray;
			string expectedName = "asdf";
			byte[] expectedValue = new byte[] { 0x6A, 0x6B, 0x6C, 0x3B };

			// Act
			NbtByteArray result = reader.ReadByteArray(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);
			CollectionAssert.AreEquivalent(expectedValue, result.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadByteArray_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtByteArray result = reader.ReadByteArray(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadByteArray_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x07, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtByteArray result = reader.ReadByteArray(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadByteArray_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x07, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtByteArray result = reader.ReadByteArray(tagInfo);
		}
		
		[TestMethod]
		public void ReadString_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x08, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.String;
			string expectedName = "asdf";
			string expectedValue = "jkl;";

			// Act
			NbtString result = reader.ReadString(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);
			Assert.AreEqual(expectedValue, result.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadString_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtString result = reader.ReadString(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadString_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x08, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73, 0x64, 0x66, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtString result = reader.ReadString(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadString_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x08, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtString result = reader.ReadString(tagInfo);
		}
		
		[TestMethod]
		public void ReadList_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x09, 0x00, 0x04 };
			byte[] data = new byte[] {
				0x61, 0x73, 0x64, 0x66, // name: "asdf"
				0x08, // type: TAG_String
				0x00, 0x00, 0x00, 0x04, // size: 4
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x31, // "jkl;1"
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x32, // "jkl;2"
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x33, // "jkl;3"
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x34  // "jkl;4"
			};

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.List;
			string expectedName = "asdf";
			NbtString[] expectedValue = new NbtString[] {
				new NbtString(string.Empty, "jkl;1"),
				new NbtString(string.Empty, "jkl;2"),
				new NbtString(string.Empty, "jkl;3"),
				new NbtString(string.Empty, "jkl;4")
			};

			// Act
			NbtList result = reader.ReadList(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);
			for (int i = 0; i < expectedValue.Length && i < result.Value.Length; i++)
			{
				Assert.AreEqual(expectedValue[i].Name, result.Value[i].Name);
				Assert.AreEqual(expectedValue[i].Type, result.Value[i].Type);
				Assert.AreEqual(expectedValue[i].Value, result.Value[i].GetValue());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadList_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] {
				0x61, 0x73, 0x64, 0x66, // name: "asdf"
				0x08, // type: TAG_String
				0x00, 0x00, 0x00, 0x04, // size: 4
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x31, // "jkl;1"
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x32, // "jkl;2"
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x33, // "jkl;3"
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x34  // "jkl;4"
			};

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtList result = reader.ReadList(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadList_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x09, 0x00, 0x04 };
			byte[] data = new byte[] {
				0x61, 0x73, 0x64, 0x66, // name: "asdf"
				0x08, // type: TAG_String
				0x00, 0x00, 0x00, 0x04, // size: 4
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x31, // "jkl;1"
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x32, // "jkl;2"
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x33, // "jkl;3"
				0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x34  // "jkl;4"
			};

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtList result = reader.ReadList(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadList_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x09, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtList result = reader.ReadList(tagInfo);
		}
		
		[TestMethod]
		public void ReadCompound_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x0A, 0x00, 0x04 };
			byte[] data = new byte[] {
				0x61, 0x73, 0x64, 0x66,
			    0x06, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18, // double
			    0x02, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x30, 0x39, // short
				0x00 // TAG_End
			};

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.Compound;
			string expectedName = "asdf";
			NbtTag[] expectedValue = new NbtTag[]
			{
				new NbtDouble("asdf", 3.14159265358979311599796346854E0),
				new NbtShort("asdf", 12345)
			};

			// Act
			NbtCompound result = reader.ReadCompound(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);

			Assert.AreEqual(expectedValue[0].Name, result.Value[0].Name);
			Assert.AreEqual(expectedValue[0].Type, result.Value[0].Type);
			Assert.AreEqual(expectedValue[0].GetValue(), result.Value[0].GetValue());

			Assert.AreEqual(expectedValue[1].Name, result.Value[1].Name);
			Assert.AreEqual(expectedValue[1].Type, result.Value[1].Type);
			Assert.AreEqual(expectedValue[1].GetValue(), result.Value[1].GetValue());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadCompound_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] {
				0x61, 0x73, 0x64, 0x66,
			    0x06, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18, // double
			    0x02, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x30, 0x39, // short
				0x00 // TAG_End
			};

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtCompound result = reader.ReadCompound(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadCompound_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x0A, 0x00, 0x04 };
			byte[] data = new byte[] {
				0x61, 0x73, 0x64, 0x66,
			    0x06, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18, // double
			    0x02, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x30, 0x39, // short
				0x00 // TAG_End
			};

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtCompound result = reader.ReadCompound(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadCompound_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x0A, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtCompound result = reader.ReadCompound(tagInfo);
		}

		[TestMethod]
		public void ReadIntArray_Normal()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x0B, 0x00, 0x04 };
			byte[] data = new byte[] 
			{ 
				0x61, 0x73, 0x64, 0x66, // "asdf"
				0x00, 0x00, 0x00, 0x04, // size: 4
				0x00, 0x00, 0x30, 0x39, 
				0x00, 0x00, 0x05, 0x39, 
				0x07, 0x5B, 0xCD, 0x15, 
				0x03, 0x4F, 0xB5, 0xE3
			};

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			NbtTagType expectedTagType = NbtTagType.IntArray;
			string expectedName = "asdf";
			int[] expectedValue = new int[] { 12345, 1337, 123456789, 55555555 };

			// Act
			NbtIntArray result = reader.ReadIntArray(tagInfo);

			// Assert
			Assert.AreEqual(expectedName, result.Name);
			Assert.AreEqual(expectedTagType, result.Type);
			CollectionAssert.AreEquivalent(expectedValue, result.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ReadIntArray_ArgumentException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x00 };
			byte[] data = new byte[] 
			{ 
				0x61, 0x73, 0x64, 0x66, // "asdf"
				0x00, 0x00, 0x00, 0x04, // size: 4
				0x00, 0x00, 0x30, 0x39, 
				0x00, 0x00, 0x05, 0x39, 
				0x07, 0x5B, 0xCD, 0x15, 
				0x03, 0x4F, 0xB5, 0xE3
			};

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtIntArray result = reader.ReadIntArray(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ReadIntArray_ObjectDisposedException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x0B, 0x00, 0x04 };
			byte[] data = new byte[] 
			{ 
				0x61, 0x73, 0x64, 0x66, // "asdf"
				0x00, 0x00, 0x00, 0x04, // size: 4
				0x00, 0x00, 0x30, 0x39, 
				0x00, 0x00, 0x05, 0x39, 
				0x07, 0x5B, 0xCD, 0x15, 
				0x03, 0x4F, 0xB5, 0xE3
			};

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			stream.Close();

			// Act
			NbtIntArray result = reader.ReadIntArray(tagInfo);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.EndOfStreamException))]
		public void ReadIntArray_EndOfStreamException()
		{
			// Arrange
			NbtTagInfo tagInfo = (NbtTagInfo)new byte[] { 0x0B, 0x00, 0x04 };
			byte[] data = new byte[] { 0x61, 0x73 };

			MemoryStream stream = new MemoryStream(data);

			NbtReader reader = new NbtReader(stream);

			// Act
			NbtIntArray result = reader.ReadIntArray(tagInfo);
		}
	}
}
