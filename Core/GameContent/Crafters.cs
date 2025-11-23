using Farming.Core;

namespace Farming.Crafting
{
    public record Crafters
    {
        public static Crafter CreateFurnace() => new(CrafterData.Furnace);
    }
}
