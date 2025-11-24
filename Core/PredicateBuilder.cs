using System.Numerics;

namespace Farming.Core
{
    public class PredicateBuilder<T>
    {
        private readonly List<Predicate<T>> _predicates = [];

        public PredicateBuilder<T> Equals(T value)
        {
            return Where(v => EqualityComparer<T>.Default.Equals(v, value));
        }

        public PredicateBuilder<T> NotEquals(T value)
        {
            return Where(v => !EqualityComparer<T>.Default.Equals(v, value));
        }

        public PredicateBuilder<T> Where(Predicate<T> p)
        {
            _predicates.Add(p);
            return this;
        }

        public Predicate<T> Build()
        {
            return x => _predicates.TrueForAll(p => p(x));
        }
    }

    public static class PredicateBuilder
    {
        public static PredicateBuilder<T> InRange<T>(this PredicateBuilder<T> builder, T min, T max)
                where T : INumber<T>
        {
            builder.Where(x => x >= min && x <= max);
            return builder;
        }
    }
}
