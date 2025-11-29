using TipeUtils.IO;
using Farming.UI;
using Farming.Core;
using Farming.GameContent;
namespace Farming
{
    public static class Program
    {
        public static Input In { get; } = new();
        public static Output Out { get; } = new();

        private static bool _running = true;

        public static readonly Player player = new();
        private static readonly List<GameObject> objects = [];

        public static Content Content { get; } = new Content();

        public static void Main()
        {
            Content.Initialize();
            InitializeWorld();
            Run();
        }

        private static void InitializeWorld()
        {
            objects.Add(player);
            objects.Add(Content.Creators.Furnace());
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
