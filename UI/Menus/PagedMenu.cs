using Farming.Core;

namespace Farming.UI
{
    public class PagedMenu : BaseMenu
    {
        private readonly IReadOnlyList<PagedItem> _items;
        private int ElementsPerPage { get; init; }
        private int ItemsPerPage => ElementsPerPage - 3;
        private int _currentPage;
        private int PageCount => Math.Max((_items.Count + ItemsPerPage - 1) / ItemsPerPage, 1);
        private int MaxPageIndex => PageCount - 1;
        private int PageStartIndex => _currentPage * ItemsPerPage;

        public PagedMenu(int elementsPerPage, ICollection<PagedItem> items)
        {
            ElementsPerPage = Math.Max(elementsPerPage, 2);
            _items = [.. items];
        }

        public PagedMenu(ICollection<PagedItem> items)
            : this(9, items) { }

        public PagedMenu(params PagedItem[] items)
            : this((ICollection<PagedItem>)items) { }

        public PagedMenu(int itemsPerPage, params PagedItem[] items)
            : this(itemsPerPage, (ICollection<PagedItem>)items) { }

        private void NextPage()
        {
            if (MaxPageIndex == 0) return;
            _currentPage = (_currentPage + 1) % PageCount;
        }

        private void PrevPage()
        {
            if (MaxPageIndex == 0) return;
            _currentPage = (_currentPage + PageCount - 1) % PageCount;
        }

        private IEnumerable<PagedItem> CurrentPageItems => _items.Skip(PageStartIndex).Take(ItemsPerPage);

        public override void Tick()
        {
            // drawing
            PagedItem?[] items = new PagedItem?[ElementsPerPage];
            {
                if (MaxPageIndex != 0)
                {
                    items[^3] = new PagedItem("Prev", PrevPage);
                    items[^2] = new PagedItem("Next", NextPage);
                }

                items[^1] = Manager.IsMain
                    ? new PagedItem("Quit", Program.Stop)
                    : new PagedItem("Back", Manager.Back);

                PagedItem[] elements = [.. CurrentPageItems];
                Array.Copy(elements, items, elements.Length);
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] is { } item)
                        Console.WriteLine($"{i + 1}. {item.Name}");

                    if (i == ItemsPerPage - 1)
                        Console.WriteLine();
                }
            }

            // handle inputs
            Console.WriteLine();

            Predicate<int> predicate =
                new PredicateBuilder<int>()
                    .InRange(1, items.Length)
                    .Where(value => items[value - 1] != null)
                    .Build();

            int value = Utils.Read(predicate);
            items[value - 1]!.Action(Manager);
        }
    }

    public record PagedItem(string Name, Action<MenuManager> Action)
    {
        public PagedItem(string name, Action action)
            : this(name, _ => action()) { }
    }
}
