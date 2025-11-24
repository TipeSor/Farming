namespace Farming.Concurrency
{
    public sealed record LockId<T>
    {
        private static long _counter;
        public long Value { get; } = Interlocked.Increment(ref _counter);

        public static implicit operator long(LockId<T> lockId)
        {
            return lockId.Value;
        }
    }
}
