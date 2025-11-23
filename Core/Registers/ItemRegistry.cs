using TipeUtils;

namespace Farming.Storage
{
    public static class ItemRegistry
    {
        private static readonly Registry<ItemId, ItemData> registry = new();

        public static Result<ItemData> Register(ItemId id, string name)
            => registry.Register(id, () => new(id, name));

        public static Result<ItemData> Get(ItemId id)
            => registry.Get(id);

        public static IEnumerable<KeyValuePair<ItemId, ItemData>> All => registry;
    }
}
