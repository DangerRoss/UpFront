using System;
using System.Reflection;

namespace UpFront.Pooling
{
    /// <summary>
    /// Represents a pool of reusable objects
    /// </summary>
    public sealed class Pool<T> : IPool<T> where T : class
    {
        public int Length => this.usagebound;

        public int Capacity => this.pool.Length;

        private T[] pool;
        private int usagebound;
        private readonly int batchcount;

        private readonly Func<T> factory;

        private readonly MethodInfo initialiser;
        private readonly MethodInfo reinitialiser;
        private readonly MethodInfo finaliser;

        public Pool(Func<T> factory) : this(factory, 16, 4) { }

        public Pool(Func<T> factory, int initialsize, int batchcount)
        {
            this.factory = factory;
            this.batchcount = batchcount;
            this.usagebound = 0;
            this.pool = new T[initialsize];
            this.initialiser = Pool<T>.FindMethodHandler<Initialiser>();
            this.reinitialiser = Pool<T>.FindMethodHandler<ReInitialiser>();
            this.finaliser = Pool<T>.FindMethodHandler<Finaliser>();
        }

        public void Compact()
        {
            int l = this.Length;

            if (this.Capacity > l)
            {
                Array.Resize(ref this.pool, l);
            }
        }

        public void Free(T obj)
        {
            this.reinitialiser?.Invoke(obj, null);
            int capacity = this.Capacity;

            if (capacity == this.Length)
            {
                Array.Resize(ref this.pool, capacity + this.batchcount);
            }
            this.pool[this.usagebound++] = obj;
        }

        public T New()
        {
            if (usagebound > 0)
            {
                var obj = this.pool[--this.usagebound];
                this.pool[this.usagebound] = default;
                return obj;
            }
            else
            {
                var obj = this.factory();
                this.initialiser?.Invoke(obj, null);
                return obj;
            }
        }

        public void Clear()
        {
            if(this.finaliser != null)
            {
                for (int i = 0; i < this.Length; i++)
                {
                    this.finaliser.Invoke(this.pool[i], null);
                }
            }
            Array.Clear(this.pool, 0, this.Length);
            this.usagebound = 0;
        }

        internal static MethodInfo FindMethodHandler<A>() where A : Attribute
        {
            var type = typeof(T);

            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (method.GetCustomAttribute<A>() != null)
                {
                    return method;
                }
            }
            return null;
        }
    }
}
