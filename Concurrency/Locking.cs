using TipeUtils;

namespace Farming.Concurrency
{
    public static class Locking
    {
        public static void LockAll<TLock>(
                IEnumerable<ILockable<TLock>> objs,
                Action action)
        {
            LockAll(objs, func: () => { action(); return Unit.Value; });
        }

        public static TResult LockAll<TLock, TResult>(
                IEnumerable<ILockable<TLock>> objs,
                Func<TResult> func)
        {
            object[] ordered = [.. objs
                .Select(static obj => new { Lock = obj.SyncRoot, Id = obj.LockId })
                .OrderBy(static x => x.Id)
                .Select(static x => x.Lock)];

            return RunLocked(ordered, func);
        }

        private static TResult RunLocked<TResult>(
            object[] locks, Func<TResult> action)
        {
            int acquired = 0;
            try
            {
                for (; acquired < locks.Length; acquired++)
                {
                    Monitor.Enter(locks[acquired]);
                }

                return action();
            }
            finally
            {
                for (int i = acquired - 1; i >= 0; i--)
                {
                    Monitor.Exit(locks[i]);
                }
            }
        }
    }
}
