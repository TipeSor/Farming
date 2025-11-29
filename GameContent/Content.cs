using Farming.Crafting;
using Farming.Storage;

namespace Farming.GameContent
{
    public static class Item
    {
        public static class Id
        {
            public static ItemId Stone { get; set; } = null!;
            public static ItemId Wood { get; set; } = null!;
            public static ItemId Coal { get; set; } = null!;
            public static ItemId IronOre { get; set; } = null!;
            public static ItemId IronIngot { get; set; } = null!;
        }

        public static class Data
        {
            public static ItemData Stone { get; set; } = null!;
            public static ItemData Wood { get; set; } = null!;
            public static ItemData Coal { get; set; } = null!;
            public static ItemData IronOre { get; set; } = null!;
            public static ItemData IronIngot { get; set; } = null!;
        }

        public static ItemRegistry Registry { get; set; } = null!;
    }

    public static class Recipe
    {
        public static class Id
        {
            public static RecipeId BurnWood { get; set; } = null!;
            public static RecipeId SmeltIron { get; set; } = null!;
        }

        public static class Data
        {
            public static RecipeData WoodBurn { get; set; } = null!;
            public static RecipeData IronSmelt { get; set; } = null!;
        }

        public static RecipeRegistry Registry { get; set; } = null!;
    }

    public static class Crafter
    {
        public static class Id
        {
            public static CrafterId Furnace { get; set; } = null!;
        }

        public static class Data
        {
            public static CrafterData Furnace { get; set; } = null!;
        }

        public static class Creator
        {
            public static Func<CrafterObject> Furnace { get; set; } = null!;
        }

        public static CrafterRegistry Registry { get; set; } = null!;
    }
}
