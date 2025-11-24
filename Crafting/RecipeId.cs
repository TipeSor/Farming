using Farming.Core;

namespace Farming.Crafting
{
    public record RecipeId(string Value) : GameId(Value)
    {
        public override string ToString() => $"recipe.{base.ToString()}";
    }
}
