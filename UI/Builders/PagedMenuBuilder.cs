namespace Farming.UI
{
    public class PagedMenuBuilder
    {
        private readonly List<PagedItem> _items = [];
        private int? elementsPerPage;

        public PagedMenuBuilder AddItem(PagedItem item)
        {
            _items.Add(item);
            return this;
        }

        public PagedMenuBuilder AddItem(string name, Action<BaseMenu> action)
            => AddItem(new PagedItem(name, action));

        public PagedMenuBuilder SetItemsPerPage(int count)
        {
            return SetElementsPerPage(count + 2);
        }

        public PagedMenuBuilder SetElementsPerPage(int count)
        {
            elementsPerPage = Math.Max(count, 2);
            return this;
        }

        public PagedMenu Build()
        {
            if (elementsPerPage == null)
                return new PagedMenu(_items);
            else
                return new PagedMenu((int)elementsPerPage, _items);
        }
    }
}
