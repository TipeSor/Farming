using Farming.Core;
using Farming.Storage;
using Farming.UI;
using static Farming.Program;
namespace Farming.GameContent
{
    public class Player : GameObject
    {
        public override string Name { get; } = "Player";
        public BasicInventory Inventory { get; } = new();

        public override BaseMenu BuildMenu()
        {
            PagedMenuBuilder builder = new();

            builder.InventoryActions(Inventory);
            builder.AddItem("Add Item", action: BuildGiveMenu);

            return builder.Build();
        }

        private void BuildGiveMenu(MenuManager manager)
        {
            PagedMenuBuilder builder = new();

            foreach ((ItemId id, ItemData data) in Program.Content.ItemRegistry)
            {
                builder.AddItem(data.GetName(), m => AddItem(m, data));
            }

            manager.Next(builder.Build());
        }

        private void AddItem(MenuManager manager, ItemData data)
        {
            ItemStack items = new(data, 100);
            Inventory.AddItem(ref items);

            manager.Next(
                new DisplayMenuBuilder()
                    .Append($"Added 100x {data.GetName()}")
                    .SetAction(static m => m.Manager.Main())
                    .Build());
        }
    }
}
