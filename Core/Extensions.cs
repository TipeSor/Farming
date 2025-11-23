using System.Numerics;
using Farming.Storage;
using Farming.UI;

namespace Farming.Core
{
    public static class Extensions
    {
        public static PredicateBuilder<T> InRange<T>(this PredicateBuilder<T> builder, T min, T max)
            where T : INumber<T>
        {
            builder.Add(x => x >= min && x <= max);
            return builder;
        }

        public static PredicateBuilder<T> Where<T>(this PredicateBuilder<T> builder, Predicate<T> predicate)
            => builder.Add(predicate);

        public static DisplayMenu Show(this Inventory inventory)
        {
            DisplayMenuBuilder builder = new();
            foreach ((ItemId id, ItemStack item) in inventory)
            {
                builder
                    .Append($"{item.Amount}x {item.Name}")
                    .NewLine();
            }
            return builder
                .SetAction(static m => m.Manager.Main())
                .Build();
        }

        public static PagedMenuBuilder InventoryActions(
            this PagedMenuBuilder builder,
            Inventory inventory
        )
        {
            return builder
                .AddItem("Show Inventory", m => m.Manager.Next(inventory.Show()));
        }
    }
}
