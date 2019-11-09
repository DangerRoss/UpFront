using System.Collections.Generic;

namespace UpFront.Buffers
{
    /// <summary>
    /// A growable sized buffer with a seekable write cursor
    /// </summary>
    public interface IDynamicBuffer<T> : IBuffer<T> where T : unmanaged 
    {
        /// <summary>
        /// Assures there is enough free space in the buffer
        /// </summary>
        /// <param name="count">minium free space requested</param>
        void Reserve(int count);

        /// <summary>
        /// Resizes the buffer to the current upper bound removing excess space
        /// </summary>
        void Compact();
    }


    public static class IDynamicBufferExtensions
    {
        public static void CopyTo<T>(this ICollection<T> collection, IDynamicBuffer<T> buffer) where T : unmanaged
        {
            var count = collection.Count;
            buffer.Reserve(count);

            foreach (var value in collection)
            {
                buffer.Write(value);
            }
        }
    }
}
