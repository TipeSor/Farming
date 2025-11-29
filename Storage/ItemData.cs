using System.Collections.Concurrent;
using TipeUtils;

namespace Farming.Storage
{
    public record ItemData
    {
        public ItemId Id { get; init; }
        private static readonly ConcurrentDictionary<ItemId, Lazy<Dictionary<Type, IItemAttribute>>> _shared = new();

        public ItemData(ItemId id, IEnumerable<IItemAttribute> attributes)
        {
            Id = id;
            _shared.GetOrAdd(id, new Lazy<Dictionary<Type, IItemAttribute>>(
                () => attributes.ToDictionary(a => a.GetType(), a => a)));
        }

        public Result<T> GetAttribute<T>() where T : IItemAttribute
        {
            if (!_shared.TryGetValue(Id, out Lazy<Dictionary<Type, IItemAttribute>>? _attributes))
                return Result<T>.Error($"Attributes for `{Id.Value}` not found.");

            if (!_attributes.Value.TryGetValue(typeof(T), out IItemAttribute? value))
                return Result<T>.Error($"Attribute '{typeof(T).Name}' not found.");

            return Result<T>.Ok((T)value);
        }
    }
}
