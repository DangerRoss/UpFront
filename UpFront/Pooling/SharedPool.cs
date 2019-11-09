using System;
using System.Runtime.CompilerServices;

namespace UpFront.Pooling
{
    public sealed class SharedPool<T> : IPool<T> where T : class
    {
        private Pool<T> pool;

        public SharedPool(Func<T> factory) : this(factory, 16, 4) { }

        public SharedPool(Func<T> factory, int initialsize, int batchcount) 
        {
            this.pool = new Pool<T>(factory, initialsize, batchcount);
        }

        public int Length => pool.Length;

        public int Capacity => pool.Capacity;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Clear() => pool.Clear();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Compact() => pool.Compact();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Free(T obj) => pool.Free(obj);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public T New() => pool.New();       
    }
}
