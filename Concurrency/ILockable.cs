namespace Farming.Concurrency
{
    public interface ILockable<T>
    {
        LockId<T> LockId { get; }
        object SyncRoot { get; }
    }
}
