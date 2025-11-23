using Farming.Core;

namespace Farming.Crafting
{
    public record RecipeId(string Id) : GameId(Id)
    {
        public static RecipeId BurnWood { get; } = new RecipeId("burn_wood");
        public static RecipeId SmeltIron { get; } = new RecipeId("smelt_iron");

        public override string ToString() => $"recipe.{base.ToString()}";
    }
}
