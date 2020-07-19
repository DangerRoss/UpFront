using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using UpFront;

namespace UpFront.Tests
{
    [TestClass]
    public class MutableStringTest
    {
        [TestMethod]
        public void WriteString()
        {
            var expected = "Hello W0rld!";
            int l = expected.Length;

            var mstring = new MutableString();
            mstring.Write(expected);

            var result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteCharArray()
        {
            var hello = "hello";
            var world = "world";

            var expected = hello + world;

            var mstring = new MutableString();
            mstring.Write(hello.ToCharArray());
            mstring.Write(world.ToCharArray());

            var result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteInt()
        {
            string expected = "0";

            var mstring = new MutableString();
            mstring.WriteInt(0);

            var result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteMinInt()
        {
            string expected = int.MinValue.ToString();

            var mstring = new MutableString();
            mstring.WriteInt(int.MinValue);

            var result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WritePointer()
        {
            var expected = "Hello World";

            int spaceIndex = expected.IndexOf(' ');

            var mstring = new MutableString(2);

            unsafe
            {
                fixed (char* expectedptr = expected)
                {
                    mstring.Write(expectedptr, 0, spaceIndex);

                    mstring.Write(expectedptr, spaceIndex, expected.Length - spaceIndex);
                }
            }
            var result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteMaxInt()
        {
            string expected = int.MaxValue.ToString();

            var mstring = new MutableString();
            mstring.WriteInt(int.MaxValue);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteMaxUInt()
        {
            string expected = uint.MaxValue.ToString();

            var mstring = new MutableString();
            mstring.WriteUInt(uint.MaxValue);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteMinLong()
        {
            string expected = long.MinValue.ToString();
            var l = expected.Length;
            var mstring = new MutableString();
            mstring.WriteLong(long.MinValue);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteMaxULong()
        {
            string expected = ulong.MaxValue.ToString();
            var l = expected.Length;
            var mstring = new MutableString();
            mstring.WriteULong(ulong.MaxValue);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void StringInitialisation()
        {
            MutableString mstring = "init0";

            var expected = "init0";
            string result = (string)mstring;

            Assert.IsTrue(mstring.Length == 5);
            Assert.IsTrue(mstring.Capacity >= 5);
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void ExpandAndWrite()
        {
            string expected = "valuevalue";

            MutableString mstring = "value";

            mstring.Write("value");

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void InitEmptyAndWrite()
        {
            string expected = "value";
            MutableString mstring = string.Empty;
            mstring.Write(expected);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteNegFloat()
        {
            var expected = "-1.2";
            var mstring = new MutableString();
            mstring.WriteFloat(-1.215645f, 1);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteFloatFraction()
        {
            var expected = "0.5";
            var mstring = new MutableString();
            mstring.WriteFloat(0.5f, 1);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteFloatNaN()
        {
            var expected = "NaN";
            var mstring = new MutableString();
            mstring.WriteFloat(float.NaN, 1);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteFloatRounded()
        {
            var value = 1.23456f;
            var expected = Math.Round(value, 5).ToString("0.00000");
            var mstring = new MutableString();
            mstring.WriteFloat(value, 5);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteFloatSpecific()
        {
            var value = 1.463f;
            var spec = 8;
            var expected = Math.Round(value, spec).ToString();
            var mstring = new MutableString();
            mstring.WriteFloat(value, spec);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteDouble()
        {
            var expected = "1.2";
            var mstring = new MutableString();
            mstring.WriteDouble(1.2d, 1);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteWholeDouble()
        {
            var expected = "1.00";
            var mstring = new MutableString();
            mstring.WriteDouble(1d, 2);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteWholeDoubleNoRemainder()
        {
            var expected = "1";
            var mstring = new MutableString();
            mstring.WriteDouble(1d, 0);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteZeroDouble()
        {
            var expected = "0.00";
            var mstring = new MutableString();
            mstring.WriteDouble(0d, 2);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void WriteInfinityDouble()
        {
            var expected = double.PositiveInfinity.ToString();
            var mstring = new MutableString();
            mstring.WriteDouble(double.PositiveInfinity, 2);

            string result = (string)mstring;
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void Compact()
        {
            string expected = "1234";
            MutableString mstring = "123456789";

            mstring.Position = 4;
            mstring.Compact();

            string result = (string)mstring;

            Assert.IsTrue(mstring.Capacity == 4);
            Assert.IsTrue(expected.Equals(result));
        }
    }
}
