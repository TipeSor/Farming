using System.Text;
using Farming.Core;
using Farming.Crafting;
using Farming.Storage;
using Farming.UI;
using TipeUtils;

namespace Farming.GameContent
{
    public class CrafterObject(CrafterData data) : GameObject
    {
        public override string Name { get; } = data.Name;
        private readonly RecipeBook recipeBook = data.RecipeBook;

        public override BaseMenu BuildMenu()
        {
            PagedMenuBuilder builder = new();
            builder.AddItem("Craft", SelectRecipe);
            return builder.Build();
        }

        private void SelectRecipe(MenuManager manager)
        {
            PagedMenuBuilder builder = new();

            foreach (RecipeData recipe in recipeBook.Recipes)
            {
                builder.AddItem(PrintRecipe(recipe),
                    m => SelectAmount(m, recipe));
            }

            manager.Next(builder.Build());
        }

        private static void SelectAmount(MenuManager manager, RecipeData recipe)
        {
            PagedMenuBuilder builder = new();

            foreach (uint multi in new uint[] { 1, 2, 5, 10, 20, 50 })
                builder.AddItem($"{multi}x", m => Craft(m, recipe, multi));

            manager.Next(builder.Build());
        }

        private static void Craft(MenuManager manager, RecipeData recipe, uint multi)
        {
            Result result = CraftingSystem.Craft(
                target: Program.player.Inventory,
                recipe: recipe,
                multiplier: multi);

            DisplayMenuBuilder builder = new();

            if (result.IsError)
            {
                builder.Append(result.Message);
            }
            else
            {
                builder.Append($"Successfuly crafted: ");
                foreach ((ItemData data, uint amount) in recipe.Output)
                {
                    builder
                        .NewLine()
                        .Append($"{amount * multi}x {data.GetName()} ");
                }
            }

            manager.Next(
                builder
                    .SetAction(static m => m.Manager.Main())
                    .Build());
        }

        private static string PrintRecipe(RecipeData recipe)
        {
            StringBuilder sb = new();

            foreach ((ItemData data, uint amount) in recipe.Input)
            {
                sb.Append($"{amount}x {data.GetName()} ");
            }

            sb.Append("=> ");

            foreach ((ItemData data, uint amount) in recipe.Output)
            {
                sb.Append($"{amount}x {data.GetName()}");
            }

            return sb.ToString();
        }
    }
}
