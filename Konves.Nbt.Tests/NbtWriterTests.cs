using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Konves.Nbt.Tests
{
	[TestClass]
	public class NbtWriterTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_Stream_ArgumentNullException()
		{
			// Arrange
			Stream stream = null;

			// Act
			NbtWriter writer = new NbtWriter(stream);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_Stream_ArgumentException()
		{
			// Arrange
			Stream stream = new MemoryStream();
			stream.Close();

			// Act
			NbtWriter writer = new NbtWriter(stream);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_BinaryWriter_ArgumentNullException()
		{
			// Arrange
			BinaryWriter binaryWriter = null;

			// Act
			NbtWriter writer = new NbtWriter(binaryWriter);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_BinaryWriter_ArgumentException()
		{
			// Arrange
			Stream stream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.Close();

			// Act
			NbtWriter writer = new NbtWriter(binaryWriter);
		}

		[TestMethod]
		public void Write_ByteTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtByte tag = new NbtByte("asdf", 123);
			byte[] expected = new byte[] { 0x01, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x7B };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_ByteTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtByte tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_Byte()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			byte value = 123;
			byte[] expected = new byte[] { 0x01, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x7B };

			// Act
			writer.Write(name, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}
		
		[TestMethod]
		public void Write_ShortTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtShort tag = new NbtShort("asdf", 12345);
			byte[] expected = new byte[] { 0x02, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x30, 0x39 };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_ShortTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtShort tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_Short()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			short value = 12345;
			byte[] expected = new byte[] { 0x02, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x30, 0x39 };

			// Act
			writer.Write(name, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Write_IntTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtInt tag = new NbtInt("asdf", 305419896);
			byte[] expected = new byte[] { 0x03, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x12, 0x34, 0x56, 0x78 };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_IntTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtInt tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_Int()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			int value = 305419896;
			byte[] expected = new byte[] { 0x03, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x12, 0x34, 0x56, 0x78 };

			// Act
			writer.Write(name, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Write_LongTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtLong tag = new NbtLong("asdf", 81985529216486895);
			byte[] expected = new byte[] { 0x04, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_LongTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtLong tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_Long()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			long value = 81985529216486895;
			byte[] expected = new byte[] { 0x04, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };

			// Act
			writer.Write(name, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Write_FloatTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtFloat tag = new NbtFloat("asdf", -3.1415927F);
			byte[] expected = new byte[] { 0x05, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0xC0, 0x49, 0x0F, 0xDB };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_FloatTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtFloat tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_Float()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			float value = -3.1415927F;
			byte[] expected = new byte[] { 0x05, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0xC0, 0x49, 0x0F, 0xDB };

			// Act
			writer.Write(name, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Write_DoubleTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtDouble tag = new NbtDouble("asdf", 3.14159265358979311599796346854E0);
			byte[] expected = new byte[] { 0x06, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18 };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_DoubleTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtDouble tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_Double()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			double value = 3.14159265358979311599796346854E0;
			byte[] expected = new byte[] { 0x06, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18 };

			// Act
			writer.Write(name, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Write_ByteArrayTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtByteArray tag = new NbtByteArray("asdf", new byte[] { 0x6A, 0x6B, 0x6C, 0x3B });
			byte[] expected = new byte[] { 0x07, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_ByteArrayTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtByteArray tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_ByteArray()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			byte[] value = new byte[] { 0x6A, 0x6B, 0x6C, 0x3B };
			byte[] expected = new byte[] { 0x07, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B };

			// Act
			writer.Write(name, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Write_StringTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtString tag = new NbtString("asdf", "jkl;");
			byte[] expected = new byte[] { 0x08, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_StringTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtString tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_String()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			string value = "jkl;";
			byte[] expected = new byte[] { 0x08, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B };

			// Act
			writer.Write(name, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Write_ListTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtList tag = new NbtList("asdf", NbtTagType.String, new NbtString[] {
				new NbtString(string.Empty, "jkl;1"),
				new NbtString(string.Empty, "jkl;2"),
				new NbtString(string.Empty, "jkl;3"),
				new NbtString(string.Empty, "jkl;4")
			});
			byte[] expected = new byte[] { 0x09, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x08, 0x00, 0x00, 0x00, 0x04, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x31, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x32, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x33, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x34 };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_ListTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtList tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_List()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			NbtTagType elementType = NbtTagType.String;
			NbtString[] value = new NbtString[] {
				new NbtString(string.Empty, "jkl;1"),
				new NbtString(string.Empty, "jkl;2"),
				new NbtString(string.Empty, "jkl;3"),
				new NbtString(string.Empty, "jkl;4")
			};
			byte[] expected = new byte[] { 0x09, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x08, 0x00, 0x00, 0x00, 0x04, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x31, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x32, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x33, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x34 };

			// Act
			writer.Write(name, elementType, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Write_CompoundTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtCompound tag = new NbtCompound("asdf", new NbtTagCollection
		    {
		        new NbtDouble("asdf", 3.14159265358979311599796346854E0),
		        new NbtShort("asdf", 12345)
		    });
			byte[] expected = new byte[] { 0x0A, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x06, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18, 0x02, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x30, 0x39, 0x00 };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_CompoundTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtCompound tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_Compound()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			NbtTag[] value = new NbtTag[]
		    {
		        new NbtDouble("asdf", 3.14159265358979311599796346854E0),
		        new NbtShort("asdf", 12345)
		    };
			byte[] expected = new byte[] { 0x0A, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x06, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18, 0x02, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x30, 0x39, 0x00 };

			// Act
			writer.Write(name, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Write_IntArrayTag()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtIntArray tag = new NbtIntArray("asdf", new int[] { 12345, 1337, 123456789, 55555555 });
			byte[] expected = new byte[] { 0x0B, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x30, 0x39, 0x00, 0x00, 0x05, 0x39, 0x07, 0x5B, 0xCD, 0x15, 0x03, 0x4F, 0xB5, 0xE3 };

			// Act
			writer.Write(tag);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Write_IntArrayTag_ArgumentNullException()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			NbtIntArray tag = null;

			// Act
			writer.Write(tag);
		}

		[TestMethod]
		public void Write_IntArray()
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			string name = "asdf";
			int[] value = new int[] { 12345, 1337, 123456789, 55555555 };
			byte[] expected = new byte[] { 0x0B, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x30, 0x39, 0x00, 0x00, 0x05, 0x39, 0x07, 0x5B, 0xCD, 0x15, 0x03, 0x4F, 0xB5, 0xE3 };

			// Act
			writer.Write(name, value);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}
	}
}
