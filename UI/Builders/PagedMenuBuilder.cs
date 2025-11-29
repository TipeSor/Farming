namespace Farming.UI
{
    public class PagedMenuBuilder
    {
        private readonly List<PagedItem> _items = [];
        private const int controlItems = 3;
        private int elementsPerPage = 9;

        public PagedMenuBuilder AddItem(PagedItem item)
        {
            _items.Add(item);
            return this;
        }

        public PagedMenuBuilder AddItem(string name, Action<MenuManager> action)
            => AddItem(new PagedItem(name, action));

        public PagedMenuBuilder SetItemsPerPage(int count)
        {
            return SetElementsPerPage(count + controlItems);
        }

        public PagedMenuBuilder SetElementsPerPage(int count)
        {
            elementsPerPage = Math.Max(count, controlItems);
            return this;
        }

        public PagedMenu Build()
        {
            return new PagedMenu(_items, elementsPerPage);
        }
    }
}
