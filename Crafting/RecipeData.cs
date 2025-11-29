using Farming.Storage;

namespace Farming.Crafting
{
    public record RecipeData(RecipeId Id, (ItemData Data, uint Amount)[] Input, (ItemData Data, uint Amount)[] Output);
}
