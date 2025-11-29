using TipeUtils.IO;
using Farming.UI;
using Farming.Core;
using Farming.GameContent;
using Farming.Crafting;
using Farming.Storage;
namespace Farming
{
    public static class Program
    {
        public static Input In { get; } = new();
        public static Output Out { get; } = new();

        private static bool _running = true;

        public static readonly Player player = new();
        private static readonly List<GameObject> objects = [];

        public static void Main()
        {
            InitializeContent();
            InitializeWorld();
            Run();
        }

        private static void InitializeContent()
        {
            InitializeRegisters();
            LoadItems();
            LoadRecipes();
            LoadCrafters();
        }

        private static void InitializeRegisters()
        {
            Item.Registry = new ItemRegistry();
            Recipe.Registry = new RecipeRegistry();
            Crafter.Registry = new CrafterRegistry();
        }

        private static void LoadItems()
        {
            Item.Id.Stone = new ItemId("stone");
            Item.Id.Wood = new ItemId("wood");
            Item.Id.Coal = new ItemId("coal");
            Item.Id.IronOre = new ItemId("iron_ore");
            Item.Id.IronIngot = new ItemId("iron_ingot");

            Item.Data.Stone = new ItemCreator()
                .SetId(Item.Id.Stone)
                .Register(Item.Registry).Value!;

            Item.Data.Wood = new ItemCreator()
                .SetId(Item.Id.Wood)
                .Register(Item.Registry).Value!;

            Item.Data.Coal = new ItemCreator()
                .SetId(Item.Id.Coal)
                .Register(Item.Registry).Value!;

            Item.Data.IronOre = new ItemCreator()
                .SetId(Item.Id.IronOre)
                .Register(Item.Registry).Value!;

            Item.Data.IronIngot = new ItemCreator()
                .SetId(Item.Id.IronIngot)
                .Register(Item.Registry).Value!;
        }

        private static void LoadRecipes()
        {
            Recipe.Id.BurnWood = new RecipeId("burn_wood");
            Recipe.Id.SmeltIron = new RecipeId("smelt_iron");

            Recipe.Data.WoodBurn =
                Recipe.Registry.Register(
                    id: Recipe.Id.BurnWood,
                    input: [(Item.Data.Wood, 2)],
                    output: [(Item.Data.Coal, 1)]
                ).Value!;

            Recipe.Data.IronSmelt =
                Recipe.Registry.Register(
                    id: Recipe.Id.SmeltIron,
                    input: [(Item.Data.Coal, 2),
                            (Item.Data.IronOre, 8)],
                    output: [(Item.Data.IronIngot, 4)]
                ).Value!;
        }

        private static void LoadCrafters()
        {
            Crafter.Id.Furnace = new CrafterId("furnace");

            Crafter.Data.Furnace =
                Crafter.Registry.Register(
                    id: Crafter.Id.Furnace,
                    name: "Furnace",
                    recipeBook:
                        new RecipeBook(
                            Recipe.Data.WoodBurn,
                            Recipe.Data.IronSmelt)).Value!;

            Crafter.Creator.Furnace = static () => new CrafterObject(Crafter.Data.Furnace);
        }

        private static void InitializeWorld()
        {
            objects.Add(player);
            objects.Add(Crafter.Creator.Furnace());
        }

        private static void Run()
        {
            PagedMenuBuilder builder = new();

            foreach (GameObject obj in objects)
                builder.AddItem(obj.Name, m => m.Next(obj.BuildMenu()));

            PagedMenu menu = builder.Build();

            MenuManager menuManager = new(menu);
            MenuManager.QuitSignal += (_, _) => _running = false;

            while (_running)
            {
                Console.Clear();
                menuManager.Tick();
            }
        }

        public static void Stop() => _running = false;
    }
}
