using TipeUtils;

namespace Farming.Storage
{
    public readonly record struct ItemStack
    {
        public ItemData ItemData { get; }
        public ItemId Id => ItemData.Id;
        public string Name => ItemData.Name;
        public uint Amount { get; }

        public ItemStack(ItemData itemData, uint amount)
        {
            ItemData = itemData;
            Amount = amount;
        }

        public ItemStack(ItemData itemData)
            : this(itemData, 0) { }

        public Result<ItemStack> Add(uint amount)
        {
            if (uint.MaxValue - Amount < amount)
                return Result<ItemStack>.Error("Not enough space");

            return Result<ItemStack>.Ok(new ItemStack(ItemData, Amount + amount));
        }

        public Result<ItemStack> Remove(uint amount)
        {
            if (Amount < amount)
                return Result<ItemStack>.Error("Not enough items");

            return Result<ItemStack>.Ok(new ItemStack(ItemData, Amount - amount));
        }
    }
}
