namespace Farming.Core
{
    public class PredicateBuilder<T>
    {
        private readonly List<Predicate<T>> _predicates = [];

        public PredicateBuilder<T> Equals(T value)
        {
            return Add(v => EqualityComparer<T>.Default.Equals(v, value));
        }

        public PredicateBuilder<T> NotEquals(T value)
        {
            return Add(v => !EqualityComparer<T>.Default.Equals(v, value));
        }

        public PredicateBuilder<T> Add(Predicate<T> p)
        {
            _predicates.Add(p);
            return this;
        }

        public Predicate<T> Build()
        {
            return x => _predicates.TrueForAll(p => p(x));
        }
    }
}
