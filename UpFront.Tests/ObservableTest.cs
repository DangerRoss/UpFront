using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using UpFront.Events;

namespace UpFront.Tests
{
    [TestClass]
    public class ObservableTest
    {
        [TestMethod]
        public void Invoke()
        {
            var called = false;

            var obs = new Observable();
            obs += () => called = true;
            obs.Notify();
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void SubAndUnsubInvoke()
        {
            int result = 0;
            var obs = new Observable();
            
            var inc = new Action(() => result++);

            obs += inc;
            obs += inc;

            obs.Notify();

            obs -= inc;

            obs.Notify();

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void UnsubcribeAll()
        {
            int result = 0;
            var obs = new Observable();

            var inc = new Action(() => result++);

            obs += inc;
            obs += inc;
            obs += () => result++;

            obs.Notify();

            obs.UnsubscribeAll(inc);

            obs.Notify();

            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void ClearSubscribers()
        {
            int result = 0;
            var obs = new Observable();

            obs += () => result++;
            obs += () => result++;

            obs.Notify();

            obs.ClearSubscribers();

            obs.Notify();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void ObservableValue()
        {
            int result = 0;

            var obs = new Observable<int>();

            obs += (number) => result += number;

            obs.Notify(10);

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void ClearSubsOnInvoke()
        {
            int result = 0;

            var obs = new Observable();

            obs += () =>
            {
                result++;
                obs.ClearSubscribers();
            };

            obs += () => result++;

            obs.Notify();

            obs.Notify();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void CancelNotify()
        {
            var obs = new Observable();
            var i = 0;

            obs += () => i++;

            obs += () => 
            {
                i++;
                obs.CancelNotify();
            };

            obs += () => i++;

            obs.Notify();


            Assert.AreEqual(2, i);
        }

        [TestMethod]
        public void IsSubscribed()
        {
            var obs = new Observable();
            obs += Obsver;

            Assert.IsTrue(obs.IsSubscribed(Obsver));
            Assert.IsTrue(obs.HasSubscribers());
        }

        private void Obsver() { }
    }
}
