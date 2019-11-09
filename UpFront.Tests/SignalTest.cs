using Microsoft.VisualStudio.TestTools.UnitTesting;

using UpFront.Events;

namespace UpFront.Tests
{
    [TestClass]
    public class SignalTest
    {
        [TestMethod]
        public void Invoke()
        {
            int result = 0;

            var signal = new Signal();

            signal += () => result++;
            signal += () => result++;
            signal += () => result++;
            signal += () => result++;

            signal.Notify();

            signal += () => result++;
            signal += () => result++;

            signal.Notify();

            signal.Notify();

            Assert.AreEqual(6, result);
        }
    }
}
