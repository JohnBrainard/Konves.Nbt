using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Konves.Nbt.Serialization;
using System.Collections;
using Konves.Testing;

namespace Konves.Nbt.Tests
{
	[TestClass]
	public class SerializationInfoTests
	{
		const string SerializationInfo = "Konves.Nbt.Serialization.SerializationInfo";

		[TestMethod]
		public void TryGetNbtTagType_Byte()
		{
			Do_TryGetNbtTagType(typeof(byte), NbtTagType.Byte, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_Short()
		{
			Do_TryGetNbtTagType(typeof(short), NbtTagType.Short, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_Int()
		{
			Do_TryGetNbtTagType(typeof(int), NbtTagType.Int, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_Long()
		{
			Do_TryGetNbtTagType(typeof(long), NbtTagType.Long, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_Float()
		{
			Do_TryGetNbtTagType(typeof(float), NbtTagType.Float, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_Double()
		{
			Do_TryGetNbtTagType(typeof(double), NbtTagType.Double, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_ByteArray()
		{
			Do_TryGetNbtTagType(typeof(byte[]), NbtTagType.ByteArray, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_IEnumerableOfByte()
		{
			Do_TryGetNbtTagType(typeof(IEnumerable<byte>), NbtTagType.ByteArray, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_String()
		{
			Do_TryGetNbtTagType(typeof(string), NbtTagType.String, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_IntArray()
		{
			Do_TryGetNbtTagType(typeof(int[]), NbtTagType.IntArray, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_IEnumerableOfInt()
		{
			Do_TryGetNbtTagType(typeof(IEnumerable<int>), NbtTagType.IntArray, null);
		}

		[TestMethod]
		public void TryGetNbtTagType_ListFromArray()
		{
			Do_TryGetNbtTagType(typeof(float[]), NbtTagType.List, NbtTagType.Float);
		}

		[TestMethod]
		public void TryGetNbtTagType_ListFromIEnumerable()
		{
			Do_TryGetNbtTagType(typeof(IEnumerable<float>), NbtTagType.List, NbtTagType.Float);
		}

		[TestMethod]
		public void TryGetNbtTagType_CompoundListFromArray()
		{
			Do_TryGetNbtTagType(typeof(EmptyClass[]), NbtTagType.List, NbtTagType.Compound);
		}

		[TestMethod]
		public void TryGetNbtTagType_CompoundListFromIEnumerable()
		{
			Do_TryGetNbtTagType(typeof(IEnumerable<EmptyClass>), NbtTagType.List, NbtTagType.Compound);
		}

		[TestMethod]
		public void TryGetNbtTagType_Compound()
		{
			Do_TryGetNbtTagType(typeof(EmptyClass), NbtTagType.Compound, null);
		}

		private void Do_TryGetNbtTagType(Type type, NbtTagType expectedTagType, NbtTagType? expectedElementType)
		{
			// Arrange
			TypeProxy proxy = TypeProxy.For("Konves.Nbt", "Konves.Nbt.Serialization.SerializationInfo");
			object[] parameters = new object[] { type, null, null };

			// Act
			proxy.Invoke("TryGetNbtTagType", parameters);

			// Assert
			Assert.AreEqual(expectedTagType, (NbtTagType)parameters[1]);
			Assert.AreEqual(expectedElementType, (NbtTagType?)parameters[2]);
		}

		[TestMethod]
		public void GetSerializationInfo()
		{
			// Arrange
			TypeProxy serializationInfo = TypeProxy.For("Konves.Nbt", "Konves.Nbt.Serialization.SerializationInfo");
			Type type = typeof(TestClass);

			InstanceProxy[] expected = new InstanceProxy[]
			{
				InstanceProxy.For(serializationInfo, NbtTagType.String, "stringProperty", null, null, null),
				InstanceProxy.For(serializationInfo, NbtTagType.Int, "IntProperty", null, null, null),
				InstanceProxy.For(serializationInfo, NbtTagType.List, "ListProperty", null, null, NbtTagType.Float)
			};

			// Act
			object actual = serializationInfo.Invoke("GetSerializationInfo", type);

			// Assert
			CollectionAssert.AreEqual(expected, (object[])actual, new InstanceProxyComparer("TagName", "TagType", "ElementType"));
		}

		public class EmptyClass { }

		public class TestClass
		{
			[NbtString("stringProperty")]
			public string StringProperty { get; set; }

			[NbtIgnore]
			public string IgnoredProperty { get; set; }

			public int IntProperty { get; set; }

			public float[] ListProperty { get; set; }
		}
	}
}
