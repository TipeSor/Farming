using TipeUtils;

namespace Farming.Storage
{
    public record ExchangeOffer(ItemStack[] Offers)
    {
        public static readonly ExchangeOffer Empty = new([]);

        public static implicit operator ExchangeOffer(ItemStack[] offers)
        {
            return new ExchangeOffer(offers);
        }
    }

    public static class ExchangeSystem
    {
        public static Result Exchange(
            Inventory target,
            Inventory source,
            ExchangeOffer toTarget,
            ExchangeOffer toSource
        )
        {
            return LockAll([target, source], () =>
            {
                Inventory _target = target.Clone();
                Inventory _source = source.Clone();

                foreach (ItemStack stack in toTarget.Offers)
                {
                    ItemStack buffer = new(stack.ItemData);

                    Result source_result = _source.GetItem(ref buffer, stack.Amount);
                    if (source_result.IsError)
                        return Result.Error(
                            $"Failed to remove {stack.Amount}x {stack.Name} from source inventory: {source_result.Message}");

                    Result target_result = _target.AddItem(ref buffer);
                    if (target_result.IsError)
                        return Result.Error(
                            $"Failed to add {stack.Amount}x {stack.Name} to target inventory: {target_result.Message}");
                }

                foreach (ItemStack stack in toSource.Offers)
                {
                    ItemStack buffer = new(stack.ItemData);

                    Result target_result = _target.GetItem(ref buffer, stack.Amount);
                    if (target_result.IsError)
                        return Result.Error(
                            $"Failed to remove {stack.Amount}x {stack.Name} from target inventory: {target_result.Message}");

                    Result source_result = _source.AddItem(ref buffer);
                    if (source_result.IsError)
                        return Result.Error(
                            $"Failed to add {stack.Amount}x {stack.Name} to source inventory: {source_result.Message}");
                }

                target.ReplaceWith(_target);
                source.ReplaceWith(_source);
                return Result.Ok();
            });
        }


        public static TResult LockAll<TResult>(
            IEnumerable<Inventory> inventories,
            Func<TResult> action)
        {
            object[] ordered = [.. inventories
                .Select(static inv => new { Lock = inv.SyncRoot, Id = inv.InventoryId })
                .OrderBy(static x => x.Id)
                .Select(static x => x.Lock)];

            return LockRecursive(ordered, 0, action);
        }

        private static TResult LockRecursive<TResult>(
            object[] locks, int index, Func<TResult> action)
        {
            if (index == locks.Length)
                return action();

            lock (locks[index])
                return LockRecursive(locks, index + 1, action);
        }
    }
}
