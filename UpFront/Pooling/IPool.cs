namespace UpFront.Pooling
{
    public interface IPool<T> where T : class
    {
        int Length { get; }
        int Capacity { get; }

        void Compact();

        void Free(T obj);
        T New();

        void Clear();
    }
}
