using Farming.Core;

namespace Farming.Crafting
{
    public record CrafterId(string Id) : GameId(Id)
    {
        public static CrafterId Furnace { get; } = new CrafterId("furnace");

        public override string ToString() => $"crafter.{base.ToString()}";
    }
}
