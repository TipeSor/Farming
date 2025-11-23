using Farming.Storage;

namespace Farming.Crafting
{
    public record RecipeData(RecipeId ID, ItemStack[] Input, ItemStack[] Output)
    {
        public static RecipeData WoodBurn { get; } =
            RecipeRegistry.Register(RecipeId.BurnWood,
                input: [new ItemStack(ItemData.Wood, 2)],
                output: [new ItemStack(ItemData.Coal, 1)]
            ).Value!;

        public static RecipeData IronSmelt { get; } =
            RecipeRegistry.Register(RecipeId.SmeltIron,
                input: [
                    new ItemStack(ItemData.IronOre, 8),
                    new ItemStack(ItemData.Coal, 1)
                ],
                output: [
                    new ItemStack(ItemData.IronIngot, 8)
                ]
            ).Value!;
    }
}
