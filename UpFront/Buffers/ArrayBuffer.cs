using System;
using System.Runtime.CompilerServices;

namespace UpFront.Buffers
{
    /// <summary>
    /// A growable sized buffer with a seekable write cursor backed by a managed array
    /// </summary>
    public class ArrayBuffer<T> : IDynamicBuffer<T> where T : unmanaged
    {
        public T this[int index] { get => this.buffer[index]; set => this.buffer[index] = value; }

        public int Position
        {
            get => this.usagebound;
            set
            {
                if (value < 0 || value > this.buffer.Length) { throw new IndexOutOfRangeException(); }
                this.usagebound = value;
            }
        }

        public int Length  => this.usagebound;

        public int Capacity => this.buffer.Length;

        protected T[] buffer;
        protected int usagebound;

        public ArrayBuffer() : this(16) { }

        public ArrayBuffer(int initialCapacity)
        {
            this.usagebound = 0;
            this.buffer = new T[initialCapacity];
        }

        public void Clear() => this.Clear(0, this.Capacity);

        public void Clear(int offset, int length)
        {
            Array.Clear(this.buffer, offset, length);
            this.usagebound = 0;
        }

        public void Compact()
        {
            var l = this.Length;

            if (this.Capacity > l)
            {
                Array.Resize(ref this.buffer, l);
            }
        }

        public void Reserve(int count)
        {
            int required = this.usagebound + count;
            int size = this.Capacity;

            if (required > 0 && required >= size)
            {
                if (size == 0)
                {
                    size = required;
                }
                else 
                {
                    size = (size * 2);

                    if(required > size)
                    {
                        size = required;
                    }
                }
                Array.Resize(ref this.buffer, size);
            }         
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Seek(int position) => this.Position = position;

        public T[] ToArray()
        {
            int count = this.Length;
            if (count > 0)
            {
                T[] values = new T[count];
                Array.Copy(this.buffer, 0, values, 0, count);
                return values;
            }
            else
            {
                return Array.Empty<T>();
            }
        }

        public void Write(T value)
        {
            this.Reserve(1);
            this.Append(value);
        }

        public void Write(T[] values) => this.Write(values, 0, values.Length);

        public void Write(T[] values, int offset, int length)
        {
            this.Reserve(length);
            this.Append(values, offset, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Append(T value) { this.buffer[this.usagebound++] = value; }

        protected void Append(T[] values, int offset, int length)
        {
            int upperArgbound = offset + length;
            for (int i = offset; i < upperArgbound; i++)
            {
                this.buffer[this.usagebound++] = values[i];
            }
        }

        public T[] GetArray() => this.buffer;

        public ref T GetReference() => ref this.buffer[0];
    }  
}
