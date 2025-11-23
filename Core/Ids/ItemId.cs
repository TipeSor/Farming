using Farming.Core;

namespace Farming.Storage
{
    public record ItemId(string Id) : GameId(Id)
    {
        public static ItemId Stone { get; } = new ItemId("stone");
        public static ItemId Wood { get; } = new ItemId("wood");
        public static ItemId Coal { get; } = new ItemId("coal");
        public static ItemId IronOre { get; } = new ItemId("iron_ore");
        public static ItemId IronIngot { get; } = new ItemId("iron_ingot");

        public override string ToString() => $"item.{base.ToString()}";
    }
}
