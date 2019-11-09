using Microsoft.VisualStudio.TestTools.UnitTesting;

using UpFront.Pooling;

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UpFront.Tests
{
    [TestClass]
    public class PoolTest
    {
        class Bar
        {
            public static int instanceCounter = 0;

            public long number = 0;

            public Bar()
            {
                Interlocked.Increment(ref instanceCounter);
            }

            [Initialiser]
            void Init() => this.number++;

            [ReInitialiser]
            void ReInit() => this.number++;

            [Finaliser]
            void Final() => this.number++;

            ~Bar()
            {
                Interlocked.Decrement(ref instanceCounter);
            }
        }

        [TestMethod]
        public void PrimaryTest()
        {
            IPool<Bar> pool = new Pool<Bar>(() => new Bar());

            for (int i = 0; i < 3; i++)
            {
                var bar = pool.New();
                pool.Free(bar);
            }

            Assert.IsTrue(pool.Length == 1);

            var barinstance = pool.New();
            pool.Free(barinstance);

            pool.Clear();

            Assert.IsTrue(Bar.instanceCounter == 1);
            Assert.IsTrue(barinstance.number == 6);
        }

        [TestMethod]
        public void IncreaseSize()
        {
            IPool<Bar> pool = new Pool<Bar>(() => new Bar(), 2, 1);

            var bar = pool.New();
            var bar2 = pool.New();
            var bar3 = pool.New();
            var bar4 = pool.New();

            pool.Free(bar);
            pool.Free(bar2);
            pool.Free(bar3);
            pool.Free(bar4);

            Assert.IsTrue(pool.Capacity == 4);
        }

        [TestMethod]
        public void Compact()
        {
            IPool<Bar> pool = new Pool<Bar>(() => new Bar());

            var bar = pool.New();
            var bar2 = pool.New();
            var bar3 = pool.New();
            var bar4 = pool.New();

            pool.Free(bar);
            pool.Free(bar2);
            pool.Free(bar3);
            pool.Free(bar4);

            pool.Compact();

            Assert.IsTrue(pool.Capacity == 4);
        }

        [TestMethod]
        public void MemoryLeakTest()
        {
            IPool<Bar> pool = new Pool<Bar>(() => new Bar());
            int inscount = 50;

            this.RequestInstancesAndFree(pool, inscount);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Assert.IsTrue(pool.Length == inscount);

            this.RequestInstancesAndNoFree(pool, inscount);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Assert.IsTrue(pool.Length == 0);
            Assert.IsTrue(Bar.instanceCounter == 0);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void RequestInstancesAndFree(IPool<Bar> pool, int count)
        {
            Bar[] instances = new Bar[count];

            for (int i = 0; i < count; i++)
            {
                instances[i] = pool.New();
            }

            foreach (var bizthing in instances)
            {
                pool.Free(bizthing);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void RequestInstancesAndNoFree(IPool<Bar> pool, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var ins = pool.New();
            }
        }
    }
}
