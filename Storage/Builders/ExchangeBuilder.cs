using TipeUtils;

namespace Farming.Storage
{
    public class ExchangeBuilder
    {
        private Inventory? Target;
        private Inventory? Source;

        private readonly Dictionary<ItemData, uint> ToTarget = [];
        private readonly Dictionary<ItemData, uint> ToSource = [];

        private Phase _phase = Phase.None;

        public ExchangeBuilder SetupSource(Inventory source)
        {
            if (_phase != Phase.None)
                throw new InvalidOperationException("Source must be the first setup.");

            Source = source ?? throw new ArgumentNullException(nameof(source));
            _phase = Phase.SourceSetup;
            return this;
        }

        public ExchangeBuilder SetupTarget(Inventory target)
        {
            if (_phase != Phase.SourceSetup)
                throw new InvalidOperationException("Target must be setup after source.");

            Target = target ?? throw new ArgumentNullException(nameof(target));
            _phase = Phase.TargetSetup;
            return this;
        }


        public ExchangeBuilder AddRequest(ItemStack stack)
        {
            if (_phase is not Phase.SourceSetup and not Phase.TargetSetup)
                throw new InvalidOperationException("AddRequest must follow a setup method.");

            Dictionary<ItemData, uint> dict = _phase == Phase.SourceSetup ? ToSource : ToTarget;
            dict[stack.ItemData] = dict.GetValueOrDefault(stack.ItemData) + stack.Amount;

            return this;
        }


        public Result Exchange()
        {
            if (_phase != Phase.TargetSetup)
                throw new InvalidOperationException("Must finish SetupSource and SetupTarget first.");

            _phase = Phase.Completed;

            return ExchangeSystem.Exchange(
                Source!,
                Target!,
                new ExchangeOffer([.. ToTarget.Select(static kvp => new ItemStack(kvp.Key, kvp.Value))]),
                new ExchangeOffer([.. ToSource.Select(static kvp => new ItemStack(kvp.Key, kvp.Value))])
            );
        }

        private enum Phase
        {
            None,
            SourceSetup,
            TargetSetup,
            Completed
        }
    }
}
