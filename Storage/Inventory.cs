using System.Collections;
using TipeUtils;

namespace Farming.Storage
{
    public class Inventory : IEnumerable<KeyValuePair<ItemId, ItemStack>>
    {
        private Dictionary<ItemId, ItemStack> Items { get; set; }

        internal object SyncRoot { get; } = new();

        private static long _idCounter;
        public long InventoryId { get; } = Interlocked.Increment(ref _idCounter);

        internal Inventory(Dictionary<ItemId, ItemStack> items)
        {
            Items = new Dictionary<ItemId, ItemStack>(items);
        }

        public Inventory()
            : this([]) { }

        public Result AddItem(ref ItemStack source)
        {
            lock (SyncRoot)
            {
                if (!Items.TryGetValue(source.Id, out ItemStack target))
                {
                    target = new ItemStack(source.ItemData, 0);
                }


                Result result = ItemUtils.Merge(ref target, ref source);
                if (result.IsError)
                    return Result.Error($"Failed to merge item {source.Name}: {result.Message}");

                Items[source.Id] = target;
                return Result.Ok();
            }
        }

        public Result GetItem(ref ItemStack target, uint amount)
        {
            lock (SyncRoot)
            {
                if (!Items.TryGetValue(target.Id, out ItemStack source))
                    return Result.Error($"Item {target.Name} not found.");


                Result result = ItemUtils.Move(ref target, ref source, amount);
                if (result.IsError)
                    return Result.Error(
                        $"Failed to move {amount} item to target: {result.Message}");

                Items[source.Id] = source;
                return Result.Ok();
            }
        }

        public Inventory Clone()
        {
            return new Inventory(new Dictionary<ItemId, ItemStack>(Items));
        }

        internal void ReplaceWith(Inventory newInv)
        {
            Items = new Dictionary<ItemId, ItemStack>(newInv.Items);
        }

        public IEnumerator<KeyValuePair<ItemId, ItemStack>> GetEnumerator()
        {
            lock (SyncRoot)
            {
                return Items.ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
