using static Farming.Program;
namespace Farming.UI
{

    public class DisplayMenu : BaseMenu
    {
        private readonly string value;
        private readonly Action<BaseMenu> action;

        public DisplayMenu(string value, Action<BaseMenu> action)
        {
            this.value = value;
            this.action = action;
        }

        public DisplayMenu(string value)
            : this(value, static m => { m.Manager.Back(); }) { }

        public DisplayMenu(object value)
            : this(value.ToString() ?? "<null>") { }

        public override void Tick()
        {
            Console.WriteLine(value);
            Console.WriteLine();
            Console.Write(">>> ");

            In.ReadLine();
            action(this);
        }
    }
}
