using Farming.UI;

namespace Farming.Core
{
    public abstract class GameObject
    {
        public abstract string Name { get; }
        public abstract BaseMenu BuildMenu();
    }
}
