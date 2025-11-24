using Farming.Core;
using TipeUtils;

namespace Farming.Storage
{
    public class ItemRegistry : Registry<ItemId, ItemData>
    {
        public Result<ItemData> Register(ItemId id, string name)
            => Register(id, () => new(id, name));
    }
}
