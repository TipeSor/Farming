namespace Farming.Storage
{
    public readonly record struct ItemStack
    {
        public ItemData ItemData { get; }
        public uint Amount { get; }

        public ItemId Id => ItemData.Id;

        public ItemStack(ItemData itemData, uint amount)
        {
            ItemData = itemData;
            Amount = amount;
        }

        public ItemStack(ItemData itemData)
            : this(itemData, 0) { }
    }
}
