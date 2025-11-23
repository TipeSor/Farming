using Farming.Storage;
using TipeUtils;

namespace Farming.Crafting
{
    public static class RecipeRegistry
    {
        private static readonly Registry<RecipeId, RecipeData> registry = new();

        public static Result<RecipeData> Register(RecipeId id, ItemStack[] input, ItemStack[] output)
            => registry.Register(id, () => new(id, input, output));

        public static Result<RecipeData> Get(RecipeId id)
            => registry.Get(id);

        public static IEnumerable<KeyValuePair<RecipeId, RecipeData>> All => registry;
    }
}
