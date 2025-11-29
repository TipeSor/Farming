using TipeUtils;

namespace Farming.Storage
{
    public class ItemSystem
    {
        public static Result Move(ref ItemStack target, ref ItemStack source, uint amount)
        {
            if (target.Id != source.Id)
                return Result.Error("Id missmatch");

            if (amount == 0)
                return Result.Ok();

            Result<ItemStack> target_result = Add(target, amount);
            Result<ItemStack> source_result = Remove(source, amount);

            if (target_result.IsError)
                return Result.Error($"Target: {target_result.Message}");

            if (source_result.IsError)
                return Result.Error($"Source: {source_result.Message}");

            target = target_result.Value;
            source = source_result.Value;

            return Result.Ok();
        }

        public static Result Merge(ref ItemStack target, ref ItemStack source)
        {
            return Move(ref target, ref source, source.Amount);
        }

        public static Result<ItemStack> Split(ref ItemStack source, uint amount)
        {
            ItemStack target = new(source.ItemData);
            Result result = Move(ref source, ref target, amount);
            if (result.IsError)
                return Result<ItemStack>.Error($"Failed to split stack: {result.Message}");

            return Result<ItemStack>.Ok(target);
        }

        internal static Result<ItemStack> Add(ItemStack target, uint amount)
        {
            if (uint.MaxValue - target.Amount < amount)
                return Result<ItemStack>.Error("Not enough space.");

            return Result<ItemStack>.Ok(new ItemStack(target.ItemData, target.Amount + amount));
        }

        internal static Result<ItemStack> Remove(ItemStack target, uint amount)
        {
            if (target.Amount < amount)
                return Result<ItemStack>.Error("Not enough items.");

            return Result<ItemStack>.Ok(new ItemStack(target.ItemData, target.Amount - amount));
        }
    }
}
