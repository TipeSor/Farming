using Farming.Storage;

namespace Farming.Crafting
{
    public record RecipeData(RecipeId ID, ItemStack[] Input, ItemStack[] Output);
}
