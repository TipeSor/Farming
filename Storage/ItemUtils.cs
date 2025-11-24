using TipeUtils;

namespace Farming.Storage
{
    public class ItemUtils
    {
        public static Result Move(ref ItemStack target, ref ItemStack source, uint amount)
        {
            if (target.Id != source.Id)
                return Result.Error("");

            Result<ItemStack> target_result = target.Add(amount);
            Result<ItemStack> source_result = source.Remove(amount);

            if (target_result.IsError)
                return Result.Error($"target: {target_result.Message}");

            if (source_result.IsError)
                return Result.Error($"source: {source_result.Message}");

            target = target_result.Value;
            source = source_result.Value;

            return Result.Ok();
        }

        public static Result Merge(ref ItemStack target, ref ItemStack source)
        {
            return Move(ref target, ref source, source.Amount);
        }

        public static Result<ItemStack> Split(ref ItemStack item, uint amount)
        {
            Result<ItemStack> result = item.Remove(amount);
            if (result.IsError)
                return result;

            item = result.Value;
            return Result<ItemStack>.Ok(new ItemStack(item.ItemData, amount));
        }
    }
}
