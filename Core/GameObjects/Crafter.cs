using System.Text;
using Farming.Crafting;
using Farming.Storage;
using Farming.UI;
using TipeUtils;

namespace Farming.Core
{
    public class Crafter(CrafterData data) : GameObject
    {
        public override string Name { get; } = data.Name;
        private readonly RecipeBook recipeBook = data.RecipeBook;

        public override BaseMenu BuildMenu()
        {
            PagedMenuBuilder builder = new();
            builder.AddItem("Craft", m => m.Manager.Next(SelectRecipe()));
            return builder.Build();
        }

        private PagedMenu SelectRecipe()
        {
            PagedMenuBuilder builder = new();

            foreach (RecipeData recipe in recipeBook.Recipes)
            {
                builder.AddItem(DescribeRecipe(recipe),
                    m => m.Manager.Next(SelectAmount(recipe)));
            }

            return builder.Build();
        }

        private static PagedMenu SelectAmount(RecipeData recipe)
        {
            PagedMenuBuilder builder = new();

            foreach (uint multi in new uint[] { 1, 2, 5, 10, 20, 50, 100 })
                builder.AddItem($"{multi}x", m => m.Manager.Next(Craft(recipe, multi)));

            return builder.Build();
        }

        private static string DescribeRecipe(RecipeData recipe)
        {
            StringBuilder sb = new();

            foreach (ItemStack stack in recipe.Input)
            {
                sb.Append($"{stack.Amount}x {stack.Name} ");
            }

            sb.Append("=> ");

            foreach (ItemStack stack in recipe.Output)
            {
                sb.Append($"{stack.Amount}x {stack.Name}");
            }

            return sb.ToString();
        }

        private static DisplayMenu Craft(RecipeData recipe, uint multi)
        {
            ExchangeOffer toSource = recipe.Input.Select(i => new ItemStack(i.ItemData, i.Amount * multi)).ToArray();
            ExchangeOffer toTarget = recipe.Output.Select(i => new ItemStack(i.ItemData, i.Amount * multi)).ToArray();

            // Create output items
            Inventory inventory = new();
            foreach (ItemStack stack in toTarget.Offers)
            {
                ItemStack items = new(stack.ItemData, stack.Amount);
                inventory.AddItem(ref items);
            }

            Result result = ExchangeSystem.Exchange(
                source: inventory,
                target: Program.player.Inventory,
                toSource: toSource,
                toTarget: toTarget);

            DisplayMenuBuilder builder = new();

            if (result.IsError)
                builder.Append(result.Message);
            else
            {
                builder.Append($"Successfuly crafter ");
                foreach (ItemStack item in toTarget.Offers)
                {
                    builder.Append($"{item.Amount}x {item.Name} ");
                }
            }

            return builder
                .SetAction(static m => m.Manager.Main())
                .Build();
        }

    }
}
