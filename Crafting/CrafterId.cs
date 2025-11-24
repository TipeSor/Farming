using Farming.Core;

namespace Farming.Crafting
{
    public record CrafterId(string Value) : GameId(Value)
    {
        public override string ToString() => $"crafter.{base.ToString()}";
    }
}
