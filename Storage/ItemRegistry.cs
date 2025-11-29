using Farming.Core;
using TipeUtils;

namespace Farming.Storage
{
    public class ItemRegistry : Registry<ItemId, ItemData>
    {
        public Result<ItemData> GetData(ItemId id) => Get(id);

        public Result<ItemStack> CreateStack(ItemId id, uint amount)
            => GetData(id).Map(data => new ItemStack(data, amount));
    }
}
