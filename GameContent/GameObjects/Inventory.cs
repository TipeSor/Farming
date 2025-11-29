using System.Collections;
using Farming.Concurrency;
using Farming.GameContent;
using TipeUtils;

namespace Farming.Storage
{
    public class BasicInventory : IInventory
    {
        private Dictionary<ItemId, ItemStack> Items { get; set; }

        public LockId<IInventory> LockId { get; } = new();
        public object SyncRoot { get; } = new object();

        public event EventHandler<ItemId>? OnItemAdded;
        public event EventHandler<ItemId>? OnItemRemoved;

        internal BasicInventory(Dictionary<ItemId, ItemStack> items)
        {
            Items = new Dictionary<ItemId, ItemStack>(items);
        }

        public BasicInventory()
            : this([]) { }


        public Result AddItem(ref ItemStack source)
        {
            lock (SyncRoot)
            {
                if (!Items.TryGetValue(source.Id, out ItemStack target))
                {
                    target = new ItemStack(source.ItemData, 0);
                }

                Result result = ItemSystem.Merge(ref target, ref source);
                if (result.IsError)
                    return Result.Error($"Failed to merge item {source.GetName()}: {result.Message}");

                Items[source.Id] = target;
            }
            OnItemAdded?.Invoke(this, source.Id);
            return Result.Ok();
        }

        public Result GetItem(ref ItemStack target, uint amount)
        {
            lock (SyncRoot)
            {
                if (!Items.TryGetValue(target.Id, out ItemStack source))
                    return Result.Error($"Item {target.GetName()} not found.");

                Result result = ItemSystem.Move(ref target, ref source, amount);
                if (result.IsError)
                    return Result.Error(
                        $"Failed to move {amount} item to target: {result.Message}");

                Items[source.Id] = source;
            }
            OnItemRemoved?.Invoke(this, target.Id);
            return Result.Ok();
        }

        public IEnumerator<(ItemId Id, ItemStack Stack)> GetEnumerator()
        {
            lock (SyncRoot)
            {
                return Items.Select(static kvp => (kvp.Key, kvp.Value)).ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IInventory Clone()
        {
            return new BasicInventory(new Dictionary<ItemId, ItemStack>(Items));
        }

        public void ReplaceWith(IInventory inventory)
        {
            Items = inventory.ToDictionary(static i => i.Id, static i => i.Stack);
        }
    }
}
