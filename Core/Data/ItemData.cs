namespace Farming.Storage
{
    public record ItemData(ItemId Id, string Name)
    {
        public static ItemData Stone { get; } = ItemRegistry.Register(ItemId.Stone, "Stone").Value!;
        public static ItemData Wood { get; } = ItemRegistry.Register(ItemId.Wood, "Wood").Value!;
        public static ItemData Coal { get; } = ItemRegistry.Register(ItemId.Coal, "Coal").Value!;
        public static ItemData IronOre { get; } = ItemRegistry.Register(ItemId.IronOre, "Iron Ore").Value!;
        public static ItemData IronIngot { get; } = ItemRegistry.Register(ItemId.IronIngot, "Iron Ingot").Value!;
    }
}

