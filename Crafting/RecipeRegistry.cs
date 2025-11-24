using Farming.Storage;
using Farming.Core;
using TipeUtils;

namespace Farming.Crafting
{
    public class RecipeRegistry : Registry<RecipeId, RecipeData>
    {
        public Result<RecipeData> Register(RecipeId id, ItemStack[] input, ItemStack[] output)
            => Register(id, () => new(id, input, output));
    }
}
