namespace Farming.Storage
{
    public readonly record struct ItemStack
    {
        public readonly ItemData ItemData { get; }
        public readonly uint Amount { get; }

        public ItemId Id => ItemData.Id;
        public string Name => ItemData.Name;

        public ItemStack(ItemData itemData, uint amount)
        {
            ItemData = itemData;
            Amount = amount;
        }

        public ItemStack(ItemData itemData)
            : this(itemData, 0) { }
    }
}
