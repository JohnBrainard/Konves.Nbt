using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.Nbt.Tests
{
	[TestClass]
	public class NbtTagInfoTests
	{
		[TestMethod]
		public void OperatorToNbtTagInfo_Normal()
		{
			// Arrange
			NbtTagType expectedTagType = NbtTagType.Float;
			byte[] data = new byte[] { (byte)expectedTagType, 0x00, 0xA0 };

			int expectedNameLength = 0x00A0;

			// Act
			NbtTagInfo result = (NbtTagInfo)data;

			// Assert
			Assert.AreEqual(expectedTagType, result.Type);
			Assert.AreEqual(expectedNameLength, result.NameLength);
		}

		[TestMethod]
		public void OperatorToNbtTagInfo_End()
		{
			// Arrange
			NbtTagType expectedTagType = NbtTagType.End;
			byte[] data = new byte[] { (byte)expectedTagType };

			int expectedNameLength = 0;

			// Act
			NbtTagInfo result = (NbtTagInfo)data;

			// Assert
			Assert.AreEqual(expectedTagType, result.Type);
			Assert.AreEqual(expectedNameLength, result.NameLength);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void OperatorToNbtTagInfo_ArgumentNullException()
		{
			// Arrange
			byte[] data = null;

			// Act
			NbtTagInfo result = (NbtTagInfo)data;
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void OperatorToNbtTagInfo_ArgumentException()
		{
			// Arrange
			byte[] data = new byte[] { 0x04, 0x03 };

			// Act
			NbtTagInfo result = (NbtTagInfo)data;
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void OperatorToNbtTagInfo_ArgumentException_LongEndTag()
		{
			// Arrange
			byte[] data = new byte[] { 0x00, 0x04, 0x03 };

			// Act
			NbtTagInfo result = (NbtTagInfo)data;
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void OperatorToNbtTagInfo_ArgumentOutOfRangeException()
		{
			// Arrange
			byte[] data = new byte[] { 0xFF, 0x00, 0x03 };

			// Act
			NbtTagInfo result = (NbtTagInfo)data;
		}
	}
}
