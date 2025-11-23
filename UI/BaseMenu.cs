namespace Farming.UI
{
    public abstract class BaseMenu
    {
        public MenuManager Manager { get; internal set; } = null!;
        public abstract void Tick();
    }
}
