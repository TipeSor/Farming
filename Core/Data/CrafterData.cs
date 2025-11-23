namespace Farming.Crafting
{
    public record CrafterData(CrafterId Id, string Name, RecipeBook RecipeBook)
    {
        public static CrafterData Furnace { get; } =
            CrafterRegistry.Register(
                id: CrafterId.Furnace,
                name: "Furnace",
                recipeBook: new RecipeBook([
                    RecipeData.WoodBurn,
                    RecipeData.IronSmelt
            ])).Value!;
    }
}

