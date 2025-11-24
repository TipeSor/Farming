using Farming.Crafting;
using Farming.Storage;

namespace Farming.GameContent
{
    public static class Item
    {
        public static class Id
        {
            public static ItemId Stone { get; } = new ItemId("stone");
            public static ItemId Wood { get; } = new ItemId("wood");
            public static ItemId Coal { get; } = new ItemId("coal");
            public static ItemId IronOre { get; } = new ItemId("iron_ore");
            public static ItemId IronIngot { get; } = new ItemId("iron_ingot");
        }

        public static class Data
        {
            public static ItemData Stone { get; } = Registry.Register(Id.Stone, "Stone").Value!;
            public static ItemData Wood { get; } = Registry.Register(Id.Wood, "Wood").Value!;
            public static ItemData Coal { get; } = Registry.Register(Id.Coal, "Coal").Value!;
            public static ItemData IronOre { get; } = Registry.Register(Id.IronOre, "Iron Ore").Value!;
            public static ItemData IronIngot { get; } = Registry.Register(Id.IronIngot, "Iron Ingot").Value!;
        }

        public static ItemRegistry Registry { get; } = new ItemRegistry();
    }

    public static class Recipe
    {
        public static class Id
        {
            public static RecipeId BurnWood { get; } = new RecipeId("burn_wood");
            public static RecipeId SmeltIron { get; } = new RecipeId("smelt_iron");
        }

        public static class Data
        {
            public static RecipeData WoodBurn { get; } =
                Registry.Register(Id.BurnWood,
                    input: [new ItemStack(Item.Data.Wood, 2)],
                    output: [new ItemStack(Item.Data.Coal, 1)]
                ).Value!;

            public static RecipeData IronSmelt { get; } =
                Registry.Register(Id.SmeltIron,
                    input: [
                        new ItemStack(Item.Data.IronOre, 8),
                        new ItemStack(Item.Data.Coal, 1)
                    ],
                    output: [
                        new ItemStack(Item.Data.IronIngot, 8)
                    ]
                ).Value!;
        }

        public static RecipeRegistry Registry { get; } = new RecipeRegistry();
    }

    public static class Crafter
    {
        public static class Id
        {
            public static CrafterId Furnace { get; } = new CrafterId("furnace");
        }

        public static class Data
        {
            public static CrafterData Furnace { get; } =
                Registry.Register(
                    id: Id.Furnace,
                    name: "Furnace",
                    recipeBook: new RecipeBook([
                        Recipe.Data.WoodBurn,
                    Recipe.Data.IronSmelt
                ])).Value!;
        }

        public static class Creator
        {
            public static CrafterObject CreateFurnace() => new CrafterObject(Data.Furnace);
        }

        public static CrafterRegistry Registry { get; } = new CrafterRegistry();
    }
}
