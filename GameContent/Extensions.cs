using Farming.Storage;
using Farming.UI;

namespace Farming.GameContent
{
    public static class Extensions
    {
        public static void Show(this IInventory inventory, MenuManager manager)
        {
            DisplayMenuBuilder builder = new();
            foreach ((ItemId id, ItemStack item) in inventory)
            {
                builder
                    .Append($"{item.Amount}x {item.ItemData.GetName()}")
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

        public static string GetName(this ItemData data)
            => data.GetAttribute<NameAttribute>()
                .Match(onSuccess: static atr => atr.Name, onError: msg => data.Id);

        public static string GetName(this ItemRegistry registry, ItemId id)
            => registry.Get(id).Match(
                onSuccess: static data => data.GetName(),
                onError: static msg => "unknown");

        public static string GetName(this ItemStack stack)
            => stack.ItemData.GetName();

        public static ItemCreator SetName(this ItemCreator creator, string Name)
            => creator.AddAttribute(new NameAttribute(Name));
    }

    public record NameAttribute(string Name) : IItemAttribute;
}
