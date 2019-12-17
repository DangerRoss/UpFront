using System;

namespace UpFront.Buffers
{
    /// <summary>
    /// Fixed sized buffer with a seekable write cursor
    /// </summary>
    public interface IBuffer<T> where T : unmanaged
    {
        T this[int index] { get; set; }


        /// <summary>
        /// The write cursor position (effectively the same as Length)
        /// </summary>
        int Position { get; set; }


        /// <summary>
        /// The upper bound of whats been written to the buffer
        /// </summary>
        int Length { get; }


        /// <summary>
        /// The total size of the buffer
        /// </summary>
        int Capacity { get; }


        /// <summary>
        /// Clears the buffer
        /// </summary>
        void Clear();


        /// <summary>
        /// Clears a range in the buffer
        /// </summary>
        void Clear(int offset, int length);


        /// <summary>
        /// Moves the write cursor relative to the buffer size
        /// </summary>
        /// <param name="position"></param>
        void Seek(int position);


        /// <summary>
        /// Returns a new array of what's been written to the buffer
        /// </summary>
        /// <returns></returns>
        T[] ToArray();


        void Write(T value);
        void Write(T[] values);
        void Write(T[] values, int offset, int length);


        /// <summary>
        /// Gets reference to the underlying buffer
        /// </summary>
        ref T GetReference();

#if NETSTANDARD2_1

        /// <summary>
        /// Gets a span containing whats been written to the buffer
        /// </summary>
        Span<T> GetSpan();

#endif

    }

    public static class IBufferExtensions
    {
        public static void Reset<T>(this IBuffer<T> buffer) where T : unmanaged => buffer.Seek(0);
    }
}
