using TipeUtils;

namespace Farming.Crafting
{
    public static class CrafterRegistry
    {
        private static readonly Registry<CrafterId, CrafterData> registry = new();

        public static Result<CrafterData> Register(CrafterId id, string name, RecipeBook recipeBook)
            => registry.Register(id, () => new(id, name, recipeBook));

        public static Result<CrafterData> Get(CrafterId id)
            => registry.Get(id);

        public static IEnumerable<KeyValuePair<CrafterId, CrafterData>> All => registry;
    }
}
