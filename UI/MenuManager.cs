namespace Farming.UI
{
    public sealed class MenuManager
    {
        private readonly BaseMenu _mainMenu;
        private readonly Stack<BaseMenu> _menus = [];

        private BaseMenu Current => _menus.Count == 0 ? _mainMenu : _menus.Peek();
        public bool IsMain => _menus.Count == 0;

        public static event EventHandler? QuitSignal;

        public MenuManager(BaseMenu main)
        {
            _mainMenu = main;
            _mainMenu.Manager = this;
        }

        public void RaiseQuit() =>
            QuitSignal?.Invoke(this, EventArgs.Empty);

        public void Next(BaseMenu menu)
        {
            menu.Manager = this;
            _menus.Push(menu);
        }

        public void Back()
        {
            _menus.TryPop(out _);
        }

        public void Replace(BaseMenu menu)
        {
            if (_menus.Count > 0)
                _menus.Pop();

            Next(menu);
        }

        public void Main()
        {
            _menus.Clear();
        }

        public void Tick()
        {
            Current.Tick();
        }
    }
}
