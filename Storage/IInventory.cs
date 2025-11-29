using Farming.Concurrency;
using TipeUtils;

namespace Farming.Storage
{
    public interface IInventory : IEnumerable<(ItemId Id, ItemStack Stack)>, ILockable<IInventory>
    {
        Result AddItem(ref ItemStack source);
        Result GetItem(ref ItemStack target, uint amount);

        IInventory Clone();
        void ReplaceWith(IInventory inventory);
    }
}
