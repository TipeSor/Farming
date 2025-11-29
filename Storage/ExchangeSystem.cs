using Farming.Concurrency;
using TipeUtils;

namespace Farming.Storage
{
    public static class ExchangeSystem
    {
        public static Result Exchange(
            IInventory target,
            IInventory source,
            ItemStack[] toTarget,
            ItemStack[] toSource
        )
        {
            return Locking.LockAll([target, source], () =>
            {
                IInventory _target = target.Clone();
                IInventory _source = source.Clone();

                foreach (ItemStack stack in toTarget)
                {
                    ItemStack buffer = new(stack.ItemData);

                    Result source_result = _source.GetItem(ref buffer, stack.Amount);
                    if (source_result.IsError)
                        return Result.Error(
                            $"Failed to remove {stack.Amount}x {stack.Id} from source inventory: {source_result.Message}");

                    Result target_result = _target.AddItem(ref buffer);
                    if (target_result.IsError)
                        return Result.Error(
                            $"Failed to add {stack.Amount}x {stack.Id} to target inventory: {target_result.Message}");
                }

                foreach (ItemStack stack in toSource)
                {
                    ItemStack buffer = new(stack.ItemData);

                    Result target_result = _target.GetItem(ref buffer, stack.Amount);
                    if (target_result.IsError)
                        return Result.Error(
                            $"Failed to remove {stack.Amount}x {stack.Id} from target inventory: {target_result.Message}");

                    Result source_result = _source.AddItem(ref buffer);
                    if (source_result.IsError)
                        return Result.Error(
                            $"Failed to add {stack.Amount}x {stack.Id} to source inventory: {source_result.Message}");
                }

                target.ReplaceWith(_target);
                source.ReplaceWith(_source);
                return Result.Ok();
            });
        }
    }
}
