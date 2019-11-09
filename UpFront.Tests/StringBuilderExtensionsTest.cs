using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using UpFront;

namespace UpFront.Tests
{
    [TestClass]
    public class StringBuilderExtensionsTest
    {
        [TestMethod]
        public void Write()
        {
            var expected = 32.ToString() + int.MaxValue.ToString();

            var sb = new StringBuilder();
            sb.AppendPrimitive(32);
            sb.AppendPrimitive(int.MaxValue);

            var result = sb.ToString();
            Assert.IsTrue(result.Equals(expected, StringComparison.Ordinal));
        }
    }
}
