using Farming.Crafting;
using Farming.Storage;
using TipeUtils;

namespace Farming.GameContent
{
    public class Content
    {
        public void Initialize()
        {
            InitializeRegisters();
            LoadItems();
            LoadRecipes();
            LoadCrafters();
        }

        private void InitializeRegisters()
        {
            ItemRegistry = new ItemRegistry();
            RecipeRegistry = new RecipeRegistry();
            CrafterRegistry = new CrafterRegistry();
        }

        private void LoadItems()
        {
            ItemIds.Stone = new ItemId("stone");
            ItemIds.Wood = new ItemId("wood");
            ItemIds.Coal = new ItemId("coal");
            ItemIds.IronOre = new ItemId("iron_ore");
            ItemIds.IronIngot = new ItemId("iron_ingot");

            new ItemCreator()
                .SetId(ItemIds.Stone)
                .SetName("Stone")
                .Register(ItemRegistry);

            new ItemCreator()
                .SetId(ItemIds.Wood)
                .SetName("Wood")
                .Register(ItemRegistry);

            new ItemCreator()
                .SetId(ItemIds.Coal)
                .SetName("Coal")
                .Register(ItemRegistry);

            new ItemCreator()
                .SetId(ItemIds.IronOre)
                .SetName("Iron Ore")
                .Register(ItemRegistry);

            new ItemCreator()
                .SetId(ItemIds.IronIngot)
                .SetName("Iron Ingot")
                .Register(ItemRegistry);
        }

        private void LoadRecipes()
        {
            RecipeIds.BurnWood = new RecipeId("burn_wood");
            RecipeIds.SmeltIron = new RecipeId("smelt_iron");

            RecipeRegistry.Register(
                id: RecipeIds.BurnWood,
                input: [(ItemData(ItemIds.Wood), 2)],
                output: [(ItemData(ItemIds.Coal), 1)]
            );

            RecipeRegistry.Register(
                id: RecipeIds.SmeltIron,
                input: [(ItemData(ItemIds.Coal), 2),
                        (ItemData(ItemIds.IronOre), 8)],
                output: [(ItemData(ItemIds.IronIngot), 4)]
            );
        }

        private void LoadCrafters()
        {
            CrafterIds.Furnace = new CrafterId("furnace");

            CrafterRegistry.Register(
                id: CrafterIds.Furnace,
                name: "Furnace",
                recipeBook:
                    new RecipeBook(
                        RecipeData(RecipeIds.BurnWood),
                        RecipeData(RecipeIds.SmeltIron)));

            Creators.Furnace = () => new CrafterObject(CrafterData(CrafterIds.Furnace));
        }

        internal ItemData ItemData(ItemId id) => ItemRegistry.GetData(id).Value!;
        internal RecipeData RecipeData(RecipeId id) => RecipeRegistry.GetData(id).Value!;
        internal CrafterData CrafterData(CrafterId id) => CrafterRegistry.GetData(id).Value!;

        public class DefItemIds
        {
            internal DefItemIds() { }

            public ItemId Stone { get; set; } = null!;
            public ItemId Wood { get; set; } = null!;
            public ItemId Coal { get; set; } = null!;
            public ItemId IronOre { get; set; } = null!;
            public ItemId IronIngot { get; set; } = null!;
        }

        public class DefRecipeIds
        {
            internal DefRecipeIds() { }

            public RecipeId BurnWood { get; set; } = null!;
            public RecipeId SmeltIron { get; set; } = null!;
        }

        public class DefCrafterIds
        {
            internal DefCrafterIds() { }

            public CrafterId Furnace { get; set; } = null!;
        }

        public class DefCreators
        {
            internal DefCreators() { }

            public Func<CrafterObject> Furnace { get; set; } = null!;
        }

        public DefItemIds ItemIds { get; } = new();
        public DefRecipeIds RecipeIds { get; } = new();
        public DefCrafterIds CrafterIds { get; } = new();
        public DefCreators Creators { get; } = new();

        public ItemRegistry ItemRegistry { get; set; } = null!;
        public RecipeRegistry RecipeRegistry { get; set; } = null!;
        public CrafterRegistry CrafterRegistry { get; set; } = null!;
    }

    public static class Extentions
    {
        public static Result<ItemData> GetData(this ItemRegistry registry, ItemId id)
            => registry.Get(id);

        public static Result<RecipeData> GetData(this RecipeRegistry registry, RecipeId id)
            => registry.Get(id);

        public static Result<CrafterData> GetData(this CrafterRegistry registry, CrafterId id)
            => registry.Get(id);

        public static Result<ItemStack> CreateStack(this ItemRegistry registry, ItemId id, uint amount)
            => registry.GetData(id).Map(data => new ItemStack(data, amount));
    }
}
