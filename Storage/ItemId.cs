using Farming.Core;

namespace Farming.Storage
{
    public record ItemId(string Value) : GameId(Value)
    {
        public override string ToString() => $"item.{base.ToString()}";
    }
}
