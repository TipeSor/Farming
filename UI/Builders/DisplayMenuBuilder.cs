using System.Text;

namespace Farming.UI
{
    public class DisplayMenuBuilder
    {
        private readonly StringBuilder _sb = new();
        private Action<BaseMenu>? action;

        public DisplayMenuBuilder Append(object value)
        {
            _sb.Append(value);
            return this;
        }

        public DisplayMenuBuilder NewLine()
        {
            _sb.AppendLine();
            return this;
        }

        public DisplayMenuBuilder SetAction(Action<BaseMenu> action)
        {
            this.action = action;
            return this;
        }

        public DisplayMenu Build()
        {
            string text = _sb.ToString();
            if (action == null)
                return new DisplayMenu(text);
            else
                return new DisplayMenu(text, action);
        }
    }
}
