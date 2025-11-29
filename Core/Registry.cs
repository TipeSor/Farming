using System.Collections;
using TipeUtils;

namespace Farming.Core
{
    public class Registry<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : notnull
        where TValue : notnull
    {
        private readonly Dictionary<TKey, TValue> _reg = [];

        public Result<TValue> Register(TKey key, TValue value)
        {
            if (_reg.ContainsKey(key))
                return Result<TValue>.Error($"Conflicting definition for key '{key}'.");

            try
            {
                _reg[key] = value;
                return Result<TValue>.Ok(value);
            }
            catch (Exception ex)
            {
                return Result<TValue>.Error(ex.Message);
            }
        }

        public Result<TValue> Get(TKey key) =>
            _reg.TryGetValue(key, out TValue? value)
                ? Result<TValue>.Ok(value)
                : Result<TValue>.Error($"{key} not found");

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _reg.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
