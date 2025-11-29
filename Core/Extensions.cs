using Farming.Storage;
using Farming.UI;

namespace Farming.Core
{
    public static class Extensions
    {
        public static void Show(this IInventory inventory, MenuManager manager)
        {
            DisplayMenuBuilder builder = new();
            foreach ((ItemId id, ItemStack item) in inventory)
            {
                builder
                    .Append($"{item.Amount}x {item.Id}")
                    .NewLine();
            }
            manager.Next(
                builder
                    .SetAction(static m => m.Manager.Main())
                    .Build());
        }

        public static PagedMenuBuilder InventoryActions(
            this PagedMenuBuilder builder,
            IInventory inventory
        )
        {
            return builder
                .AddItem("Show Inventory", inventory.Show);
        }
    }
}
