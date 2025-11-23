using Farming.Storage;
using Farming.UI;

namespace Farming.Core
{
    public class Player : GameObject
    {
        public override string Name => "Player";
        public Inventory Inventory { get; } = new();

        public override BaseMenu BuildMenu()
        {
            PagedMenuBuilder builder = new();

            builder.InventoryActions(Inventory);
            builder.AddItem("Add Item", m => m.Manager.Next(BuildGiveMenu()));

            return builder.Build();
        }

        private PagedMenu BuildGiveMenu()
        {
            PagedMenuBuilder builder = new();

            foreach ((ItemId id, ItemData data) in ItemRegistry.All)
            {
                builder.AddItem(data.Name, m => m.Manager.Next(AddItem(data)));
            }

            return builder.Build();
        }

        private DisplayMenu AddItem(ItemData data)
        {
            ItemStack items = new(data, 100);
            Inventory.AddItem(ref items);

            return new DisplayMenuBuilder()
                .Append($"Added 100x {data.Name}")
                .SetAction(static m => m.Manager.Main())
                .Build();
        }
    }
}
