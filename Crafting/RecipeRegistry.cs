using Farming.Storage;
using Farming.Core;
using TipeUtils;

namespace Farming.Crafting
{
    public class RecipeRegistry : Registry<RecipeId, RecipeData>
    {
        public Result<RecipeData> Register(RecipeId id, (ItemData Data, uint Amount)[] input, (ItemData Data, uint Amount)[] output)
            => Register(id, new(id, input, output));
    }
}
